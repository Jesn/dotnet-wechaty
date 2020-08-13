using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class ReadyEventHandler : AccessoryAppService, IDistributedEventHandler<EventReadyPayload>
    {
        public async Task HandleEventAsync(EventReadyPayload eventData)
        {
            Console.WriteLine($"Ready:{eventData.Data}");
            readyState = StateEnum.On;
        }
    }
}
