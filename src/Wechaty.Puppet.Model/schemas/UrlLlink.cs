using Newtonsoft.Json;

namespace Wechaty.PuppetModel
{
    public partial class UrlLinkPayload
    {
        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("thumbnailUrl", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }
    }
}
