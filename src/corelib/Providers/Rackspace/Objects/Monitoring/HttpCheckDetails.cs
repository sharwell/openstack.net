namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;

    [JsonObject(MemberSerialization.OptIn)]
    public class HttpCheckDetails : CheckDetails
    {
        [JsonProperty("url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _url;

        [JsonProperty("auth_password", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _authPassword;

        [JsonProperty("auth_user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _authUser;

        [JsonProperty("body", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _body;

        [JsonProperty("body_matches", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private object _bodyMatches;

        [JsonProperty("follow_redirects", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _followRedirects;

        [JsonProperty("headers", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _headers;

        [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        private HttpMethod? _method;

        [JsonProperty("payload", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _payload;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected HttpCheckDetails()
        {
        }

        public HttpCheckDetails(Uri url, string authUser, string authPassword, string body, object bodyMatches, bool? followRedirects, IDictionary<string, string> headers, HttpMethod? method, string payload)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            _url = url.ToString();
            _authUser = authUser;
            _authPassword = authPassword;
            _body = body;
            _bodyMatches = bodyMatches;
            _followRedirects = followRedirects;
            _headers = headers;
            _method = method;
            _payload = payload;
        }

        /// <summary>
        /// Gets the target URL.
        /// </summary>
        public Uri Url
        {
            get
            {
                if (_url == null)
                    return null;

                return new Uri(_url);
            }
        }

        /// <summary>
        /// Gets the password of the user to authenticate over HTTP.
        /// </summary>
        public string AuthPassword
        {
            get
            {
                return _authPassword;
            }
        }

        /// <summary>
        /// Gets the username of the user to authenticate over HTTP.
        /// </summary>
        public string AuthUser
        {
            get
            {
                return _authUser;
            }
        }

        /// <summary>
        /// Gets the regular expression to match against the body of the reply.
        /// </summary>
        public string Body
        {
            get
            {
                return _body;
            }
        }

        public object BodyMatches
        {
            get
            {
                return _bodyMatches;
            }
        }

        /// <summary>
        /// Gets a value indicating whether redirects should be followed.
        /// </summary>
        /// <value>
        /// <c>true</c> to follow redirects; otherwise, <c>false</c>.
        /// </value>
        public bool? FollowRedirects
        {
            get
            {
                return _followRedirects;
            }
        }

        /// <summary>
        /// Gets a collection of additional HTTP headers which are sent with the request.
        /// </summary>
        public ReadOnlyDictionary<string, string> Headers
        {
            get
            {
                if (_headers == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_headers);
            }
        }

        /// <summary>
        /// Gets the HTTP method to use for the request.
        /// </summary>
        public HttpMethod? Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Gets the body to send with the request.
        /// </summary>
        /// <remarks>
        /// If following a redirect, the payload will only be sent to the initial URI.
        /// </remarks>
        public string Payload
        {
            get
            {
                return _payload;
            }
        }

        /// <inheritdoc/>
        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteHttp;
        }
    }
}
