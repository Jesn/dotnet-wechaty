using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Modularity;
using Wechaty.PuppetContracts;

namespace Wechaty.PuppetHostie
{
    [DependsOn(
        typeof(AbpEventBusModule),
        typeof(WechatyPuppetContractsModule)
        )]
    public class WechatyPuppetHostieModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddTransient(sp => (IPuppetTestService)sp.GetRequiredService<PuppetTestService>());
        }
    }
}
