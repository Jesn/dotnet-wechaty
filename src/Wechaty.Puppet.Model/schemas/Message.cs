using Newtonsoft.Json;
using System.Collections.Generic;


namespace Wechaty.PuppetModel
{

    public enum MessageType
    {
        Unknown = 0,

        Attachment,     // Attach(6),
        Audio,          // Audio(1), Voice(34)
        Contact,        // ShareCard(42)
        ChatHistory,    // ChatHistory(19)
        Emoticon,       // Sticker: Emoticon(15), Emoticon(47)
        Image,          // Img(2), Image(3)
        Text,           // Text(1)
        Location,       // Location(48)
        MiniProgram,    // MiniProgram(33)
        GroupNote,      // GroupNote(53)
        Transfer,       // Transfers(2000)
        RedEnvelope,    // RedEnvelopes(2001)
        Recalled,       // Recalled(10002)
        Url,            // Url(5)
        Video,          // Video(4), Video(43)
    }

    /**
     * Huan(202001): Wechat Server Message Type Value (to be confirmed.)
     */
    public enum WechatAppMessageType
    {
        Text = 1,
        Img = 2,
        Audio = 3,
        Video = 4,
        Url = 5,
        Attach = 6,
        Open = 7,
        Emoji = 8,
        VoiceRemind = 9,
        ScanGood = 10,
        Good = 13,
        Emotion = 15,
        CardTicket = 16,
        RealtimeShareLocation = 17,
        ChatHistory = 19,
        MiniProgram = 33,
        Transfers = 2000,
        RedEnvelopes = 2001,
        ReaderType = 100001,
    }

    /**
     * Wechat Server Message Type Value (to be confirmed)
     *  Huan(202001): The Windows(PC) DLL match the following numbers.
     */
    public enum WechatMessageType
    {
        Text = 1,
        Image = 3,
        Voice = 34,
        VerifyMsg = 37,
        PossibleFriendMsg = 40,
        ShareCard = 42,
        Video = 43,
        Emoticon = 47,
        Location = 48,
        App = 49,
        VoipMsg = 50,
        StatusNotify = 51,
        VoipNotify = 52,
        VoipInvite = 53,
        MicroVideo = 62,
        Transfer = 2000, // 转账
        RedEnvelope = 2001, // 红包
        MiniProgram = 2002, // 小程序
        GroupInvite = 2003, // 群邀请
        File = 2004, // 文件消息
        SysNotice = 9999,
        Sys = 10000,
        Recalled = 10002,  // NOTIFY 服务通知
    }

    public partial class MessagePayloadBase
    {
        [JsonProperty("filename", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public double Type { get; set; }
    }

    public partial class MessagePayloadRoom
    {
        [JsonProperty("fromId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FromId { get; set; }

        [JsonProperty("mentionIdList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MentionIdList { get; set; }

        [JsonProperty("roomId", Required = Required.Always)]
        public string RoomId { get; set; }

        [JsonProperty("toId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ToId { get; set; }
    }

    public partial class MessagePayloadTo
    {
        [JsonProperty("fromId", Required = Required.Always)]
        public string FromId { get; set; }

        [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomId { get; set; }

        [JsonProperty("toId", Required = Required.Always)]
        public string ToId { get; set; }
    }

    public partial class MessagePayload
    {
        [JsonProperty("filename", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public MessageType Type { get; set; }

        [JsonProperty("fromId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FromId { get; set; }

        [JsonProperty("mentionIdList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MentionIdList { get; set; }

        [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomId { get; set; }

        [JsonProperty("toId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ToId { get; set; }
    }

    public partial class MessageQueryFilter
    {
        [JsonProperty("fromId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FromId { get; set; }

        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomId { get; set; }

        [JsonProperty("text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Text? Text { get; set; }

        [JsonProperty("toId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ToId { get; set; }

        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Type { get; set; }
    }

    public partial struct Text
    {
        public RegExp RegExp;
        public string String;

        public static implicit operator Text(RegExp RegExp) => new Text { RegExp = RegExp };
        public static implicit operator Text(string String) => new Text { String = String };
    }




}
