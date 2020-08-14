using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;
using Wechaty.Application;
using Wechaty.Application.Contracts;
using Wechaty.Domain.Shared.DTO;

namespace Wechaty.Getting.Start
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEventBusModule),
        typeof(WechatyApplicationModule),
        typeof(WechatyApplicationContractModule)
        //,typeof(WechatyPluginBaseModule)
        )]
    public class WechatyConsoleModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            //ConfigWechatyOption(configuration);
        }


        private void ConfigWechatyOption(IConfiguration configuration)
        {
            Configure<WechatyOptions>(options =>
            {
                options.Name = configuration["Wechaty_Name"];
                options.Token = configuration["Wechaty_Token"];
                options.EndPoint = configuration["Wechaty_EndPoint"];
            });
        }
    }
}
