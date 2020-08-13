using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class DongEventHandler : AccessoryAppService, IDistributedEventHandler<EventDongPayload>
    {
        public async Task HandleEventAsync(EventDongPayload eventData)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"dong：{eventData.Data}");
            });

        }
    }
}
