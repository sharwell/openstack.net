namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;
    using INetworkingService = OpenStack.Services.Networking.V2.INetworkingService;

    /// <summary>
    /// This class models the JSON representation of an extension resource in
    /// the <see cref="IComputeService"/> or <see cref="INetworkingService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Extension : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Alias"/> property.
        /// </summary>
        [JsonProperty("alias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ExtensionAlias _alias;

        /// <summary>
        /// This is the backing field for the <see cref="Name"/> property.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _name;

        /// <summary>
        /// This is the backing field for the <see cref="Description"/> property.
        /// </summary>
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _description;

        /// <summary>
        /// This is the backing field for the <see cref="Namespace"/> property.
        /// </summary>
        [JsonProperty("namespace", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _namespace;

        /// <summary>
        /// This is the backing field for the <see cref="LastModified"/> property.
        /// </summary>
        [JsonProperty("updated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private DateTimeOffset? _updated;

        /// <summary>
        /// This is the backing field for the <see cref="Links"/> property.
        /// </summary>
        [JsonProperty("links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken[] _links; // seriously, what is this?
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="Extension"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Extension()
        {
        }

        /// <summary>
        /// Gets the alias which serves as a unique identifier for the extension.
        /// </summary>
        /// <value>
        /// The alias for the extension.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public ExtensionAlias Alias
        {
            get
            {
                return _alias;
            }
        }

        /// <summary>
        /// Gets the name of the extension.
        /// </summary>
        /// <value>
        /// The name of the extension.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets a description of the extension.
        /// </summary>
        /// <value>
        /// A description of the extension.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }
        }

        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        /// <summary>
        /// Gets a timestamp indicating when the extension resource was last modified.
        /// </summary>
        /// <value>
        /// A timestamp indicating when the extension resource was last modified.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public DateTimeOffset? LastModified
        {
            get
            {
                return _updated;
            }
        }

        public ReadOnlyCollection<JToken> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<JToken>(_links);
            }
        }
    }
}
