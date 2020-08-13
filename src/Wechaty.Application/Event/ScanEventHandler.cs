using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class ScanEventHandler : AccessoryAppService, IDistributedEventHandler<EventScanPayload>
    {

        public async Task HandleEventAsync(EventScanPayload eventData)
        {
            await Task.Run(() =>
            {
                string qrcodeImageUrl = "https://wechaty.github.io/qrcode/" + eventData.Qrcode;
                Console.WriteLine($"onScan {eventData.Status},qrCodeUrl：{qrcodeImageUrl}");
            });
        }


    }

}
