namespace Rackspace.Services.AutoScale.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;


    /// <summary>
    /// This class extends the <see cref="LaunchConfiguration"/> class with
    /// strongly-typed launch arguments.
    /// </summary>
    /// <typeparam name="TArguments">The type modeling the arguments for a launch configuration.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class LaunchConfiguration<TArguments> : LaunchConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Arguments"/> property.
        /// </summary>
        [JsonProperty("args", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TArguments _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration{TArguments}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LaunchConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration{TArguments}"/> class
        /// with the specified launch type and arguments.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        /// <param name="arguments">The arguments for launching a server.</param>
        protected LaunchConfiguration(LaunchType launchType, TArguments arguments)
            : base(launchType)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration{TArguments}"/> class
        /// with the specified launch type, arguments, and extension data.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        /// <param name="arguments">The arguments for launching a server.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        protected LaunchConfiguration(LaunchType launchType, TArguments arguments, params JProperty[] extensionData)
            : base(launchType, extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration{TArguments}"/> class
        /// with the specified launch type, arguments, and extension data.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        /// <param name="arguments">The arguments for launching a server.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        protected LaunchConfiguration(LaunchType launchType, TArguments arguments, IDictionary<string, JToken> extensionData)
            : base(launchType, extensionData)
        {
            _arguments = arguments;
        }

        /// <summary>
        /// Gets the launch arguments.
        /// </summary>
        public TArguments Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
