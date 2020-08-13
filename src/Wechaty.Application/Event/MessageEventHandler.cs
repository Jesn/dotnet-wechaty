using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class MessageEventHandler : AccessoryAppService, IDistributedEventHandler<EventMessagePayload>
    {
        public async Task HandleEventAsync(EventMessagePayload eventData)
        {
            var messages = await Message.Load(eventData.MessageId);
            if (messages != null)
            {
                Console.WriteLine($"消息体:{JsonConvert.SerializeObject(messages.Payload)}");
            }
        }
    }
}
