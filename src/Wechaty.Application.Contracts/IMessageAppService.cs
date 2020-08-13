using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Wechaty.PuppetModel;
using System.Threading.Tasks;

namespace Wechaty.Application.Contracts
{
    public interface IMessageAppService : IApplicationService
    {

        MessagePayload Payload { get; }

        Task<IMessageAppService> Load(string id);

        Task<MessagePayload> Ready(string id);


        IContactAppService From { get; }
        IContactAppService To { get; }
        IRoomAppService Rooms { get; }


        Task<MessagePayload> Say(string text);
        Task<MessagePayload> Say(string text, MessagePayload payload);

    }
}
