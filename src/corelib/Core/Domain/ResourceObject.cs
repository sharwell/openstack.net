namespace net.openstack.Core.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the Resource Object of the home document described by
    /// <strong>Home Documents for HTTP APIs</strong>.
    /// </summary>
    /// <seealso href="http://tools.ietf.org/html/draft-nottingham-json-home-02#section-3">Resource Objects (Home Documents for HTTP APIs - draft-nottingham-json-home-02)</seealso>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _href;

        [JsonProperty("href-template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _hrefTemplate;

        [JsonProperty("href-vars", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Dictionary<string, string> _hrefVars;

        [JsonProperty("hints")]
        private ResourceHints _hints;
#pragma warning restore 649

        public string Href
        {
            get
            {
                return _href;
            }
        }

        public string HrefTemplate
        {
            get
            {
                return _hrefTemplate;
            }
        }

        public IDictionary<string, string> HrefVars
        {
            get
            {
                return _hrefVars;
            }
        }

        public ResourceHints Hints
        {
            get
            {
                return _hints;
            }
        }
    }
}
