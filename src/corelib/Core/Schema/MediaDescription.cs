namespace net.openstack.Core.Schema
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class MediaDescription
    {
        [JsonProperty("type")]
        private string _type;

        [JsonProperty("binaryEncoding")]
        private string _binaryEncoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDescription"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected MediaDescription()
        {
        }

        /// <summary>
        /// Gets a media type, as described in RFC 2046.
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a content encoding schema, as described in RFC 2045.
        /// </summary>
        public string BinaryEncoding
        {
            get
            {
                return _binaryEncoding;
            }
        }
    }
}
