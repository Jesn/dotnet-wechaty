using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Wechaty.PuppetModel;

namespace Wechaty.PuppetCache
{
    public class PuppetCacheData: ITransientDependency
    {
        private readonly IDistributedCache<ContactPayload> _cacheContactPayload;
        private readonly IDistributedCache<FriendshipPayload> _cacheFriendshipPayload;
        private readonly IDistributedCache<MessagePayload> _cacheMessagePayload;
        private readonly IDistributedCache<RoomPayload> _cacheRoomPayload;
        private readonly IDistributedCache<RoomMemberPayload> _cacheRoomMemberPayload;
        private readonly IDistributedCache<RoomInvitationPayload> _cacheRoomInvitationPayload;

        public PuppetCacheData(IDistributedCache<ContactPayload> cacheContactPayload,
            IDistributedCache<FriendshipPayload> cacheFriendshipPayload,
            IDistributedCache<MessagePayload> cacheMessagePayload,
            IDistributedCache<RoomPayload> cacheRoomPayload,
            IDistributedCache<RoomMemberPayload> cacheRoomMemberPayload,
            IDistributedCache<RoomInvitationPayload> cacheRoomInvitationPayload)
        {
            _cacheContactPayload = cacheContactPayload;
            _cacheFriendshipPayload = cacheFriendshipPayload;
            _cacheMessagePayload = cacheMessagePayload;
            _cacheRoomPayload = cacheRoomPayload;
            _cacheRoomMemberPayload = cacheRoomMemberPayload;
            _cacheRoomInvitationPayload = cacheRoomInvitationPayload;

        }


        //public async Task<MessagePayload> MessagePayload(string messageId)
        //{
        //    if (messageId.IsNullOrEmpty())
        //    {
        //        throw new Exception("no message id");
        //    }

        //    // 1. Try to get from cache first
        //    var cachedPayload = MessagePaylaodCache(messageId);
        //    if (cachedPayload != null)
        //    {
        //        return cachedPayload;
        //    }

        //    // 2 . load 
        //    var rawPayload = await MessageRawPayload(messageId);
        //    var payload = await MessageRawPayloadParser(rawPayload);

        //    await _cacheMessagePayload.SetAsync(messageId, payload);

        //    return payload;
        //}


        //public async Task<string> MessageForward(string conversationId, string messageId)
        //{
        //    var newMsgId = string.Empty;

        //    var payload = await MessagePayload(messageId);

        //    switch (payload.Type)
        //    {
        //        case (int)MessageType.Attachment:     // Attach(6),
        //        case (int)MessageType.Audio:          // Audio(1), Voice(34)
        //        case (int)MessageType.Image:          // Img(2), Image(3)
        //        case (int)MessageType.Video:          // Video(4), Video(43)
        //            break;
        //        case (int)MessageType.Text:           // Text(1)
        //            if (!payload.Text.IsNullOrEmpty())
        //                newMsgId = MessageSendText(conversationId, payload.Text);
        //            else
        //            {
        //                //log.warn('Puppet', 'messageForward() payload.text is undefined.')
        //            }
        //            break;
        //        case (int)MessageType.MiniProgram:    // MiniProgram(33)
        //            newMsgId = await MessageSendMiniProgram(conversationId, MessageMiniProgram(messageId));

        //            break;
        //        case (int)MessageType.Url:            // Url(5)
        //            await MessageSendUrl(conversationId, await MessageUrl(messageId));
        //            break;
        //        case (int)MessageType.Contact:        // ShareCard(42)
        //            newMsgId = await MessageSendContact(conversationId, await MessageContact(messageId));
        //            break;
        //        case (int)MessageType.ChatHistory:    // ChatHistory(19)
        //        case (int)MessageType.Location:       // Location(48)
        //        case (int)MessageType.Emoticon:       // Sticker: Emoticon(15), Emoticon(47)
        //        case (int)MessageType.Transfer:
        //        case (int)MessageType.RedEnvelope:
        //        case (int)MessageType.Recalled:       // Recalled(10002)
        //                                              //throwUnsupportedError()
        //            throw new Exception("upsupported");

        //        case (int)MessageType.Unknown:
        //        default:
        //            // TODO  to set enum description
        //            throw new Exception("Unsupported forward message type:" + payload.Type);
        //            //throw new Exception("Unsupported forward message type:" + MessageType[payload.type])
        //    }
        //    return newMsgId;
        //}


    }
}
