using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wechaty.PuppetModel
{
    public partial class RoomMemberQueryFilter
    {
        [JsonProperty("contactAlias", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ContactAlias { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("roomAlias", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomAlias { get; set; }
    }

    public partial class RoomQueryFilter
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("topic", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Topic { get; set; }
    }

    
    public partial class RoomPayload
    {
        [JsonProperty("adminIdList", Required = Required.Always)]
        public List<string> AdminIdList { get; set; }

        [JsonProperty("avatar", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("memberIdList", Required = Required.Always)]
        public List<string> MemberIdList { get; set; }

        [JsonProperty("ownerId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerId { get; set; }

        [JsonProperty("topic", Required = Required.Always)]
        public string Topic { get; set; }
    }

    public partial class RoomMemberPayload
    {
        [JsonProperty("avatar", Required = Required.Always)]
        public string Avatar { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("inviterId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InviterId { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("roomAlias", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string RoomAlias { get; set; }
    }


}
