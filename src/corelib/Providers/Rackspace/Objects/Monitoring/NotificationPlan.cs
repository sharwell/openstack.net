namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class NotificationPlan
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("label")]
        private NotificationPlanId _label;

        [JsonProperty("critical_state")]
        private NotificationId[] _criticalState;

        [JsonProperty("warning_state")]
        private NotificationId[] _warningState;

        [JsonProperty("ok_state")]
        private NotificationId[] _okState;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPlan"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected NotificationPlan()
        {
        }

        public NotificationPlanId Label
        {
            get
            {
                return _label;
            }
        }

        public ReadOnlyCollection<NotificationId> CriticalState
        {
            get
            {
                if (_criticalState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_criticalState);
            }
        }

        public ReadOnlyCollection<NotificationId> WarningState
        {
            get
            {
                if (_warningState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_warningState);
            }
        }

        public ReadOnlyCollection<NotificationId> OkState
        {
            get
            {
                if (_okState == null)
                    return null;

                return new ReadOnlyCollection<NotificationId>(_okState);
            }
        }
    }
}
