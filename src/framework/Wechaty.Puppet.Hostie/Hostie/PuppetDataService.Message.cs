using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wechaty.PuppetModel;
using ImageType = Wechaty.PuppetModel.ImageType;

namespace Wechaty.PuppetHostie
{
    public partial class PuppetDataService
    {
        public async Task<MiniProgramPayload> MessageMiniProgram(string messageId)
        {
            var request = new MessageMiniProgramRequest();
            request.Id = messageId;

            var response = await grpcClient.MessageMiniProgramAsync(request);
            var payload = JsonConvert.DeserializeObject<MiniProgramPayload>(response?.MiniProgram);
            return payload;
        }

        public async Task<FileBox> MessageImage(string messageId, ImageType imageType)
        {
            var request = new MessageImageRequest();
            request.Id = messageId;
            request.Type = (github.wechaty.grpc.puppet.ImageType)imageType;

            var response = await grpcClient.MessageImageAsync(request);

            return JsonConvert.DeserializeObject<FileBox>(response.Filebox);
        }

        public async Task<string> MessageContact(string messageId)
        {
            var request = new MessageContactRequest();
            request.Id = messageId;

            var response = await grpcClient.MessageContactAsync(request);
            return response?.Id;
        }

        public async Task<string> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        {
            var request = new MessageSendMiniProgramRequest();
            request.ConversationId = conversationId;
            request.MiniProgram = JsonConvert.SerializeObject(miniProgramPayload);

            var response = await grpcClient.MessageSendMiniProgramAsync(request);
            return response?.Id;
        }

        /// <summary>
        /// 消息撤销
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<bool> MessageRecall(string messageId)
        {
            var request = new MessageRecallRequest();
            request.Id = messageId;

            var response = await grpcClient.MessageRecallAsync(request);
            if (response == null)
            {
                return false;
            }
            return response.Success;
        }

        public async Task<FileBox> MessageFile(string id)
        {
            var request = new MessageFileRequest();
            request.Id = id;

            var response = await grpcClient.MessageFileAsync(request);

            return JsonConvert.DeserializeObject<FileBox>(response?.Filebox);
        }

        public async Task<MessagePayload> MessageRawPayload(string messageId)
        {
            MessagePayload payload = new MessagePayload();

            var request = new MessagePayloadRequest()
            {
                Id = messageId
            };
            var response = await grpcClient.MessagePayloadAsync(request);

            if (response != null)
            {
                payload = new MessagePayload()
                {
                    Id = messageId,
                    Filename = response.Filename,
                    FromId = response.FromId,
                    Text = response.Text,
                    MentionIdList = response.MentionIds.ToList(),
                    RoomId = response.RoomId,
                    Timestamp = response.Timestamp,
                    Type = (PuppetModel.MessageType)response.Type,
                    ToId = response.ToId
                };
            }
            return payload;
        }

        public MessagePayload MessageRawPayloadParser(MessagePayload payload)
        {
            return payload;
        }

        public async Task<string> MessageSendText(string conversationId, string text)
        {
            var request = new MessageSendTextRequest()
            {
                ConversationId = conversationId,
                Text = text,
                //MentonalIds = mentonalIds
            };

            var response = await grpcClient.MessageSendTextAsync(request);
            return response?.Id;
        }

        public async Task<string> MessageSendFile(string conversationId, FileBox file)
        {
            var request = new MessageSendFileRequest();
            request.ConversationId = conversationId;
            request.Filebox = JsonConvert.SerializeObject(file);

            var response = await grpcClient.MessageSendFileAsync(request);
            return response?.Id;
        }

        public async Task<string> MessageSendContact(string conversationId, string contactId)
        {
            var request = new MessageSendContactRequest()
            {
                ConversationId = conversationId,
                ContactId = contactId
            };

            var response = await grpcClient.MessageSendContactAsync(request);
            return response?.Id;
        }

        public async Task<string> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        {
            var request = new MessageSendUrlRequest()
            {
                ConversationId = conversationId,
                UrlLink = JsonConvert.SerializeObject(urlLinkPayload)
            };

            var response = await grpcClient.MessageSendUrlAsync(request);
            return response?.Id;
        }

        public async Task<UrlLinkPayload> MessageUrl(string messageId)
        {
            var request = new MessageUrlRequest()
            {
                Id = messageId
            };

            var response = await grpcClient.MessageUrlAsync(request);
            var payload = JsonConvert.DeserializeObject<UrlLinkPayload>(response?.UrlLink);
            return payload;
        }
    }
}
