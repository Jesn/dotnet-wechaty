using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.Domain.Shared;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class RoomTopicEventHandler : AccessoryAppService, IDistributedEventHandler<EventRoomTopicPayload>
    {

        public async Task HandleEventAsync(EventRoomTopicPayload eventData)
        {
            var room = await Room.Load(eventData.RoomId);
            var changer = await Room.Load(eventData.ChangerId);
            var date = ((long)(eventData.Timestamp)).TimestampToDateTime();

            var model = new RoomTopicActionData()
            {
                Room = room,
                Changer = changer,
                Date = date,
                NewTopic = eventData.NewTopic,
                OldTopic = eventData.OldTopic,
            };

        }
    }
}
