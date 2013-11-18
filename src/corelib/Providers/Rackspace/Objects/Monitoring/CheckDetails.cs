namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;

    [JsonObject(MemberSerialization.OptIn)]
    public class CheckDetails
    {
        [JsonProperty("url")]
        private string _url;

        [JsonProperty("method")]
        [JsonConverter(typeof(StringEnumConverter))]
        private HttpMethod? _method;

        [JsonProperty("follow_redirects")]
        private bool? _followRedirects;

        [JsonProperty("include_body")]
        private bool? _includeBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckDetails()
        {
        }

        public CheckDetails(string url, HttpMethod? method, bool? followRedirects, bool? includeBody)
        {
            _url = url;
            _method = method;
            _followRedirects = followRedirects;
            _includeBody = includeBody;
        }

        public Uri Url
        {
            get
            {
                if (_url == null)
                    return null;

                return new Uri(_url);
            }
        }

        public HttpMethod? Method
        {
            get
            {
                return _method;
            }
        }

        public bool? FollowRedirects
        {
            get
            {
                return _followRedirects;
            }
        }

        public bool? IncludeBody
        {
            get
            {
                return _includeBody;
            }
        }
    }
}
