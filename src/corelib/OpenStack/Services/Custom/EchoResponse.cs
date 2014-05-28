namespace OpenStack.Services.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using net.openstack.Core.Domain.Converters;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

#if PORTABLE
    using IPAddress = System.String;
#else
    using IPAddress = System.Net.IPAddress;
#endif

    /// <summary>
    /// This class models the JSON response returned by the "echo" API.
    /// </summary>
    /// <seealso cref="IEchoService.PrepareEchoAsync"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class EchoResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <seealso cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("args", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Dictionary<string, string> _args;

        /// <summary>
        /// This is the backing field for the <seealso cref="Headers"/> property.
        /// </summary>
        [JsonProperty("headers", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Dictionary<string, string> _headers;

        /// <summary>
        /// This is the backing field for the <seealso cref="Origin"/> property.
        /// </summary>
        [JsonProperty("origin", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(IPAddressSimpleConverter))]
        private IPAddress _origin;

        /// <summary>
        /// This is the backing field for the <seealso cref="Uri"/> property.
        /// </summary>
        [JsonProperty("url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _uri;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EchoResponse()
        {
        }

        /// <summary>
        /// Gets a copy of the query parameters included in the HTTP request.
        /// </summary>
        /// <value>
        /// A copy of the query parameters passed in the HTTP request.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the HTTP response did not include the underlying JSON property.</para>
        /// </value>
        public ReadOnlyDictionary<string, string> Arguments
        {
            get
            {
                if (_args == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_args);
            }
        }

        /// <summary>
        /// Gets a copy of the headers included in the HTTP request.
        /// </summary>
        /// <value>
        /// A copy of the headers passed in the HTTP request.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the HTTP response did not include the underlying JSON property.</para>
        /// </value>
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
        /// Gets the source address from which the HTTP request was received.
        /// </summary>
        /// <value>
        /// The source address from which the HTTP request was received.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the HTTP response did not include the underlying JSON property.</para>
        /// </value>
        public IPAddress Origin
        {
            get
            {
                return _origin;
            }
        }

        /// <summary>
        /// Gets the URI which was requested.
        /// </summary>
        /// <value>
        /// The URI which was requested.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the HTTP response did not include the underlying JSON property.</para>
        /// </value>
        public Uri Uri
        {
            get
            {
                if (_uri == null)
                    return null;

                return new Uri(_uri, UriKind.RelativeOrAbsolute);
            }
        }
    }
}