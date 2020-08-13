using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Contracts
{
    public interface IContactAppService : IApplicationService
    {
      
        Task Ready();
        Task Ready(string contactId, bool forceSync = false);
        Task<IContactAppService> Load(string id);

        Task<List<IContactAppService>> LoadAll(IReadOnlyList<string> contactIdList);

        Task SetAlias(string newAlias = default);

        Task<FileBox> Avatar();
     

        bool IsReady { get; }

        bool IsSelf { get; }

        string Id { get; }
        string WeiXin { get; }

        string Name { get; }
        ContactType Type { get; }
        string Alias { get; }
        bool? IsFriend { get; }
        bool Personal { get; }
        bool? Star { get; }
        ContactGender Gender { get; }
        string Province { get; }
        string City { get; }


    }
}
