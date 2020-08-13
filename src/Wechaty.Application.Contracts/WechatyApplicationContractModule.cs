using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Wechaty.Domain.Shared;

namespace Wechaty.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        //typeof(WechatyPuppetModelModule),
        typeof(WechatyDomainSharedModule)
        )]
    public class WechatyApplicationContractModule : AbpModule
    {
    }
}
