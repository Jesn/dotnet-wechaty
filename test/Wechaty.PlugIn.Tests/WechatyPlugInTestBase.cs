using Volo.Abp.Modularity;
using Wechaty.Plugin.Base;
using Wechaty.TestBase;

namespace Wechaty.PlugIn.Tests
{
    [DependsOn(
        typeof(WechatyPluginBaseModule),
        typeof(WechatyPlugInWeatherModule)
     )]
    public class WechatyPlugInTestBase : WechatyTestBase<WechatyPlugInTestModule>
    {
    }
}
