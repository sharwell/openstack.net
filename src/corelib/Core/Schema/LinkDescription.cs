namespace net.openstack.Core.Schema
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class LinkDescription
    {
        [JsonProperty("href", Required = Required.Always)]
        private string _href;

        [JsonProperty("rel", Required = Required.Always)]
        private string _rel;

        [JsonProperty("title")]
        private string _title;

        [JsonProperty("targetSchema")]
        private JsonSchema _targetSchema;

        [JsonProperty("mediaType")]
        private string _mediaType;

        [JsonProperty("method")]
        private string _method;

        [JsonProperty("encType")]
        private string _encType;

        [JsonProperty("schema")]
        private JsonSchema _schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkDescription"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LinkDescription()
        {
        }

        /// <summary>
        /// Gets a URI template, as defined by RFC 6570, with the addition of the $, (, and ) characters for preprocessing.
        /// </summary>
        public string Href
        {
            get
            {
                return _href;
            }
        }

        /// <summary>
        /// Gets the relation to the target resource of the link.
        /// </summary>
        public string Rel
        {
            get
            {
                return _rel;
            }
        }

        /// <summary>
        /// Gets the title for the link.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
        }

        /// <summary>
        /// Gets the JSON Schema describing the link target.
        /// </summary>
        public JsonSchema TargetSchema
        {
            get
            {
                return _targetSchema;
            }
        }

        /// <summary>
        /// Gets the media type (as defined by RFC 2046) describing the link target.
        /// </summary>
        public string MediaType
        {
            get
            {
                return _mediaType;
            }
        }

        /// <summary>
        /// Gets the method for requesting the target of the link (e.g. for HTTP this might be "GET" or "DELETE").
        /// </summary>
        public string Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Gets the media type in which to submit data along with the request.
        /// </summary>
        public string EncodingType
        {
            get
            {
                return _encType ?? "application/json";
            }
        }

        /// <summary>
        /// Gets the schema describing the data to submit along with the request.
        /// </summary>
        public JsonSchema Schema
        {
            get
            {
                return _schema;
            }
        }
    }
}
