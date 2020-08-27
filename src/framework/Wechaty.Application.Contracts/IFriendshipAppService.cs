using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wechaty.PuppetModel;

namespace Wechaty.Application.Contracts
{
    public interface IFriendshipAppService : IApplicationService
    {
        IContactAppService ContactInfo { get; }


        Task<FriendshipPayload> Load(string id);
    }
}
