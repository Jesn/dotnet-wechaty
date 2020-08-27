using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Wechaty.TestBase
{
    [DependsOn(
         typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule)
        )]
    public class WechatyTestBaseModule : AbpModule
    {

    }
}
