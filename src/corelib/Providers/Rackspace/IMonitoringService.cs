﻿namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using net.openstack.Providers.Rackspace.Objects.Monitoring;
    using Newtonsoft.Json.Linq;
    using CancellationToken = System.Threading.CancellationToken;

    /// <summary>
    /// Represents a provider for the Rackspace Cloud Monitoring service.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/overview.html">Rackspace Cloud Monitoring Developer Guide - API v1.0</seealso>
    /// <preliminary/>
    public interface IMonitoringService
    {
        #region Core

        #region Account

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-root">Get Account (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<MonitoringAccount> GetAccountAsync(CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-put-account">Update Account (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAccountAsync(MonitoringAccountId accountId, AccountConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-get-limits">Get Limits (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<MonitoringLimits> GetLimitsAsync(CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-list-audits">List Audits (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Audit, AuditId>> ListAuditsAsync(AuditId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        #endregion Account

        #region Entities

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-create">Create Entities (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<EntityId> CreateEntityAsync(EntityConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-list">List Entities (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Entity, EntityId>> ListEntitiesAsync(EntityId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-get">Get Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Entity> GetEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-update">Update Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateEntityAsync(EntityId entityId, UpdateEntityConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-delete">Delete Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        #endregion Entities

        #region Checks

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-create">Create Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckId> CreateCheckAsync(EntityId entityId, CheckConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test">Test Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test-debug">Test Check and Include Debug Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckData[]> TestCheckAsync(EntityId entityId, CheckConfiguration configuration, bool? debug, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test-existing">Test Existing Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckData[]> TestExistingCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-list">List Checks (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Check, CheckId>> ListChecksAsync(EntityId entityId, CheckId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-get">Get Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Check> GetCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-update">Update Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateCheckAsync(EntityId entityId, CheckId checkId, UpdateCheckConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-delete">Delete Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        #endregion Checks

        #region Check Types

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-check-types.html#service-check-types-list">List Check Types (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<CheckType, CheckTypeId>> ListCheckTypesAsync(CheckTypeId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-check-types.html#service-check-types-get">Get Check Type (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckType> GetCheckTypeAsync(CheckTypeId checkTypeId, CancellationToken cancellationToken);

        #endregion Check Types

        #region Metrics

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/metrics-api.html#list-metrics">List Metrics (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Metric, MetricName>> ListMetricsAsync(EntityId entityId, CheckId checkId, MetricName marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/metrics-api.html#fetch-data-points">Fetch Data Points (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<DataPoint, EntityId>> GetDataPointsAsync(EntityId entityId, CheckId checkId, MetricName metricName, int? points, DataPointGranularity resolution, IEnumerable<DataPointStatistic> select, CancellationToken cancellationToken);

        #endregion Metrics

        #region Alarms

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-create">Create Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmId> CreateAlarmAsync(EntityId entityId, AlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-test">Test Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmData[]> TestAlarmAsync(EntityId entityId, TestAlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-list">List Alarms (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Alarm, AlarmId>> ListAlarmsAsync(EntityId entityId, AlarmId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-get">Get Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Alarm> GetAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-update">Update Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAlarmAsync(EntityId entityId, AlarmId alarmId, UpdateAlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-delete">Remove Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        #endregion Alarms

        #region Notification Plans

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-create">Create Notification Plan (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationPlanId> CreateNotificationPlanAsync(NotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-list">List Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<NotificationPlan, NotificationPlanId>> ListNotificationPlansAsync(NotificationPlanId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-get">Get Notification Plan (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationPlan> GetNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-update">Update Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateNotificationPlanAsync(NotificationPlanId notificationPlanId, UpdateNotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-delete">Delete Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        #endregion Notification Plans

        #region Monitoring Zones

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-monitoring-zones.html#service-monitoring-zones-list">List Monitoring Zones (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<MonitoringZone, MonitoringZoneId>> ListMonitoringZonesAsync(MonitoringZoneId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-monitoring-zones.html#service-monitoring-zones-get">Get Monitoring Zone (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<MonitoringZone> GetMonitoringZoneAsync(MonitoringZoneId monitoringZoneId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-monitoring-zones.html#service-monitoring-zones-traceroute">Perform a "traceroute" from a Monitoring Zone (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<TraceRoute> PerformTraceRouteFromMonitoringZoneAsync(MonitoringZoneId monitoringZoneId, TraceRouteConfiguration configuration, CancellationToken cancellationToken);

        #endregion Monitoring Zones

        #region Alarm Notification History

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-notification-history.html#service-alarm-notification-history-discover">Discover Alarm Notification History (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckId[]> DiscoverAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-notification-history.html#service-alarm-notification-history-list">List Alarm Notification History (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AlarmNotificationHistoryItem, AlarmNotificationHistoryItemId>> ListAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CheckId checkId, AlarmNotificationHistoryItemId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-notification-history.html#service-alarm-notification-history-get">Get Alarm Notification History (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmNotificationHistoryItem> GetAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CheckId checkId, AlarmNotificationHistoryItemId alarmNotificationHistoryItemId, CancellationToken cancellationToken);

        #endregion Alarm Notification History

        #region Notifications

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-create">Create Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationId> CreateNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-test-new">Test Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationData> TestNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-test-existing">Test Existing Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationData> TestExistingNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-list">List Notifications (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Notification, NotificationId>> ListNotificationsAsync(NotificationId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-get">Get Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Notification> GetNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-update">Update Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateNotificationAsync(NotificationId notificationId, UpdateNotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-delete">Delete Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        #endregion Notifications

        #region Notification Types

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-types-crud.html#Service-Notification-Types-List">List Notification Types (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<NotificationType, NotificationTypeId>> ListNotificationTypesAsync(NotificationTypeId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-types-crud.html#Service-Notification-Types-get">Get Notification Type (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationType> GetNotificationTypeAsync(NotificationTypeId notificationTypeId, CancellationToken cancellationToken);

        #endregion Notification Types

        #region Changelogs

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-changelogs.html#service-changelogs-alarms-list">List Alarm Changelogs (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AlarmChangelog, AlarmChangelogId>> ListAlarmChangelogsAsync(AlarmChangelogId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-changelogs.html#service-changelogs-alarms-list">List Alarm Changelogs (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AlarmChangelog, AlarmChangelogId>> ListAlarmChangelogsAsync(EntityId entityId, AlarmChangelogId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        #endregion Changelogs

        #region Views

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-views.html#service-views-overview">Get Overview (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<EntityOverview, EntityId>> ListEntityOverviewsAsync(EntityId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-views.html#service-views-overview">Get Overview (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<EntityOverview, EntityId>> ListEntityOverviewsAsync(EntityId marker, int? limit, IEnumerable<EntityId> entityIdFilter, CancellationToken cancellationToken);

        #endregion Views

        #region Alarm Examples

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-examples.html#service-alarm-examples-list">List Alarm Examples (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AlarmExample, AlarmExampleId>> ListAlarmExamplesAsync(AlarmExampleId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-examples.html#service-alarm-examples-get">Get Alarm Example (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmExample> GetAlarmExampleAsync(AlarmExampleId alarmExampleId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-examples.html#service-alarm-examples-post">Evaluate Alarm Example (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<BoundAlarmExample> EvaluateAlarmExampleAsync(AlarmExampleId alarmExampleId, IDictionary<string, object> exampleParameters, CancellationToken cancellationToken);

        #endregion Alarm Examples

        #endregion Core

        #region Agent

        #region Agents

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agents">List Agents (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Agent, AgentId>> ListAgentsAsync(AgentId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent">Get Agent (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Agent> GetAgentAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connections">List Agent Connections (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AgentConnection, AgentConnectionId>> ListAgentConnectionsAsync(AgentId agentId, AgentConnectionId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connection">Get Agent Connection (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentConnection> GetAgentConnectionAsync(AgentId agentId, AgentConnectionId agentConnectionId, CancellationToken cancellationToken);

        #endregion Agents

        #region Agent Token

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-create-token">Create Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentTokenId> CreateAgentTokenAsync(AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-list">List Agent Tokens (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AgentToken, AgentTokenId>> ListAgentTokensAsync(AgentTokenId marker, int? limit, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-get">Get Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentToken> GetAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-update">Update Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAgentTokenAsync(AgentTokenId agentTokenId, AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-delete">Delete Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        #endregion Agent Token

        #region Agent Host Information

        /// <seealso href=""> (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<JToken>> GetAgentHostInformationAsync(AgentId agentId, HostInformationType hostInformation, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-cpus">Get CPUs Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<CpuInformation>>> GetCpuInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-disks">Get Disks Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<DiskInformation>>> GetDiskInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-filesystems">Get Filesystems Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<FilesystemInformation>>> GetFilesystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-memory">Get Memory Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<MemoryInformation>> GetMemoryInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-network_interfaces">Get Network Interfaces Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<NetworkInterfaceInformation>>> GetNetworkInterfaceInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-processes">Get Processes Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<ProcessInformation>>> GetProcessInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-system">Get System Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<SystemInformation>> GetSystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-who">Get Logged-in User Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<LoginInformation>>> GetLoginInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        #endregion Agent Host Information

        #region Agent Targets

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-targets.html#service-agent-list-check-targets">List Agent Check Targets (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<CheckTarget, CheckTargetId>> ListAgentCheckTargetsAsync(EntityId entityId, CheckTypeId agentCheckType, CheckTargetId marker, int? limit, CancellationToken cancellationToken);

        #endregion Agent Targets

        #endregion Agent
    }
}
