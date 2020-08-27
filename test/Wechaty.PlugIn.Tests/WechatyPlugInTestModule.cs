using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Wechaty.PlugIn.Tests
{
    [DependsOn(typeof(WechatyPlugInWeatherModule))]
    public class WechatyPlugInTestModule : AbpModule
    {
    }
}
