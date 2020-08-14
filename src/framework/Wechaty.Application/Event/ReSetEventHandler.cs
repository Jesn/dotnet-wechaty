using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Event
{
    public class ReSetEventHandler : IDistributedEventHandler<EventResetPayload>, ITransientDependency
    {
        public async Task HandleEventAsync(EventResetPayload eventData)
        {
            Console.WriteLine($"ReSet Handler:{eventData.Data}");
        }
    }
}
