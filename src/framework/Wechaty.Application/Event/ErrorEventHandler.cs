using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class ErrorEventHandler : AccessoryAppService, IDistributedEventHandler<EventErrorPayload>
    {
        public async Task HandleEventAsync(EventErrorPayload eventData)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Error ：{eventData.Data}");
            });

        }
    }
}
