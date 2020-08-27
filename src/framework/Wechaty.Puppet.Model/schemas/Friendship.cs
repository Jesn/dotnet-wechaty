using Newtonsoft.Json;

namespace Wechaty.PuppetModel
{
    public enum FriendshipType
    {
        Unknown = 0,
        Confirm,
        Receive,
        Verify,
    }

    /**
     * Huan(202002): Does those numbers are the underlying Wechat Protocol Data Values?
     */
    public enum FriendshipSceneType
    {
        Unknown = 0,   // Huan(202003) added by myself
        QQ = 1,    // FIXME: Huan(202002) in Wechat PC, QQ = 12.
        Email = 2,
        Weixin = 3,
        QQtbd = 12,   // FIXME: confirm the two QQ number QQ号搜索
        Room = 14,
        Phone = 15,
        Card = 17,   // 名片分享
        Location = 18,
        Bottle = 25,
        Shaking = 29,
        QRCode = 30,
    }


    public partial class FriendshipPayloadBase
    {
        [JsonProperty("contactId", Required = Required.Always)]
        public string ContactId { get; set; }

        [JsonProperty("hello", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Hello { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }
    }

    public partial class FriendshipPayloadConfirm
    {
        [JsonProperty("contactId", Required = Required.Always)]
        public string ContactId { get; set; }

        [JsonProperty("hello", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Hello { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public double Type { get; set; }
    }

    public partial class FriendshipPayloadReceive
    {
        [JsonProperty("contactId", Required = Required.Always)]
        public string ContactId { get; set; }

        [JsonProperty("hello", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Hello { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        /// <summary>
        /// Huan(202002): Does those numbers are the underlying Wechat Protocol Data Values?
        /// </summary>
        [JsonProperty("scene", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Scene { get; set; }

        [JsonProperty("stranger", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Stranger { get; set; }

        [JsonProperty("ticket", Required = Required.Always)]
        public string Ticket { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public double Type { get; set; }
    }

    public partial class FriendshipPayloadVerify
    {
        [JsonProperty("contactId", Required = Required.Always)]
        public string ContactId { get; set; }

        [JsonProperty("hello", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Hello { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public double Type { get; set; }
    }

    public partial class FriendshipPayload
    {
        [JsonProperty("contactId", Required = Required.Always)]
        public string ContactId { get; set; }

        [JsonProperty("hello", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Hello { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public double Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public FriendshipType Type { get; set; }

        /// <summary>
        /// Huan(202002): Does those numbers are the underlying Wechat Protocol Data Values?
        /// </summary>
        [JsonProperty("scene", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Scene { get; set; }

        [JsonProperty("stranger", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Stranger { get; set; }

        [JsonProperty("ticket", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Ticket { get; set; }
    }

    public partial class FriendshipSearchCondition
    {
        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }

        [JsonProperty("weixin", Required = Required.Always)]
        public string Weixin { get; set; }
    }



}
