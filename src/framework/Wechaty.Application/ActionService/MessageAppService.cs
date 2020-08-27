using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Wechaty.Application.Contracts;
using Wechaty.PuppetModel;

namespace Wechaty.Application.ActionService
{
    public class MessageAppService : AccessoryAppService, IMessageAppService
    {
        private string AT_SEPRATOR_REGEX = "[\\u2005\\u0020]";
        protected MessagePayload MessagePayload;

        private new ILogger<MessageAppService> Logger { get; set; }

        public MessageAppService()
        {
            Logger = NullLogger<MessageAppService>.Instance;
        }

        public async Task<IMessageAppService> Load(string id)
        {
            await Ready(id);
            return this;
        }


        public async Task<MessagePayload> Ready(string id)
        {
            MessagePayload = await _puppetService.MessageRawPayload(id);
            return MessagePayload;
        }


        public async Task<MessagePayload> Say(string text, MessagePayload payload)
        {
            var roomId = payload.RoomId;
            var fromId = payload.FromId;
            var conversationId = roomId.IsNullOrEmpty() ? fromId : roomId;
            if (conversationId == null)
            {
                //throw new InvalidOperationException("neither room nor from?");
                Logger.LogError("neither room nor from?");
            }
            var msgId = await _puppetService.MessageSendText(conversationId, text);
            if (!msgId.IsNullOrWhiteSpace())
            {
                var result = await Ready(msgId);
                return result;
            }
            return null;
        }

        public async Task<MessagePayload> Say(string text)
        {
            return await Say(text, MessagePayload);
        }

        public MessagePayload Payload => MessagePayload;

        public IContactAppService From
        {
            get
            {
                if (MessagePayload == null)
                {
                    throw new UserFriendlyException("no payload");
                }
                var id = MessagePayload.FromId;
                if (id.IsNullOrEmpty())
                {
                    return null;
                }
                var contact = Contact.LoadInfo(id).Result;
                return contact;
            }
        }

        public IContactAppService To
        {
            get
            {
                if (MessagePayload == null)
                {
                    throw new UserFriendlyException("no payload");
                }
                var id = MessagePayload.ToId;
                if (id.IsNullOrEmpty())
                {
                    return null;
                }
                var contact = Contact.LoadInfo(id).Result;
                return contact;
            }
        }

        public IRoomAppService Rooms
        {
            get
            {
                if (MessagePayload == null)
                {
                    throw new UserFriendlyException("no payload");
                }
                var id = MessagePayload.RoomId;
                if (id.IsNullOrEmpty())
                {
                    return null;
                }
                var room = Room.Load(id).Result;
                return room;
            }
        }


    }
}
