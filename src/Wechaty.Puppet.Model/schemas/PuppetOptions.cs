using Newtonsoft.Json;

namespace Wechaty.PuppetModel
{

    public partial class PuppetOptions
    {
        [JsonProperty("endpoint", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EndPoint { get; set; }

        [JsonProperty("timeout", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int Timeout { get; set; }

        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

}
