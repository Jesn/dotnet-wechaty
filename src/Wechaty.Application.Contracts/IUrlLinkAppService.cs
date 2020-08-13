using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Contracts
{
    public interface IUrlLinkAppService:IApplicationService
    {
        Task<UrlLinkPayload> Create(string url);
    }
}
