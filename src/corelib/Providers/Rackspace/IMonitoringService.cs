namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using net.openstack.Providers.Rackspace.Objects.Monitoring;
    using Newtonsoft.Json.Linq;
    using CancellationToken = System.Threading.CancellationToken;
    using WebException = System.Net.WebException;

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
        /// Gets information about a monitoring account.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="MonitoringAccount"/> object describing
        /// the account.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-root">Get Account (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<MonitoringAccount> GetAccountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring account.
        /// </summary>
        /// <param name="accountId">The account ID. This is obtained from <see cref="MonitoringAccount.Id">MonitoringAccount.Id</see>.</param>
        /// <param name="configuration">An <see cref="AccountConfiguration"/> object describing the changes to apply to the account.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="accountId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-put-account">Update Account (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAccountAsync(MonitoringAccountId accountId, AccountConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the resource and rate limits enforced by the monitoring service.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="MonitoringLimits"/> object describing
        /// the resource and rate limits of the monitoring service.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-get-limits">Get Limits (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<MonitoringLimits> GetLimitsAsync(CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-account.html#service-account-list-audits">List Audits (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Audit, AuditId>> ListAuditsAsync(AuditId marker, int? limit, DateTimeOffset? from, DateTimeOffset? to, CancellationToken cancellationToken);

        #endregion Account

        #region Entities

        /// <summary>
        /// Creates a new monitoring entity.
        /// </summary>
        /// <param name="configuration">An <see cref="EntityConfiguration"/> object describing the new entity.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="EntityId"/> identifying the new entity.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-create">Create Entities (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<EntityId> CreateEntityAsync(EntityConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-list">List Entities (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Entity, EntityId>> ListEntitiesAsync(EntityId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring entity by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="Entity"/> object describing the entity.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="entityId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-get">Get Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Entity> GetEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring entity.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateEntityConfiguration"/> object describing the changes to apply to the entity.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-update">Update Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateEntityAsync(EntityId entityId, UpdateEntityConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring entity by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="entityId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-entities.html#service-entities-delete">Delete Entity (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveEntityAsync(EntityId entityId, CancellationToken cancellationToken);

        #endregion Entities

        #region Checks

        /// <summary>
        /// Creates a new check.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="configuration">A <see cref="CheckConfiguration"/> object describing the new check.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="CheckId"/> identifying the new check.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-create">Create Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckId> CreateCheckAsync(EntityId entityId, CheckConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test">Test Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test-debug">Test Check and Include Debug Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckData[]> TestCheckAsync(EntityId entityId, CheckConfiguration configuration, bool? debug, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-test-existing">Test Existing Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<CheckData[]> TestExistingCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-list">List Checks (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Check, CheckId>> ListChecksAsync(EntityId entityId, CheckId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring check by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="checkId">The check ID. This is obtained from <see cref="Check.Id">Check.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="Check"/> object describing the check.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="checkId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-get">Get Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Check> GetCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring check.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="checkId">The check ID. This is obtained from <see cref="Check.Id">Check.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateCheckConfiguration"/> object describing the changes to apply to the check.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="checkId"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-update">Update Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateCheckAsync(EntityId entityId, CheckId checkId, UpdateCheckConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring check by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="checkId">The check ID. This is obtained from <see cref="Check.Id">Check.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="checkId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-checks.html#service-checks-delete">Delete Check (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveCheckAsync(EntityId entityId, CheckId checkId, CancellationToken cancellationToken);

        #endregion Checks

        #region Check Types

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-check-types.html#service-check-types-list">List Check Types (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<CheckType, CheckTypeId>> ListCheckTypesAsync(CheckTypeId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring check type by ID.
        /// </summary>
        /// <param name="checkTypeId">The check type ID. This is obtained from <see cref="CheckType.Id">CheckType.Id</see>, or from the predefined values in <see cref="CheckTypeId"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="CheckType"/> object describing the
        /// check type.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="checkTypeId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <summary>
        /// Creates a new alarm.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="configuration">An <see cref="AlarmConfiguration"/> object describing the new alarm.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="AlarmId"/> identifying the new alarm.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-create">Create Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmId> CreateAlarmAsync(EntityId entityId, AlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-test">Test Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmData[]> TestAlarmAsync(EntityId entityId, TestAlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-list">List Alarms (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Alarm, AlarmId>> ListAlarmsAsync(EntityId entityId, AlarmId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring alarm by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="alarmId">The alarm ID. This is obtained from <see cref="Alarm.Id">Alarm.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="Alarm"/> object describing the alarm.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="alarmId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-get">Get Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Alarm> GetAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring alarm.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="alarmId">The alarm ID. This is obtained from <see cref="Alarm.Id">Alarm.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateAlarmConfiguration"/> object describing the changes to apply to the alarm.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="alarmId"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-update">Update Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAlarmAsync(EntityId entityId, AlarmId alarmId, UpdateAlarmConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring alarm by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="alarmId">The alarm ID. This is obtained from <see cref="Alarm.Id">Alarm.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="alarmId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarms.html#service-alarms-delete">Remove Alarm (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveAlarmAsync(EntityId entityId, AlarmId alarmId, CancellationToken cancellationToken);

        #endregion Alarms

        #region Notification Plans

        /// <summary>
        /// Creates a new notification plan.
        /// </summary>
        /// <param name="configuration">A <see cref="NotificationPlanConfiguration"/> object describing the new notification plan.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="NotificationPlanId"/> identifying the
        /// new notification plan.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-create">Create Notification Plan (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationPlanId> CreateNotificationPlanAsync(NotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-list">List Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<NotificationPlan, NotificationPlanId>> ListNotificationPlansAsync(NotificationPlanId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring notification plan by ID.
        /// </summary>
        /// <param name="notificationPlanId">The notification plan ID. This is obtained from <see cref="NotificationPlan.Id">NotificationPlan.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="NotificationPlan"/> object describing
        /// the notification plan.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="notificationPlanId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-get">Get Notification Plan (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationPlan> GetNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring notification plan.
        /// </summary>
        /// <param name="notificationPlanId">The notification plan ID. This is obtained from <see cref="NotificationPlan.Id">NotificationPlan.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateNotificationPlanConfiguration"/> object describing the changes to apply to the notification plan.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="notificationPlanId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-update">Update Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateNotificationPlanAsync(NotificationPlanId notificationPlanId, UpdateNotificationPlanConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring notification plan by ID.
        /// </summary>
        /// <param name="notificationPlanId">The notification plan ID. This is obtained from <see cref="NotificationPlan.Id">NotificationPlan.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="notificationPlanId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-plans.html#service-notification-plans-delete">Delete Notification Plans (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveNotificationPlanAsync(NotificationPlanId notificationPlanId, CancellationToken cancellationToken);

        #endregion Notification Plans

        #region Monitoring Zones

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-monitoring-zones.html#service-monitoring-zones-list">List Monitoring Zones (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<MonitoringZone, MonitoringZoneId>> ListMonitoringZonesAsync(MonitoringZoneId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring zone by ID.
        /// </summary>
        /// <param name="entityId">The monitoring zone ID. This is obtained from <see cref="MonitoringZone.Id">MonitoringZone.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="MonitoringZone"/> object describing the
        /// monitoring zone.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="monitoringZoneId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <summary>
        /// Gets a monitoring alarm notification history item by ID.
        /// </summary>
        /// <param name="entityId">The entity ID. This is obtained from <see cref="Entity.Id">Entity.Id</see>.</param>
        /// <param name="alarmId">The alarm ID. This is obtained from <see cref="Alarm.Id">Alarm.Id</see>.</param>
        /// <param name="checkId">The check ID. This is obtained from <see cref="Check.Id">Check.Id</see>.</param>
        /// <param name="alarmNotificationHistoryItemId">The alarm notification history item ID. This is obtained from <see cref="AlarmNotificationHistoryItem.Id">AlarmNotificationHistoryItem.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="AlarmNotificationHistoryItem"/> object
        /// describing the alarm notification history item.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="entityId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="alarmId"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="checkId"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="alarmNotificationHistoryItemId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-alarm-notification-history.html#service-alarm-notification-history-get">Get Alarm Notification History (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AlarmNotificationHistoryItem> GetAlarmNotificationHistoryAsync(EntityId entityId, AlarmId alarmId, CheckId checkId, AlarmNotificationHistoryItemId alarmNotificationHistoryItemId, CancellationToken cancellationToken);

        #endregion Alarm Notification History

        #region Notifications

        /// <summary>
        /// Creates a new notification.
        /// </summary>
        /// <param name="configuration">A <see cref="NotificationConfiguration"/> object describing the new notification.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="NotificationId"/> identifying the
        /// new notification.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-create">Create Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationId> CreateNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-test-new">Test Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationData> TestNotificationAsync(NotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-test-existing">Test Existing Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<NotificationData> TestExistingNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-list">List Notifications (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<Notification, NotificationId>> ListNotificationsAsync(NotificationId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring notification by ID.
        /// </summary>
        /// <param name="notificationId">The notification ID. This is obtained from <see cref="Notification.Id">Notification.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="Notification"/> object describing the
        /// notification.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="notificationId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-get">Get Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Notification> GetNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring notification.
        /// </summary>
        /// <param name="notificationId">The notification ID. This is obtained from <see cref="Notification.Id">Notification.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateNotificationConfiguration"/> object describing the changes to apply to the notification.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="notificationId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-update">Update Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateNotificationAsync(NotificationId notificationId, UpdateNotificationConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring notification by ID.
        /// </summary>
        /// <param name="notificationId">The notification ID. This is obtained from <see cref="Notification.Id">Notification.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="notificationId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notifications.html#service-notifications-delete">Delete Notification (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveNotificationAsync(NotificationId notificationId, CancellationToken cancellationToken);

        #endregion Notifications

        #region Notification Types

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-notification-types-crud.html#Service-Notification-Types-List">List Notification Types (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<NotificationType, NotificationTypeId>> ListNotificationTypesAsync(NotificationTypeId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring notification type by ID.
        /// </summary>
        /// <param name="notificationTypeId">The notification type ID. This is obtained from <see cref="NotificationType.Id">NotificationType.Id</see>, or from the predefined values in <see cref="NotificationTypeId"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="NotificationType"/> object describing\
        /// the notification type.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="notificationTypeId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <summary>
        /// Gets a monitoring alarm example by ID.
        /// </summary>
        /// <param name="entityId">The alarm example ID. This is obtained from <see cref="AlarmExample.Id">AlarmExample.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="AlarmExample"/> object describing the
        /// alarm example.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="alarmExampleId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <summary>
        /// Gets a monitoring agent by ID.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="Agent"/> object describing the agent.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent">Get Agent (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<Agent> GetAgentAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connections">List Agent Connections (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AgentConnection, AgentConnectionId>> ListAgentConnectionsAsync(AgentId agentId, AgentConnectionId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring agent connection by ID.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="agentConnectionId">The agent connection ID. This is obtained from <see cref="AgentConnection.Id">AgentConnection.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="AgentConnection"/> object describing
        /// the agent connection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="agentId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="agentConnectionId"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent.html#service-agent-list-agent-connection">Get Agent Connection (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentConnection> GetAgentConnectionAsync(AgentId agentId, AgentConnectionId agentConnectionId, CancellationToken cancellationToken);

        #endregion Agents

        #region Agent Token

        /// <summary>
        /// Creates a new agent token.
        /// </summary>
        /// <param name="configuration">An <see cref="AgentTokenConfiguration"/> object describing the new agent token.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain the <see cref="AgentTokenId"/> identifying the
        /// new agent token.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-create-token">Create Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentTokenId> CreateAgentTokenAsync(AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-list">List Agent Tokens (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<ReadOnlyCollectionPage<AgentToken, AgentTokenId>> ListAgentTokensAsync(AgentTokenId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a monitoring agent token by ID.
        /// </summary>
        /// <param name="agentTokenId">The agent token ID. This is obtained from <see cref="AgentToken.Id">AgentToken.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain an <see cref="AgentToken"/> object describing the
        /// agent token.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentTokenId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-get">Get Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<AgentToken> GetAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a monitoring agent token.
        /// </summary>
        /// <param name="agentTokenId">The agent token ID. This is obtained from <see cref="AgentToken.Id">AgentToken.Id</see>.</param>
        /// <param name="configuration">An <see cref="UpdateAgentTokenConfiguration"/> object describing the changes to apply to the agent token.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="agentTokenId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="configuration"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-update">Update Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task UpdateAgentTokenAsync(AgentTokenId agentTokenId, AgentTokenConfiguration configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete a monitoring agent token by ID.
        /// </summary>
        /// <param name="agentTokenId">The agent token ID. This is obtained from <see cref="AgentToken.Id">AgentToken.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentTokenId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-tokens.html#service-agent-token-delete">Delete Agent Token (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task RemoveAgentTokenAsync(AgentTokenId agentTokenId, CancellationToken cancellationToken);

        #endregion Agent Token

        #region Agent Host Information

        /// <seealso href=""> (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<JToken>> GetAgentHostInformationAsync(AgentId agentId, HostInformationType hostInformation, CancellationToken cancellationToken);

        /// <summary>
        /// Gets CPU information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-cpus">Get CPUs Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<CpuInformation>>> GetCpuInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets disk information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-disks">Get Disks Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<DiskInformation>>> GetDiskInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets filesystem information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-filesystems">Get Filesystems Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<FilesystemInformation>>> GetFilesystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets memory information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-memory">Get Memory Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<MemoryInformation>> GetMemoryInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets network interface information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-network_interfaces">Get Network Interfaces Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<NetworkInterfaceInformation>>> GetNetworkInterfaceInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets process information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-processes">Get Processes Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<ReadOnlyCollection<ProcessInformation>>> GetProcessInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets system information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cm/api/v1.0/cm-devguide/content/service-agent-host_info.html#service-agent-host_info-system">Get System Information (Cloud Monitoring Developer Guide - API v1.0)</seealso>
        Task<HostInformation<SystemInformation>> GetSystemInformationAsync(AgentId agentId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets login information reported by a monitoring agent.
        /// </summary>
        /// <param name="agentId">The agent ID. This is obtained from <see cref="Agent.Id">Agent.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When
        /// the task completes successfully, the <see cref="Task{TResult}.Result"/>
        /// property will contain a <see cref="HostInformation{T}"/>
        /// object containing the host information.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="agentId"/> is <c>null</c>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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
