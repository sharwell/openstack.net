namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation of an HTTP API response containing an <see cref="V1.Deployment"/> object.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class DeploymentResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Deployment"/> property.
        /// </summary>
        [JsonProperty("software_deployment", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Deployment _softwareDeployment;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected DeploymentResponse()
        {
        }

        /// <summary>
        /// Gets the <see cref="V1.Deployment"/> object.
        /// </summary>
        /// <value>
        /// <para>The <see cref="V1.Deployment"/> object</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public Deployment Deployment
        {
            get
            {
                return _softwareDeployment;
            }
        }
    }
}
