namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class TestAlarmConfiguration
    {
        [JsonProperty("criteria")]
        private string _criteria;

        [JsonProperty("check_data")]
        private CheckData[] _checkData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestAlarmConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected TestAlarmConfiguration()
        {
        }

        public TestAlarmConfiguration(string criteria, IEnumerable<CheckData> checkData)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            if (string.IsNullOrEmpty(criteria))
                throw new ArgumentException("criteria cannot be empty");
            if (checkData == null)
                throw new ArgumentNullException("checkData");
            if (checkData.Contains(null))
                throw new ArgumentException("checkData cannot contain any null values", "checkData");

            _criteria = criteria;
            _checkData = checkData.ToArray();
        }
    }
}
