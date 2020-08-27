using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class RoomJoinEventHandler : AccessoryAppService, IDistributedEventHandler<EventRoomJoinPayload>
    {
        public async Task HandleEventAsync(EventRoomJoinPayload eventData)
        {
            var room = await Room.Load(eventData.RoomId);
            var inviteedList = await Contact.LoadAll(eventData.InviteeIdList);
            var inviter = await Contact.LoadInfo(eventData.InviterId);
            var date = ((long)eventData.Timestamp).TimestampToDateTime();

            var model = new RoomJoinActionData()
            {
                Room = room,
                InviteedList = inviteedList,
                Inviter = inviter
            };

        }
    }
}
