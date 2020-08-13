using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Wechaty.Plugin.Base;

namespace Wechaty.PlugIn
{
    public class WechatyPlugInWeatherModule : WechatyPluginBaseModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            base.PreConfigureServices(context);

            // 跳过Abp 框架的自动注册
            SkipAutoServiceRegistration = true;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 手动注册当前服务
            context.Services.AddAssemblyOf<WechatyPlugInWeatherModule>();
        }

    }
}
