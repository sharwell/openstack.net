namespace net.openstack.Core.Domain
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class AuthenticationRequirement
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("scheme")]
        private string _scheme;

        [JsonProperty("realms")]
        private string[] _realms;
#pragma warning restore 649

        public string Scheme
        {
            get
            {
                return _scheme;
            }
        }

        public ReadOnlyCollection<string> Realms
        {
            get
            {
                if (_realms == null)
                    return null;

                return new ReadOnlyCollection<string>(_realms);
            }
        }
    }
}
