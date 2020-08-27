using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class FriendshipAppService : AccessoryAppService, IFriendshipAppService
    {
        private readonly IDistributedCache<FriendshipPayload> _cacheFriendShipPayload;
        private new readonly ILogger<FriendshipAppService> Logger;

        protected FriendshipPayload Payload { get; set; }

        public FriendshipAppService(IDistributedCache<FriendshipPayload> cacheFriendShipPayload)
        {
            _cacheFriendShipPayload = cacheFriendShipPayload;
            Logger = NullLogger<FriendshipAppService>.Instance;
        }

        public async Task<FriendshipPayload> Load(string id)
        {
            Payload = await _cacheFriendShipPayload.GetOrAddAsync(id,
                async () => await _puppetService.FriendshipRawPayload(id),
                () => new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheConst.CacheGlobalExpirationTime)
                });
            return Payload;
        }

        public async Task Accept(string friendShipId)
        {
            if (Payload == null)
            {
                Logger.LogWarning("Accept() no payload");
                return;
            }
            if (Payload.Type != FriendshipType.Receive)
            {
                Logger.LogWarning($"accept() payload type must Receive,but this  payload type is {Payload.Type.ToString()} ");
                return;
            }
            await _puppetService.FriendshipAccept(friendShipId);
            await Policy.Handle<Exception>()
                      .WaitAndRetryAsync(10, (attempt) => TimeSpan.FromSeconds((1d - new Random().NextDouble()) * Math.Pow(2, attempt)), (exception, attempt) =>
                      {
                          if (Logger.IsEnabled(LogLevel.Trace))
                          {
                              Logger.LogTrace($"accept() retry() ready() attempt {attempt}");
                          }
                      }).ExecuteAsync(async () =>
                      {
                          //await contact.Ready();
                          //if (IsReady)
                          //{
                          //    if (Logger.IsEnabled(LogLevel.Trace))
                          //    {
                          //        Logger.LogTrace($"accept() with contact {contact.Name} ready()");
                          //    }
                          //    return;
                          //}

                          await ContactInfo.Ready();

                          throw new InvalidOperationException("Friendship.accept() contact.ready() not ready");
                      }).ContinueWith(task =>
                      {
                          if (task.IsFaulted)
                          {
                              Logger.LogWarning($"accept() contact {ContactInfo} not ready because of {task.Exception?.Message}");
                          }
                      });

        }

        public IContactAppService ContactInfo
        {
            get
            {
                return Contact.LoadInfo(Payload.ContactId).Result;
            }
        }
    }
}
