namespace Rackspace.Services.AutoScale.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;
    using Link = OpenStack.Services.Compute.V2.Link;

    [JsonObject(MemberSerialization.OptIn)]
    public class Policy : PolicyData
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private PolicyId _id;

        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Policy()
        {
        }

        public PolicyId Id
        {
            get
            {
                return _id;
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
