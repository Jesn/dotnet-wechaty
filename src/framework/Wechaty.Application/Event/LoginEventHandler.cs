using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class LoginEventHandler : AccessoryAppService, IDistributedEventHandler<EventLoginPayload>
    {
        public async Task HandleEventAsync(EventLoginPayload eventData)
        {
            LoginWeiXinId = eventData.ContactId;
            var contact = await Contact.LoadInfo(eventData.ContactId);

            Console.WriteLine($"Login User Id :{eventData.ContactId}");
        }
    }
}
