namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the configurable properties of the JSON representation of
    /// a check resource in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="IMonitoringService.CreateCheckAsync"/>
    /// <see href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html">Checks (Rackspace Cloud Monitoring Developer Guide - API v1.0)</see>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class CheckConfiguration
    {
        /// <summary>
        /// This is the backing field for the <see cref="Label"/> property.
        /// </summary>
        [JsonProperty("label", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _label;

        /// <summary>
        /// This is the backing field for the <see cref="Type"/> property.
        /// </summary>
        [JsonProperty("type")]
        private CheckTypeId _type;

        /// <summary>
        /// This is the backing field for the <see cref="Details"/> property.
        /// </summary>
        [JsonProperty("details", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject _details;

        /// <summary>
        /// This is the backing field for the <see cref="MonitoringZonesPoll"/> property.
        /// </summary>
        [JsonProperty("monitoring_zones_poll", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private MonitoringZoneId[] _monitoringZonesPoll;

        /// <summary>
        /// This is the backing field for the <see cref="Timeout"/> property.
        /// </summary>
        [JsonProperty("timeout", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _timeout;

        /// <summary>
        /// This is the backing field for the <see cref="Period"/> property.
        /// </summary>
        [JsonProperty("period", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _period;

        /// <summary>
        /// This is the backing field for the <see cref="TargetAlias"/> property.
        /// </summary>
        [JsonProperty("target_alias", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _targetAlias;

        /// <summary>
        /// This is the backing field for the <see cref="TargetHostname"/> property.
        /// </summary>
        [JsonProperty("target_hostname", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _targetHostname;

        /// <summary>
        /// This is the backing field for the <see cref="ResolverType"/> property.
        /// </summary>
        [JsonProperty("target_resolver", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TargetResolverType _resolverType;

        /// <summary>
        /// This is the backing field for the <see cref="Metadata"/> property.
        /// </summary>
        [JsonProperty("metadata")]
        private IDictionary<string, string> _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected CheckConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckConfiguration"/> class
        /// with the specified properties.
        /// </summary>
        /// <param name="label">The friendly name of the check. If this is <c>null</c>, the label assigned to the new check is unspecified.</param>
        /// <param name="checkTypeId">The check type ID. This is obtained from <see cref="CheckType.Id">CheckType.Id</see>, or from the predefined values in <see cref="CheckTypeId"/>.</param>
        /// <param name="details">A <see cref="CheckDetails"/> object containing detailed configuration information for the specific check type.</param>
        /// <param name="monitoringZonesPoll">A collection of <see cref="MonitoringZoneId"/> objects identifying the monitoring zones to poll from.</param>
        /// <param name="timeout">The timeout of a check operation. If this value is <c>null</c>, a provider-specific default value is used.</param>
        /// <param name="period">The period between check operations. If this value is <c>null</c>, a provider-specific default value is used.</param>
        /// <param name="targetAlias">The alias of the target for this check in the associated entity's <see cref="EntityConfiguration.IPAddresses"/> map.</param>
        /// <param name="targetHostname">The hostname this check should target.</param>
        /// <param name="resolverType">The type of resolver to use for converting <paramref name="targetHostname"/> to an IP address.</param>
        /// <param name="metadata">A collection of metadata to associate with the check. If this parameter is <c>null</c>, the check is created without any custom metadata.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="checkTypeId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="details"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="label"/> is non-<c>null</c> but empty.
        /// <para>-or-</para>
        /// <para>If the specified <paramref name="details"/> object does support checks of type <paramref name="checkTypeId"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="checkTypeId"/> is a remote check (i.e. the <see cref="Monitoring.CheckTypeId.IsRemote"/> property is <c>true</c>) and <paramref name="monitoringZonesPoll"/> is <c>null</c> or empty.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="checkTypeId"/> is a remote check (i.e. the <see cref="Monitoring.CheckTypeId.IsRemote"/> property is <c>true</c>) and both <paramref name="targetAlias"/> and <paramref name="targetHostname"/> are <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="monitoringZonesPoll"/> contains any <c>null</c> values.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="period"/> is less than or equal to <paramref name="timeout"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="targetAlias"/> is non-<c>null</c> but empty.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="targetHostname"/> is non-<c>null</c> but empty.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> contains any empty keys, or any <c>null</c> values.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="timeout"/> is less than or equal to <see cref="TimeSpan.Zero"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="period"/> is less than or equal to <see cref="TimeSpan.Zero"/>.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If <paramref name="targetAlias"/> and <paramref name="targetHostname"/> are both non-<c>null</c>.
        /// </exception>
        public CheckConfiguration(string label, CheckTypeId checkTypeId, CheckDetails details, IEnumerable<MonitoringZoneId> monitoringZonesPoll, TimeSpan? timeout, TimeSpan? period, string targetAlias, string targetHostname, TargetResolverType resolverType, IDictionary<string, string> metadata)
        {
            if (checkTypeId == null)
                throw new ArgumentNullException("checkTypeId");
            if (details == null)
                throw new ArgumentNullException("details");
            if (label == string.Empty)
                throw new ArgumentException("label cannot be empty");
            if (!details.SupportsCheckType(checkTypeId))
                throw new ArgumentException(string.Format("The check details object does not support '{0}' checks.", checkTypeId), "details");
            if (checkTypeId.IsRemote && monitoringZonesPoll == null)
                throw new ArgumentException("monitoringZonesPoll cannot be null or empty for remote checks", "monitoringZonesPoll");
            if (checkTypeId.IsRemote && targetAlias == null && targetHostname == null)
                throw new ArgumentException("targetAlias and targetHostname cannot both be null for remote checks");
            if (timeout <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeout");
            if (period <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("period");
            if (period <= timeout)
                throw new ArgumentException("period cannot be less than or equal to timeout", "period");
            if (targetAlias != null && targetHostname != null)
                throw new InvalidOperationException("targetAlias and targetHostname cannot both be specified");

            _label = label;
            _type = checkTypeId;
            _details = JObject.FromObject(details);
            _monitoringZonesPoll = monitoringZonesPoll != null ? monitoringZonesPoll.ToArray() : null;
            if (checkTypeId.IsRemote && _monitoringZonesPoll.Length == 0)
                throw new ArgumentException("monitoringZonesPoll cannot be null or empty for remote checks", "monitoringZonesPoll");
            if (_monitoringZonesPoll != null && _monitoringZonesPoll.Contains(null))
                throw new ArgumentException("monitoringZonesPoll cannot contain any null values", "monitoringZonesPoll");

            _timeout = timeout.HasValue ? (int?)timeout.Value.TotalSeconds : null;
            _period = period.HasValue ? (int?)period.Value.TotalSeconds : null;
            _targetAlias = targetAlias;
            _targetHostname = targetHostname;
            _resolverType = resolverType;
            _metadata = metadata;
            if (_metadata != null)
            {
                if (_metadata.ContainsKey(string.Empty))
                    throw new ArgumentException("metadata cannot contain any empty keys", "metadata");
            }
        }

        /// <summary>
        /// Gets the friendly name of the check.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Gets the ID of the check type.
        /// </summary>
        public CheckTypeId CheckTypeId
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a <see cref="CheckDetails"/> object describing the detailed properties specific
        /// to this type of check.
        /// </summary>
        public CheckDetails Details
        {
            get
            {
                if (_details == null)
                    return null;

                return CheckDetails.FromJObject(CheckTypeId, _details);
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="MonitoringZoneId"/> objects identifying the monitoring
        /// zones to poll from.
        /// </summary>
        public ReadOnlyCollection<MonitoringZoneId> MonitoringZonesPoll
        {
            get
            {
                if (_monitoringZonesPoll == null)
                    return null;

                return new ReadOnlyCollection<MonitoringZoneId>(_monitoringZonesPoll);
            }
        }

        /// <summary>
        /// Gets the timeout for the check.
        /// </summary>
        public TimeSpan? Timeout
        {
            get
            {
                if (_timeout == null)
                    return null;

                return TimeSpan.FromSeconds(_timeout.Value);
            }
        }

        /// <summary>
        /// Gets the delay between check operations.
        /// </summary>
        public TimeSpan? Period
        {
            get
            {
                if (_period == null)
                    return null;

                return TimeSpan.FromSeconds(_period.Value);
            }
        }

        /// <summary>
        /// Gets the key for looking up the target for this check in the associated
        /// entity's <see cref="EntityConfiguration.IPAddresses"/> map.
        /// </summary>
        public string TargetAlias
        {
            get
            {
                return _targetAlias;
            }
        }

        /// <summary>
        /// Gets the target hostname this check should target.
        /// </summary>
        public string TargetHostname
        {
            get
            {
                return _targetHostname;
            }
        }

        /// <summary>
        /// Gets the type of resolver that should be used to convert the <see cref="TargetHostname"/>
        /// to an IP address.
        /// </summary>
        public TargetResolverType ResolverType
        {
            get
            {
                return _resolverType;
            }
        }

        /// <summary>
        /// Gets a collection of custom metadata associated with the check.
        /// </summary>
        public ReadOnlyDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_metadata);
            }
        }
    }
}
