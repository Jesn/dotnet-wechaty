using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Wechaty.Application.Contracts
{
    public interface ITagAppService: IApplicationService
    {
        Task<List<string>> Load(string contactId = default);
        Task Add(string contactId, string tagId);
        Task Remove(string contactId, string tagId);
        Task Delete(string contactId);

    }
}
