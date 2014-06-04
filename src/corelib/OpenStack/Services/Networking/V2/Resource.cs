namespace OpenStack.Services.Networking.V2
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using Link = OpenStack.Services.Compute.V2.Link;

    [JsonObject(MemberSerialization.OptIn)]
    public class Resource : ExtensibleJsonObject
    {
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _collection;

        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Resource()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Collection
        {
            get
            {
                return _collection;
            }
        }

        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }
    }
}
