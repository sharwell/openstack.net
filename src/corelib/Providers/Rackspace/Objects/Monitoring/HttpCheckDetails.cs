﻿namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;

    /// <summary>
    /// This class represents the detailed configuration parameters for a
    /// <see cref="CheckTypeId.RemoteHttp"/> check.
    /// </summary>
    /// <seealso cref="CheckTypeId.RemoteHttp"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class HttpCheckDetails : CheckDetails
    {
        /// <summary>
        /// This is the backing field for the <see cref="Url"/> property.
        /// </summary>
        [JsonProperty("url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _url;

        /// <summary>
        /// This is the backing field for the <see cref="AuthPassword"/> property.
        /// </summary>
        [JsonProperty("auth_password", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _authPassword;

        /// <summary>
        /// This is the backing field for the <see cref="AuthUser"/> property.
        /// </summary>
        [JsonProperty("auth_user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _authUser;

        /// <summary>
        /// This is the backing field for the <see cref="Body"/> property.
        /// </summary>
        [JsonProperty("body", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _body;

        /// <summary>
        /// This is the backing field for the <see cref="BodyMatches"/> property.
        /// </summary>
        [JsonProperty("body_matches", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _bodyMatches;

        /// <summary>
        /// This is the backing field for the <see cref="FollowRedirects"/> property.
        /// </summary>
        [JsonProperty("follow_redirects", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _followRedirects;

        /// <summary>
        /// This is the backing field for the <see cref="Headers"/> property.
        /// </summary>
        [JsonProperty("headers", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _headers;

        /// <summary>
        /// This is the backing field for the <see cref="Method"/> property.
        /// </summary>
        [JsonProperty("method", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        private HttpMethod? _method;

        /// <summary>
        /// This is the backing field for the <see cref="Payload"/> property.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCheckDetails"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="url">The target URI.</param>
        /// <param name="authUser">The username of the user to authenticate over HTTP.</param>
        /// <param name="authPassword">The password of the user to authenticate over HTTP.</param>
        /// <param name="body">The regular expression to match against the body of the reply.</param>
        /// <param name="bodyMatches">A collection of named regular expressions to match against the body of the reply.</param>
        /// <param name="followRedirects"><c>true</c> to follow redirects; otherwise, <c>false</c>.</param>
        /// <param name="headers">A collection of additional HTTP headers which are sent with the request.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="payload">The body to include with the HTTP request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="url"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="bodyMatches"/> contains any <c>null</c> or empty keys.
        /// <para>-or-</para>
        /// <para>If <paramref name="headers"/> contains any <c>null</c> or empty keys.</para>
        /// </exception>
        public HttpCheckDetails(Uri url, string authUser, string authPassword, string body, IDictionary<string, string> bodyMatches, bool? followRedirects, IDictionary<string, string> headers, HttpMethod? method, string payload)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (bodyMatches != null && (bodyMatches.ContainsKey(null) || bodyMatches.ContainsKey(string.Empty)))
                throw new ArgumentException("bodyMatches cannot contain any null or empty keys", "bodyMatches");
            if (headers != null && (headers.ContainsKey(null) || headers.ContainsKey(string.Empty)))
                throw new ArgumentException("headers cannot contain any null or empty keys", "headers");

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
        /// Gets the target URI.
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

        /// <summary>
        /// Gets a collection of named regular expressions to match against the
        /// body of the reply.
        /// </summary>
        public ReadOnlyDictionary<string, string> BodyMatches
        {
            get
            {
                if (_bodyMatches == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_bodyMatches);
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
        /// <remarks>
        /// This class only supports <see cref="CheckTypeId.RemoteHttp"/> checks.
        /// </remarks>
        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteHttp;
        }
    }
}
