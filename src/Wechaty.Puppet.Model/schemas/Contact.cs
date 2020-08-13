using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Wechaty.PuppetModel.Filter;

namespace Wechaty.PuppetModel
{

    public enum ContactGender
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
    }

    /**
     * Huan(202004) TODO: Lock the ENUM number (like protobuf) ?
     */
    public enum ContactType
    {
        Unknown = 0,
        Individual = 1,
        Official = 2,

        /**
         * Huan(202004):
         * @deprecated: use Individual instead
         */
        Personal = Individual,
    }

    public partial class ContactQueryFilter: IFilter
    {
        StringOrRegex? IFilter.this[string key]
        {
            get
            {
                switch (key)
                {
                    case nameof(Alias):
                        return Alias;
                    case nameof(Id):
                        return Id;
                    case nameof(Name):
                        return Name;
                    case nameof(Weixin):
                        return Weixin;
                    default:
                        throw new MissingMemberException(GetType().FullName, key);
                }
            }
        }

        private readonly ImmutableList<string> _keys = new List<string>
        {
            nameof(Alias),
            nameof(Id),
            nameof(Name),
            nameof(Weixin)
        }.ToImmutableList();

        public StringOrRegex this[string key] => throw new System.NotImplementedException();

        public IReadOnlyList<string> Keys => _keys;

        [JsonProperty("alias", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public StringOrRegex? Alias { get; set; }

        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public StringOrRegex? Name { get; set; }

        [JsonProperty("weixin", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Weixin { get; set; }

       
    }

    public partial class RegExp 
    {
        [JsonProperty("global", Required = Required.Always)]
        public bool Global { get; set; }

        [JsonProperty("ignoreCase", Required = Required.Always)]
        public bool IgnoreCase { get; set; }

        [JsonProperty("lastIndex", Required = Required.Always)]
        public double LastIndex { get; set; }

        [JsonProperty("multiline", Required = Required.Always)]
        public bool Multiline { get; set; }

        [JsonProperty("source", Required = Required.Always)]
        public string Source { get; set; }
    }

    public partial class ContactPayload 
    {
        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("alias", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("avatar", Required = Required.Always)]
        public string Avatar { get; set; }

        [JsonProperty("city", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("friend", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Friend { get; set; }

        [JsonProperty("gender", Required = Required.Always)]
        public ContactGender Gender { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("province", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Province { get; set; }

        [JsonProperty("signature", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Signature { get; set; }

        [JsonProperty("star", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Star { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public ContactType Type { get; set; }

        [JsonProperty("weixin", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Weixin { get; set; }
    }

    //public partial struct Alias 
    //{
    //    public RegExp RegExp;
    //    public string String;

    //    public static implicit operator Alias(RegExp RegExp) => new Alias { RegExp = RegExp };
    //    public static implicit operator Alias(string String) => new Alias { String = String };
    //}

    
   
}
