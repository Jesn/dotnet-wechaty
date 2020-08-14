using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wechaty.Domain.Shared;

namespace Wechaty.Application.Contracts
{
    public interface IWechatyHandlerService : IApplicationService
    {
        public IWechatyBotService WechatBot();
        void On<T>(EventEnum @event, Func<T, Task> func);
    }
}
