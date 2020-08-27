using Newtonsoft.Json;

namespace Wechaty.PuppetModel
{
    public partial class MiniProgramPayload
    {
        [JsonProperty("appid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Appid { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("iconUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string IconUrl { get; set; }

        [JsonProperty("pagePath", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PagePath { get; set; }

        [JsonProperty("shareId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ShareId { get; set; }

        [JsonProperty("thumbKey", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbKey { get; set; }

        [JsonProperty("thumbUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbUrl { get; set; }

        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("username", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
    }
}
