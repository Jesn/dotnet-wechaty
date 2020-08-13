using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class RoomInvitationAppService : AccessoryAppService, IRoomInvitationAppService
    {
        private readonly IDistributedCache<RoomInvitationPayload> _cacheRoomInvitationPayload;
        protected RoomInvitationPayload Payload { get; set; }

        public RoomInvitationAppService(IDistributedCache<RoomInvitationPayload> cacheRoomInvitationPayload)
        {
            _cacheRoomInvitationPayload = cacheRoomInvitationPayload;
        }

        public async Task<string> Topic() => (await GetRoomInvitationPayload(Payload.Id)).Topic ?? "";

        public async Task<IRoomInvitationAppService> Load(string roomId)
        {
            await GetRoomInvitationPayload(roomId);
            return this;
        }

        public async Task<int> MemberCount(string roomId)
        {
            var payload = await GetRoomInvitationPayload(roomId);
            return (int)payload.MemberCount;
        }

        public async Task<IReadOnlyList<IContactAppService>> RoomMemberList(string roomId)
        {
            await GetRoomInvitationPayload(roomId);
            var contact = await Contact.LoadAll(Payload.MemberIdList);
            return contact;
        }
        public async Task<DateTime> Date(string roomId)
        {
            await GetRoomInvitationPayload(roomId);
            return Payload.Timestamp.TimestampToDateTime();
        }

        public async Task<long> Age(string roomId)
        {
            var receiveDate = await Date(roomId);
            var timstamp = DateTime.Now - receiveDate;
            return (long)timstamp.TotalSeconds;
        }


        public async Task Accept(string roomId)
        {
            await _puppetService.RoomInvitationAccept(roomId);
            var inviter = await Inviter(roomId);
            var topic = Topic();
            await inviter.Ready();
        }

        public async Task<IContactAppService> Inviter(string roomId)
        {
            var payload = await _puppetService.RoomInvitationRawPayload(roomId);
            var inviter = await Contact.LoadInfo(payload.InviterId);
            return inviter;
        }

        private async Task<RoomInvitationPayload> GetRoomInvitationPayload(string roomId)
        {
            Payload = await _cacheRoomInvitationPayload.GetOrAddAsync(roomId,
                async () => _puppetService.RoomInvitationRawPayloadParser(await _puppetService.RoomInvitationRawPayload(roomId)),
                () => new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
                });
            return Payload;
        }




    }
}
