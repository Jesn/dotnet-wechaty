using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Wechaty.Application.Contracts
{
    public interface IRoomAppService: IApplicationService
    {
        Task<IRoomAppService> Load(string roomId);
        Task Ready(string roomId, bool forceSync = false);
    }
}
