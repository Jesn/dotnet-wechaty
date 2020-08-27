using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class RoomAppService : AccessoryAppService, IRoomAppService
    {
        private readonly IDistributedCache<RoomPayload> _cacheRoomPayload;
        private readonly IDistributedCache<RoomMemberPayload> _cacheRoomMemberPayload;
        protected RoomPayload RoomPayload { get; set; }
        public string RoomId { get; set; }
        public bool IsReady => RoomPayload != null;


        public RoomAppService(IDistributedCache<RoomPayload> cacheRoomPayload, IDistributedCache<RoomMemberPayload> cacheRoomMemberPayload)
        {
            _cacheRoomPayload = cacheRoomPayload;
            _cacheRoomMemberPayload = cacheRoomMemberPayload;
        }

        public async Task<IRoomAppService> Load(string roomId)
        {
            RoomPayload = await _cacheRoomPayload.GetOrAddAsync(roomId,
            async () => await _puppetService.RoomRawPayload(roomId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
            });
            return this;
        }

        public async Task Ready(string roomId, bool forceSync = false)
        {
            if (!forceSync && IsReady)
            {
                return;
            }

            if (forceSync)
            {
                await RoomPayloadDirty(roomId);
                await RoomMemberPayloadDirty(roomId);
            }

            RoomPayload = await _cacheRoomPayload.GetOrAddAsync(roomId,
               async () => await _puppetService.RoomRawPayload(roomId),
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
               });

            var memberIdList = await _puppetService.RoomMemberList(roomId);

            Task.WhenAll(memberIdList.Select(async id =>
              {
                  try
                  {
                      await Contact.Ready(id);
                  }
                  catch (Exception ex)
                  {
                      Logger.LogError($"ready() member.ready() rejection: {ex.Message}");
                  }
              })).Wait();
        }

        private async Task<IMessageAppService> TryLoad(string msgId)
        {
            if (string.IsNullOrWhiteSpace(msgId))
            {
                return null;
            }
            var msg = await Message.Load(msgId);

            return msg;
        }

        public async Task<IMessageAppService> Say(string text, params ContactAppService[] replyTo)
        {
            if (replyTo != null)
            {
                var memtionAlias = replyTo.Select(c => c.Alias ?? c.Name);
                text = '@' + string.Join('@', memtionAlias);
            }
            //var msgId = await Puppet.MessageSendText(Id, text, replyTo?.Select(c => c.Id));
            var msgId = await _puppetService.MessageSendText(RoomId, text);
            return await TryLoad(msgId);
        }

        public async Task<IMessageAppService> Say(FileBox file)
        {
            var msgId = await _puppetService.MessageSendFile(RoomId, file);
            return await TryLoad(msgId);
        }

        public async Task<IMessageAppService> Say(UrlLinkPayload urlLink)
        {
            var msgId = await _puppetService.MessageSendUrl(RoomId, urlLink);
            return await TryLoad(msgId);
        }

        public async Task<IMessageAppService> Say(MiniProgramPayload miniProgram)
        {
            var msgId = await _puppetService.MessageSendMiniProgram(RoomId, miniProgram);
            return await TryLoad(msgId);
        }

        public async Task<IMessageAppService> Say(IContactAppService contact)
        {
            var msgId = await _puppetService.MessageSendContact(RoomId, contact.Id);
            return await TryLoad(msgId);
        }

        public async Task Add(string contactId)
        {
            await _puppetService.RoomAdd(RoomId, contactId);
        }

        public async Task Delete(string contactId)
        {
            await _puppetService.RoomDel(RoomId, contactId);
        }

        public async Task Quit()
        {
            await _puppetService.RoomQuit(RoomId);
        }

        public async Task<string> GetTopic()
        {
            //if (!IsReady)
            //{
            //    Logger.LogWarning("topic() room not ready");
            //    return default ;
            //}
            if (RoomPayload != null && !string.IsNullOrWhiteSpace(RoomPayload.Topic))
            {
                return RoomPayload.Topic;
            }
            else
            {
                var memberIdList = await _puppetService.RoomMemberList(RoomId);
                var memberList = memberIdList
                    .Where(id => id != LoginWeiXinId)
                    .Select(id => Contact.LoadInfo(id));

                return string.Concat(",", memberList.Take(3).Select(x => x.Result.Name));
            }
        }

        public async Task SetTopic(string newTopic)
        {
            await _puppetService.RoomTopic(RoomId, newTopic);
        }

        public async Task<string> GetAnnounce()
        {
            var announce = await _puppetService.RoomAnnounce(RoomId);
            return announce;
        }

        public async Task<string> SetAnnounce(string text)
        {
            return await _puppetService.RoomAnnounce(RoomId, text);
        }

        public async Task<string> QrCode()
        {
            var qrCode = await _puppetService.RoomQRCode(RoomId);
            return qrCode;
        }

        public async Task<string> Alias(string contactId)
        {
            var memberPayload = await _cacheRoomMemberPayload.GetOrAddAsync(
                CacheConst.CacheKeyRoomMember(RoomId, contactId),
                async () => await _puppetService.RoomMemberRawPayload(RoomId, contactId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
                });

            return memberPayload?.Name;
        }

        public async Task<bool> Has(string contactId)
        {
            var memberIdList = await _puppetService.RoomMemberList(RoomId);
            return memberIdList.Any(id => contactId == id);
        }

        public async Task<IReadOnlyList<IContactAppService>> MemberAllAsync()
        {
            var memberIdList = await _puppetService.RoomMemberList(RoomId);
            var list = memberIdList.Select(id => Contact.LoadInfo(id).Result);
            return list.ToImmutableList();
        }

        public IContactAppService Owner
        {
            get
            {
                var id = RoomPayload.OwnerId;
                return Contact.LoadInfo(id).Result;
            }
        }

        public Task<FileBox> Avatar
        {
            get
            {
                return _puppetService.RoomAvatar(RoomId);
            }
        }

        private async Task RoomPayloadDirty(string roomId)
        {
            await _cacheRoomPayload.RemoveAsync(roomId);
        }

        private async Task RoomMemberPayloadDirty(string roomId)
        {
            var contactIdList = await _puppetService.RoomMemberList(RoomPayload.Id);
            if (contactIdList.IsNullOrEmpty())
            {
                return;
            }

            Logger.LogInformation($"remove room member cache, room:{roomId},contact:{string.Join(',', contactIdList)}");

            foreach (var contactId in contactIdList)
            {
                var cacheKey = CacheConst.CacheKeyRoomMember(roomId, contactId);
                _ = _cacheRoomPayload.RemoveAsync(cacheKey);
            }
        }



        // TODO Event:===> invite leave join topic

    }
}
