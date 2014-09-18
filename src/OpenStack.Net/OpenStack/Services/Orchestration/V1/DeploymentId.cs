namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// Represents the unique identifier of a <see cref="Deployment"/> resource in the OpenStack Orchestration Service.
    /// </summary>
    /// <seealso cref="Deployment.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(DeploymentId.Converter))]
    public sealed class DeploymentId : ResourceIdentifier<DeploymentId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public DeploymentId(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="DeploymentId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        /// <preliminary/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override DeploymentId FromValue(string id)
            {
                return new DeploymentId(id);
            }
        }
    }
}
