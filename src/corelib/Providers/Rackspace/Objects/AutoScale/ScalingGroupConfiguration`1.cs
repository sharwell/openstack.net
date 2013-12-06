namespace net.openstack.Providers.Rackspace.Objects.AutoScale
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ScalingGroupConfiguration<TPolicyConfiguration>
        where TPolicyConfiguration : PolicyConfiguration
    {
        [JsonProperty("groupConfiguration")]
        private GroupConfiguration _groupConfiguration;

        [JsonProperty("launchConfiguration")]
        private JObject _launchConfiguration;

        [JsonProperty("scalingPolicies")]
        private TPolicyConfiguration[] _scalingPolicies;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingGroupConfiguration{TPolicyConfiguration}"/>
        /// class during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ScalingGroupConfiguration()
        {
        }

        protected ScalingGroupConfiguration(GroupConfiguration groupConfiguration, LaunchConfiguration launchConfiguration, IEnumerable<TPolicyConfiguration> scalingPolicies)
        {
            if (groupConfiguration == null)
                throw new ArgumentNullException("groupConfiguration");
            if (launchConfiguration == null)
                throw new ArgumentNullException("launchConfiguration");
            if (scalingPolicies == null)
                throw new ArgumentNullException("scalingPolicies");
            if (scalingPolicies.Contains(null))
                throw new ArgumentException("scalingPolicies cannot contain any null values", "scalingPolicies");

            _groupConfiguration = groupConfiguration;
            _launchConfiguration = JObject.FromObject(launchConfiguration);
            _scalingPolicies = scalingPolicies.ToArray();
        }

        public GroupConfiguration GroupConfiguration
        {
            get
            {
                return _groupConfiguration;
            }
        }

        public LaunchConfiguration LaunchConfiguration
        {
            get
            {
                if (_launchConfiguration == null)
                    return null;

                return LaunchConfiguration.FromJObject(_launchConfiguration);
            }
        }

        public ReadOnlyCollection<TPolicyConfiguration> ScalingPolicies
        {
            get
            {
                if (_scalingPolicies == null)
                    return null;

                return new ReadOnlyCollection<TPolicyConfiguration>(_scalingPolicies);
            }
        }
    }
}
