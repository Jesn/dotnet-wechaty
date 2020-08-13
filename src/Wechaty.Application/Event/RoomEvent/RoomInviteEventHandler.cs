using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class RoomInviteEventHandler : AccessoryAppService, IDistributedEventHandler<EventRoomInvitePayload>
    {
        public async Task HandleEventAsync(EventRoomInvitePayload eventData)
        {
            var roomInvitationService = await RoomInvitation.Load(eventData.RoomInvitationId);
        }

    }
}
