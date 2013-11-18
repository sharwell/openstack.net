namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using net.openstack.Providers.Rackspace.Objects.Monitoring;
    using CancellationToken = System.Threading.CancellationToken;
    using JObject = Newtonsoft.Json.Linq.JObject;

    /// <summary>
    /// Represents a provider for the Rackspace Cloud Monitoring service.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/overview.html">Rackspace Cloud Monitoring Developer Guide - API v1.0</seealso>
    /// <preliminary/>
    public interface IMonitoringService
    {
        #region Core

        #region Account

        Task<MonitoringAccount> GetAccountAsync(CancellationToken cancellationToken);

        Task UpdateAccountAsync(MonitoringAccountId accountId, AccountConfiguration configuration, CancellationToken cancellationToken);

        Task<MonitoringLimits> GetLimitsAsync(CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Audit, AuditId>> ListAuditsAsync(AuditId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        #endregion Account

        #region Entities

        Task<EntityId> CreateEntityAsync(EntityConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Entity, EntityId>> ListEntitiesAsync(EntityId marker, int? limit, CancellationToken cancellationToken);

        Task<Entity> GetEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        Task UpdateEntityAsync(EntityId entityId, UpdateEntityConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        #endregion Entities

        #region Checks

        Task<CheckId> CreateCheckAsync(EntityId entityId, CheckConfiguration configuration, CancellationToken cancellationToken);

        Task<CheckData[]> TestCheckAsync(EntityId entityId, CheckConfiguration configuration, bool? debug, CancellationToken cancellationToken);

        Task<CheckData[]> TestExistingCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Check, CheckId>> ListChecksAsync(EntityId entityId, CheckId marker, int? limit, CancellationToken cancellationToken);

        Task<Check> GetCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        Task UpdateCheckAsync(EntityId entityId, CheckId checkId, UpdateCheckConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        #endregion Checks

        #region Check Types

        //Task CreateCheckTypeAsync();

        Task<ReadOnlyCollectionPage<CheckType, CheckTypeId>> ListCheckTypesAsync(CheckTypeId marker, int? limit, CancellationToken cancellationToken);

        Task<CheckType> GetCheckTypeAsync(CheckTypeId checkTypeId, CancellationToken cancellationToken);

        //Task UpdateCheckTypeAsync();

        //Task RemoveCheckTypeAsync();

        #endregion Check Types

        #region Metrics

        Task<ReadOnlyCollectionPage<Metric, MetricName>> ListMetricsAsync(EntityId entityId, CheckId checkId, MetricName marker, int? limit, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<DataPoint, EntityId>> GetDataPointsAsync(EntityId entityId, CheckId checkId, MetricName metricName, int? points, DataPointGranularity resolution, IEnumerable<DataPointStatistic> select, CancellationToken cancellationToken);

        #endregion Metrics

        #region Alarms

        Task<AlarmId> CreateAlarmAsync(EntityId entityId, AlarmConfiguration configuration, CancellationToken cancellationToken);

        Task<AlarmData[]> TestAlarmAsync(EntityId entityId, TestAlarmConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Alarm, AlarmId>> ListAlarmsAsync(EntityId entityId, AlarmId marker, int? limit, CancellationToken cancellationToken);

        Task<Alarm> GetAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        Task UpdateAlarmAsync(EntityId entityId, AlarmId alarmId, UpdateAlarmConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        #endregion Alarms

        #region Notification Plans

        Task<NotificationPlanId> CreateNotificationPlanAsync(NotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<NotificationPlan, NotificationPlanId>> ListNotificationPlansAsync(NotificationPlanId marker, int? limit, CancellationToken cancellationToken);

        Task<NotificationPlan> GetNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        Task UpdateNotificationPlanAsync(NotificationPlanId notificationPlanId, UpdateNotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        #endregion Notification Plans

        #region Monitoring Zones

        //Task CreateMonitoringZoneAsync();

        Task<ReadOnlyCollectionPage<MonitoringZone, MonitoringZoneId>> ListMonitoringZonesAsync(MonitoringZoneId marker, int? limit, CancellationToken cancellationToken);

        Task<MonitoringZone> GetMonitoringZoneAsync(MonitoringZoneId monitoringZoneId, CancellationToken cancellationToken);

        //Task UpdateMonitoringZoneAsync();

        //Task RemoveMonitoringZoneAsync();

        Task<TraceRoute> PerformTraceRouteFromMonitoringZoneAsync(MonitoringZoneId monitoringZoneId, TraceRouteConfiguration configuration, CancellationToken cancellationToken);

        #endregion Monitoring Zones

        #region Alarm Notification History

        Task<CheckId[]> DiscoverAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<AlarmNotificationHistoryItem, AlarmNotificationHistoryItemId>> ListAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CheckId checkId, AlarmNotificationHistoryItemId marker, int? limit, CancellationToken cancellationToken);

        Task<AlarmNotificationHistoryItem> GetAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CheckId checkId, AlarmNotificationHistoryItemId alarmNotificationHistoryItemId, CancellationToken cancellationToken);

        #endregion Alarm Notification History

        #region Notifications

        Task<NotificationId> CreateNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        Task<NotificationData> TestNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        Task<NotificationData> TestExistingNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Notification, NotificationId>> ListNotificationsAsync(NotificationId marker, int? limit, CancellationToken cancellationToken);

        Task<Notification> GetNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        Task UpdateNotificationAsync(NotificationId notificationId, UpdateNotificationConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        #endregion Notifications

        #region Notification Types

        //Task CreateNotificationTypeAsync();

        Task<ReadOnlyCollectionPage<NotificationType, NotificationTypeId>> ListNotificationTypesAsync(NotificationTypeId marker, int? limit, CancellationToken cancellationToken);

        Task<NotificationType> GetNotificationTypeAsync(NotificationTypeId notificationTypeId, CancellationToken cancellationToken);

        //Task UpdateNotificationTypeAsync();

        //Task RemoveNotificationTypeAsync();

        #endregion Notification Types

        #region Changelogs

        Task<ReadOnlyCollectionPage<AlarmChangelog, AlarmChangelogId>> ListAlarmChangelogsAsync(AlarmChangelogId marker, int? limit, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<AlarmChangelog, AlarmChangelogId>> ListAlarmChangelogsAsync(EntityId entityId, AlarmChangelogId marker, int? limit, CancellationToken cancellationToken);

        #endregion Changelogs

        #region Views

        Task<ReadOnlyCollectionPage<EntityOverview, EntityId>> GetOverviewViewAsync(EntityId marker, int? limit, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<EntityOverview, EntityId>> GetOverviewViewAsync(EntityId marker, int? limit, IEnumerable<EntityId> entityIdFilter, CancellationToken cancellationToken);

        #endregion Views

        #region Alarm Examples

        Task<ReadOnlyCollectionPage<AlarmExample, AlarmExampleId>> ListAlarmExamplesAsync(AlarmExampleId marker, int? limit, CancellationToken cancellationToken);

        Task<AlarmExample> GetAlarmExampleAsync(AlarmExampleId alarmExampleId, CancellationToken cancellationToken);

        Task<BoundAlarmExample> EvaluateAlarmExampleAsync(AlarmExampleId alarmExampleId, AlarmExampleData exampleData, CancellationToken cancellationToken);

        #endregion Alarm Examples

        #endregion Core

        #region Agent

        #region Agents

        Task<ReadOnlyCollectionPage<Agent, AgentId>> ListAgentsAsync(AgentId marker, int? limit, CancellationToken cancellationToken);

        Task<Agent> GetAgentAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<AgentConnection, AgentConnectionId>> ListAgentConnectionsAsync(AgentId agentId, AgentConnectionId marker, int? limit, CancellationToken cancellationToken);

        Task<AgentConnection> GetAgentConnectionAsync(AgentId agentId, AgentConnectionId agentConnectionId, CancellationToken cancellationToken);

        #endregion Agents

        #region Agent Token

        Task<AgentTokenId> CreateAgentTokenAsync(AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<AgentToken, AgentTokenId>> ListAgentTokensAsync(AgentTokenId marker, int? limit, CancellationToken cancellationToken);

        Task<AgentToken> GetAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        Task UpdateAgentTokenAsync(AgentTokenId agentTokenId, AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        #endregion Agent Token

        #region Agent Host Information

        Task<HostInformation<JObject>> GetAgentHostInformationAsync(AgentId agentId, HostInformationType hostInformation, CancellationToken cancellationToken);

        Task<HostInformation<CpuInformation>> GetCpuInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<DiskInformation>> GetDiskInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<FilesystemInformation>> GetFilesystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<MemoryInformation>> GetMemoryInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<NetworkInterfaceInformation>> GetNetworkInterfaceInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<ProcessInformation>> GetProcessInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<SystemInformation>> GetSystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        Task<HostInformation<LoginInformation>> GetLoginInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        #endregion Agent Host Information

        #region Agent Targets

        Task<string[]> ListAgentCheckTargetsAsync(EntityId entityId, AgentCheckType agentCheckType, CancellationToken cancellationToken);

        #endregion Agent Targets

        #endregion Agent
    }
}
