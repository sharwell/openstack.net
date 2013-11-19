namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;

    [JsonObject(MemberSerialization.OptIn)]
    public class Audit
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("id")]
        private AuditId _id;

        [JsonProperty("timestamp")]
        private long? _timestamp;

        [JsonProperty("headers")]
        private Dictionary<string, string> _headers;

        [JsonProperty("url")]
        private string _url;

        [JsonProperty("app")]
        private string _app;

        [JsonProperty("query")]
        private Dictionary<string, string> _query;

        [JsonProperty("txnId")]
        private TransactionId _transactionId;

        [JsonProperty("payload")]
        private string _payload;

        [JsonProperty("method")]
        [JsonConverter(typeof(StringEnumConverter))]
        private HttpMethod _method;

        [JsonProperty("account_id")]
        private ProjectId _accountId;

        [JsonProperty("who")]
        private string _who;

        [JsonProperty("why")]
        private string _why;

        [JsonProperty("statusCode")]
        private int _statusCode;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Audit"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Audit()
        {
        }

        public AuditId Id
        {
            get
            {
                return _id;
            }
        }

        public DateTimeOffset? Timestamp
        {
            get
            {
                return DateTimeOffsetExtensions.ToDateTimeOffset(_timestamp);
            }
        }

        public ReadOnlyDictionary<string, string> Headers
        {
            get
            {
                if (_headers == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_headers);
            }
        }

        public Uri Url
        {
            get
            {
                if (_url == null)
                    return null;

                return new Uri(_url, UriKind.RelativeOrAbsolute);
            }
        }

        public string App
        {
            get
            {
                return _app;
            }
        }

        /// <summary>
        /// Gets a collection of query string parameters decoded from <see cref="Url"/>.
        /// </summary>
        public ReadOnlyDictionary<string, string> Query
        {
            get
            {
                if (_query == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_query);
            }
        }

        public TransactionId TransactionId
        {
            get
            {
                return _transactionId;
            }
        }

        public string Payload
        {
            get
            {
                return _payload;
            }
        }

        public HttpMethod Method
        {
            get
            {
                return _method;
            }
        }

        public ProjectId AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public string Who
        {
            get
            {
                return _who;
            }
        }

        public string Why
        {
            get
            {
                return _why;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return (HttpStatusCode)_statusCode;
            }
        }
    }
}
