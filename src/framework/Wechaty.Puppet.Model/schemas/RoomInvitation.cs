using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wechaty.PuppetModel
{
    public partial class RoomInvitationPayload
    {
        [JsonProperty("avatar", Required = Required.Always)]
        public string Avatar { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("invitation", Required = Required.Always)]
        public string Invitation { get; set; }

        [JsonProperty("inviterId", Required = Required.Always)]
        public string InviterId { get; set; }

        [JsonProperty("memberCount", Required = Required.Always)]
        public double MemberCount { get; set; }

        [JsonProperty("memberIdList", Required = Required.Always)]
        public List<string> MemberIdList { get; set; }

        [JsonProperty("receiverId", Required = Required.Always)]
        public string ReceiverId { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public long Timestamp { get; set; }

        [JsonProperty("topic", Required = Required.Always)]
        public string Topic { get; set; }
    }

}
