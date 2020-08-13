using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared;
using Wechaty.PuppetContracts;
using Wechaty.PuppetHostie;
using Wechaty.PuppetModel;

namespace Wechaty.Application
{
    [DependsOn(
        typeof(AbpEventBusModule),
        typeof(AbpCachingModule),
        typeof(WechatyApplicationContractModule),
        typeof(WechatyPuppetContractsModule),
        //typeof(WechatyPuppetModelModule),
        typeof(WechatyPuppetHostieModule),
        typeof(WechatyDomainSharedModule)
        )]
    public class WechatyApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPreApplicationInitialization(context);
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            base.PostConfigureServices(context);
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            base.OnApplicationShutdown(context);
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            base.PreConfigureServices(context);
        }

    }

}
