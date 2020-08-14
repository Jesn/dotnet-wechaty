using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wechaty.PuppetModel
{
    public enum ScanStatus
    {
        Unknown = 0,
        Cancel = 1,
        Waiting = 2,
        Scanned = 3,
        Confirmed = 4,
        Timeout = 5,
    }


    public partial class EventFriendshipPayload: TEvent
    {
        [JsonProperty("friendshipId")]
        public string FriendshipId { get; set; }
    }

    public partial class EventLoginPayload: TEvent
    {
        [JsonProperty("contactId")]
        public string ContactId { get; set; }
    }

    public partial class EventLogoutPayload: TEvent
    {
        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventMessagePayload: TEvent
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
    }

    public partial class EventRoomInvitePayload: TEvent
    {
        [JsonProperty("roomInvitationId")]
        public string RoomInvitationId { get; set; }
    }

    public partial class EventRoomJoinPayload: TEvent
    {
        [JsonProperty("inviteeIdList")]
        public List<string> InviteeIdList { get; set; }

        [JsonProperty("inviterId")]
        public string InviterId { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }

        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }
    }

    public partial class EventRoomLeavePayload: TEvent
    {
        [JsonProperty("removeeIdList")]
        public List<string> RemoveeIdList { get; set; }

        [JsonProperty("removerId")]
        public string RemoverId { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }

        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }
    }

    public partial class EventRoomTopicPayload: TEvent
    {
        [JsonProperty("changerId")]
        public string ChangerId { get; set; }

        [JsonProperty("newTopic")]
        public string NewTopic { get; set; }

        [JsonProperty("oldTopic")]
        public string OldTopic { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }

        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }
    }

    public partial class EventScanPayload : TEvent
    {
        [JsonProperty("data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        [JsonProperty("qrcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Qrcode { get; set; }

        [JsonProperty("status")]
        public ScanStatus Status { get; set; }
    }

    public partial class EventDongPayload : TEvent
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventErrorPayload : TEvent
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventReadyPayload : TEvent
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventResetPayload : TEvent
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventHeartbeatPayload : TEvent
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }

    public partial class EventAllPayload : TEvent
    {
        [JsonProperty("friendshipId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FriendshipId { get; set; }

        [JsonProperty("contactId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ContactId { get; set; }

        [JsonProperty("data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        [JsonProperty("messageId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MessageId { get; set; }

        [JsonProperty("roomInvitationId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomInvitationId { get; set; }

        [JsonProperty("inviteeIdList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> InviteeIdList { get; set; }

        [JsonProperty("inviterId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InviterId { get; set; }

        [JsonProperty("roomId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomId { get; set; }

        [JsonProperty("timestamp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Timestamp { get; set; }

        [JsonProperty("removeeIdList", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RemoveeIdList { get; set; }

        [JsonProperty("removerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RemoverId { get; set; }

        [JsonProperty("changerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChangerId { get; set; }

        [JsonProperty("newTopic", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NewTopic { get; set; }

        [JsonProperty("oldTopic", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string OldTopic { get; set; }

        [JsonProperty("qrcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Qrcode { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double? Status { get; set; }
    }



}
