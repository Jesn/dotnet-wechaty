using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class FriendshipEventHandler : AccessoryAppService, IDistributedEventHandler<EventFriendshipPayload>
    {
        public async Task HandleEventAsync(EventFriendshipPayload eventData)
        {
            var friendship = await Friendship.Load(eventData.FriendshipId);
        }
    }
}
