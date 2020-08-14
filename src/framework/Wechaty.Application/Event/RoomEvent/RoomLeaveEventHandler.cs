using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class RoomLeaveEventHandler : AccessoryAppService, IDistributedEventHandler<EventRoomLeavePayload>
    {
        public async Task HandleEventAsync(EventRoomLeavePayload eventData)
        {
            var leaverList = await Contact.LoadAll(eventData.RemoveeIdList);
            var remover = await Contact.LoadInfo(eventData.RemoverId);
            var room = await Room.Load(eventData.RoomId);

            var model = new RoomLevelActionData()
            {
                Room = room,
                LeaverList = leaverList,
                Remover = remover
            };
            Console.WriteLine($"{remover.Name}离开了{eventData.RoomId}房间");

        }
    }
}
