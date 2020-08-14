using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class LogoutEventHandler : AccessoryAppService, IDistributedEventHandler<EventLogoutPayload>
    {
        public async Task HandleEventAsync(EventLogoutPayload eventData)
        {
            var contact = await Contact.LoadSelf();
            Console.WriteLine($"logout:{JsonConvert.SerializeObject(eventData)}");
        }
    }
}
