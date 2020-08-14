using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    /// <summary>
    /// Heart Beat Event
    /// </summary>
    public class HeartBeatEventHandler : AccessoryAppService, IDistributedEventHandler<EventHeartbeatPayload>
    {
        public async Task HandleEventAsync(EventHeartbeatPayload eventData)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"当前时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}，心跳包： {eventData.Data}");
            });
        }
    }
}
