namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class EntityOverview
    {
        [JsonProperty("entity")]
        private Entity _entity;

        [JsonProperty("checks")]
        private Check[] _checks;

        [JsonProperty("alarms")]
        private Alarm[] _alarms;

        [JsonProperty("latest_alarm_states")]
        private AlarmState[] _latestAlarmStates;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityOverview"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected EntityOverview()
        {
        }

        public Entity Entity
        {
            get
            {
                return _entity;
            }
        }

        public ReadOnlyCollection<Check> Checks
        {
            get
            {
                if (_checks == null)
                    return null;

                return new ReadOnlyCollection<Check>(_checks);
            }
        }

        public ReadOnlyCollection<Alarm> Alarms
        {
            get
            {
                if (_alarms == null)
                    return null;

                return new ReadOnlyCollection<Alarm>(_alarms);
            }
        }

        public ReadOnlyCollection<AlarmState> LatestAlarmStates
        {
            get
            {
                if (_latestAlarmStates == null)
                    return null;

                return new ReadOnlyCollection<AlarmState>(_latestAlarmStates);
            }
        }
    }
}
