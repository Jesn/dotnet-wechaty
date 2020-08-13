using System.Threading.Tasks;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetContracts
{
    public partial interface IPuppetDataService
    {
         Task<MiniProgramPayload> MessageMiniProgram(string messageId);
         Task<FileBox> MessageImage(string messageId, ImageType imageType);
         Task<string> MessageContact(string messageId);
         Task<string> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload);
         Task<bool> MessageRecall(string messageId);
         Task<FileBox> MessageFile(string id);
         Task<MessagePayload> MessageRawPayload(string messageId);
         MessagePayload MessageRawPayloadParser(MessagePayload payload);
         Task<string> MessageSendText(string conversationId, string text);
         Task<string> MessageSendFile(string conversationId, FileBox file);
         Task<string> MessageSendContact(string conversationId, string contactId);
         Task<string> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload);
         Task<UrlLinkPayload> MessageUrl(string messageId);

    }
}
