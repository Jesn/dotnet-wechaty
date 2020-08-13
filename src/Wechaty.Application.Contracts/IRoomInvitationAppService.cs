using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Wechaty.Application.Contracts
{
    public interface IRoomInvitationAppService : IApplicationService
    {
        Task<IRoomInvitationAppService> Load(string roomId);
        Task Accept(string roomId);
        Task<string> Topic();
        Task<int> MemberCount(string roomId);
        Task<DateTime> Date(string roomId);
        Task<long> Age(string roomId);

        //Task<IReadOnlyList<IContactAppService>> MemberList();
        Task<IReadOnlyList<IContactAppService>> RoomMemberList(string roomId);

        Task<IContactAppService> Inviter(string roomId);
    }
}
