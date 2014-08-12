﻿namespace OpenStack.Services.Databases.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using ExtensibleJsonObject = OpenStack.ObjectModel.ExtensibleJsonObject;
    using Link = OpenStack.Services.Compute.V2.Link;

    /// <summary>
    /// This class models the JSON representation of a database instance flavor in the <see cref="IDatabaseService"/>.
    /// </summary>
    /// <seealso cref="IDatabaseService.ListFlavorsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseFlavor : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Id"/> property.
        /// </summary>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private FlavorId _id;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Link[] _links;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Memory"/> property.
        /// </summary>
        [JsonProperty("ram", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _ram;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseFlavor"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DatabaseFlavor()
        {
        }

        /// <summary>
        /// Gets the unique identifier for the flavor.
        /// </summary>
        public FlavorId Id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets a reference to the flavor in URI form.
        /// </summary>
        /// <remarks>
        /// The "flavorRef" is obtained from the <see cref="Links"/> property via the link
        /// with the <see cref="Link.Relation"/> property set to <c>self</c>.
        /// </remarks>
        public FlavorRef Href
        {
            get
            {
                if (_links == null)
                    return null;

                foreach (Link link in _links)
                {
                    if (string.Equals(link.Relation, "self", StringComparison.OrdinalIgnoreCase))
                        return new FlavorRef(link.Target.OriginalString);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="Link"/> objects describing resources associated
        /// with this flavor resource.
        /// </summary>
        public ReadOnlyCollection<Link> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<Link>(_links);
            }
        }

        /// <summary>
        /// Gets the name of the flavor.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets the amount of memory allocated to this flavor, in GB.
        /// </summary>
        public int? Memory
        {
            get
            {
                return _ram;
            }
        }
    }
}
