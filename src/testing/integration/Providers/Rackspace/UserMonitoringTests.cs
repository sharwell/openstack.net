﻿namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Providers.Rackspace;
    using CloudIdentity = net.openstack.Core.Domain.CloudIdentity;
    using IIdentityProvider = net.openstack.Core.Providers.IIdentityProvider;
    using Path = System.IO.Path;
    using CancellationToken = System.Threading.CancellationToken;
    using CancellationTokenSource = System.Threading.CancellationTokenSource;
    using net.openstack.Providers.Rackspace.Objects.Monitoring;

    [TestClass]
    public class UserMonitoringTests
    {
        /// <summary>
        /// The prefix to use for names of entities created during integration testing.
        /// </summary>
        public static readonly string TestEntityPrefix = "UnitTestEntity-";

        /// <summary>
        /// The prefix to use for names of notification plans created during integration testing.
        /// </summary>
        public static readonly string TestNotificationPlanPrefix = "UnitTestNotificationPlan-";

        /// <summary>
        /// The prefix to use for names of notifications created during integration testing.
        /// </summary>
        public static readonly string TestNotificationPrefix = "UnitTestNotification-";

        /// <summary>
        /// The prefix to use for names of agent tokens created during integration testing.
        /// </summary>
        public static readonly string TestAgentTokenPrefix = "UnitTestAgentToken-";

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupMonitoringEntities()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                List<Task> cleanupTasks = new List<Task>();
                Entity[] entities = ListAllEntities(provider, null, cancellationTokenSource.Token).ToArray();
                foreach (Entity entity in entities)
                {
                    if (entity.Label == null || !entity.Label.StartsWith(TestEntityPrefix, StringComparison.OrdinalIgnoreCase))
                        continue;

                    Console.WriteLine("Removing entity '{0}' ({1})", entity.Label, entity.Id);
                    cleanupTasks.Add(provider.RemoveEntityAsync(entity.Id, cancellationTokenSource.Token));
                }

                if (cleanupTasks.Count > 0)
                    await Task.Factory.ContinueWhenAll(cleanupTasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public void CleanupMonitoringChecks()
        {
            // these are cleaned up as part of the entity cleanup
        }

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public void CleanupMonitoringAlarms()
        {
            // these are cleaned up as part of the entity cleanup
        }

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupMonitoringNotificationPlans()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                List<Task> cleanupTasks = new List<Task>();
                NotificationPlan[] plans = ListAllNotificationPlans(provider, null, cancellationTokenSource.Token).ToArray();
                foreach (NotificationPlan plan in plans)
                {
                    if (plan.Label == null || !plan.Label.Value.StartsWith(TestNotificationPlanPrefix, StringComparison.OrdinalIgnoreCase))
                        continue;

                    Console.WriteLine("Removing notification plan '{0}'", plan.Label);
                    cleanupTasks.Add(provider.RemoveNotificationPlansAsync(plan.Label, cancellationTokenSource.Token));
                }

                if (cleanupTasks.Count > 0)
                    await Task.Factory.ContinueWhenAll(cleanupTasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupMonitoringNotifications()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                List<Task> cleanupTasks = new List<Task>();
                Notification[] notifications = ListAllNotifications(provider, null, cancellationTokenSource.Token).ToArray();
                foreach (Notification notification in notifications)
                {
                    if (notification.Label == null || !notification.Label.StartsWith(TestNotificationPrefix, StringComparison.OrdinalIgnoreCase))
                        continue;

                    Console.WriteLine("Removing notification '{0}' ({1})", notification.Label, notification.Id);
                    cleanupTasks.Add(provider.RemoveNotificationAsync(notification.Id, cancellationTokenSource.Token));
                }

                if (cleanupTasks.Count > 0)
                    await Task.Factory.ContinueWhenAll(cleanupTasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.Cleanup)]
        public async Task CleanupMonitoringAgentTokens()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                List<Task> cleanupTasks = new List<Task>();
                AgentToken[] agentTokens = ListAllAgentTokens(provider, null, cancellationTokenSource.Token).ToArray();
                foreach (AgentToken agentToken in agentTokens)
                {
                    if (agentToken.Label == null || !agentToken.Label.StartsWith(TestAgentTokenPrefix, StringComparison.OrdinalIgnoreCase))
                        continue;

                    Console.WriteLine("Removing agent token '{0}' ({1})", agentToken.Label, agentToken.Id);
                    cleanupTasks.Add(provider.RemoveAgentTokenAsync(agentToken.Id, cancellationTokenSource.Token));
                }

                if (cleanupTasks.Count > 0)
                    await Task.Factory.ContinueWhenAll(cleanupTasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAccount()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateAccount()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetLimits()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAudits()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateEntity()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                string entityName = CreateRandomEntityName();
                EntityConfiguration configuration = new EntityConfiguration(entityName, null, null, null);
                EntityId entityId = await provider.CreateEntityAsync(configuration, cancellationTokenSource.Token);
                Assert.IsNotNull(entityId);

                Entity entity = await provider.GetEntityAsync(entityId, cancellationTokenSource.Token);
                Assert.IsNotNull(entity);
                Assert.AreEqual(entityId, entity.Id);
                Assert.AreEqual(entityName, entity.Label);

                await provider.RemoveEntityAsync(entityId, cancellationTokenSource.Token);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateEntityWithMetadata()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                string entityName = CreateRandomEntityName();
                IDictionary<string, string> metadata =
                    new Dictionary<string, string>
                    {
                        { "Key 1", "Value 1" },
                        { "key 1", "value 1" },
                        { "Key ²", "Value ²" },
                    };

                EntityConfiguration configuration = new EntityConfiguration(entityName, null, null, metadata);
                EntityId entityId = await provider.CreateEntityAsync(configuration, cancellationTokenSource.Token);
                Assert.IsNotNull(entityId);

                Entity entity = await provider.GetEntityAsync(entityId, cancellationTokenSource.Token);
                Assert.IsNotNull(entity);
                Assert.AreEqual(entityId, entity.Id);
                Assert.AreEqual(entityName, entity.Label);
                Assert.AreEqual("Value 1", entity.Metadata["Key 1"]);
                Assert.AreEqual("value 1", entity.Metadata["key 1"]);
                Assert.AreEqual("Value ²", entity.Metadata["Key ²"]);

                await provider.RemoveEntityAsync(entityId, cancellationTokenSource.Token);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListEntities()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Entity[] entities = ListAllEntities(provider, null, cancellationTokenSource.Token).ToArray();
                if (entities.Length == 0)
                    Assert.Inconclusive("The service did not report any entities.");

                foreach (Entity entity in entities)
                    Console.WriteLine("Entity {0} ({1})", entity.Label, entity.Id);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetEntity()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Entity[] entities = ListAllEntities(provider, null, cancellationTokenSource.Token).ToArray();
                if (entities.Length == 0)
                    Assert.Inconclusive("The service did not report any entities.");

                foreach (Entity entity in entities)
                {
                    Entity singleEntity = await provider.GetEntityAsync(entity.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(singleEntity);
                    Assert.AreEqual(entity.Id, singleEntity.Id);
                    Assert.AreEqual(entity.AgentId, singleEntity.AgentId);
                    Assert.AreEqual(entity.Label, singleEntity.Label);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateEntity()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTestCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTestExistingCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListChecks()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestRemoveCheck()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListCheckTypes()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetCheckType()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListMetrics()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetDataPoints()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateAlarm()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTestAlarm()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarms()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAlarm()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateAlarm()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestRemoveAlarm()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateNotificationPlan()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListNotificationPlans()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                NotificationPlan[] notificationPlans = ListAllNotificationPlans(provider, null, cancellationTokenSource.Token).ToArray();
                if (notificationPlans.Length == 0)
                    Assert.Inconclusive("The service did not report any notification plans.");

                foreach (NotificationPlan notificationPlan in notificationPlans)
                    Console.WriteLine("Notification plan {0}", notificationPlan.Label);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNotificationPlan()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateNotificationPlan()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestRemoveNotificationPlan()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListMonitoringZones()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                MonitoringZone[] monitoringZones = ListAllMonitoringZones(provider, null, cancellationTokenSource.Token).ToArray();
                if (monitoringZones.Length == 0)
                    Assert.Inconclusive("The provider did not return any monitoring zones.");

                foreach (MonitoringZone monitoringZone in monitoringZones)
                    Console.WriteLine("Monitoring zone '{0}'", monitoringZone.Label);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetMonitoringZone()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTraceRouteFromMonitoringZone()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarmNotificationHistory()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                bool foundHistoryItem = false;
                foreach (Entity entity in ListAllEntities(provider, null, cancellationTokenSource.Token))
                {
                    foreach (Alarm alarm in ListAllAlarms(provider, entity.Id, null, cancellationTokenSource.Token))
                    {
                        foreach (CheckId checkId in await provider.DiscoverAlarmNotificationHistoryAsync(entity.Id, alarm.Id, cancellationTokenSource.Token))
                        {
                            AlarmNotificationHistoryItem[] alarmNotificationHistory = ListAllAlarmNotificationHistory(provider, entity.Id, alarm.Id, checkId, null, cancellationTokenSource.Token).ToArray();
                            foundHistoryItem |= alarmNotificationHistory.Any();
                            foreach (AlarmNotificationHistoryItem item in alarmNotificationHistory)
                                Console.WriteLine("Alarm notification history item '{0}'", item.Id);
                        }
                    }
                }

                if (!foundHistoryItem)
                    Assert.Inconclusive("The provider did not return any alarm notification history items.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAlarmNotificationHistorySequential()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                bool foundHistoryItem = false;
                foreach (Entity entity in ListAllEntities(provider, null, cancellationTokenSource.Token))
                {
                    foreach (Alarm alarm in ListAllAlarms(provider, entity.Id, null, cancellationTokenSource.Token))
                    {
                        foreach (CheckId checkId in await provider.DiscoverAlarmNotificationHistoryAsync(entity.Id, alarm.Id, cancellationTokenSource.Token))
                        {
                            AlarmNotificationHistoryItem[] alarmNotificationHistory = ListAllAlarmNotificationHistory(provider, entity.Id, alarm.Id, checkId, null, cancellationTokenSource.Token).ToArray();
                            foundHistoryItem |= alarmNotificationHistory.Any();
                            foreach (AlarmNotificationHistoryItem item in alarmNotificationHistory)
                            {
                                AlarmNotificationHistoryItem singleItem = await provider.GetAlarmNotificationHistoryAsync(entity.Id, alarm.Id, checkId, item.Id, cancellationTokenSource.Token);
                                Assert.IsNotNull(singleItem);
                                Assert.AreEqual(item.Id, singleItem.Id);
                                Assert.AreEqual(item.NotificationPlanId, singleItem.NotificationPlanId);
                                Assert.AreEqual(item.PreviousState, singleItem.PreviousState);
                                Assert.AreEqual(item.State, singleItem.State);
                                Assert.AreEqual(item.Status, singleItem.Status);
                                Assert.AreEqual(item.Timestamp, singleItem.Timestamp);
                                Assert.AreEqual(item.TransactionId, singleItem.TransactionId);
                                if (item.Results == null)
                                {
                                    Assert.IsNull(singleItem.Results);
                                }
                                else
                                {
                                    Assert.AreEqual(item.Results.Count, singleItem.Results.Count);
                                    for (int i = 0; i < item.Results.Count; i++)
                                    {
                                        Assert.AreEqual(item.Results[i].NotificationId, singleItem.Results[i].NotificationId);
                                        Assert.AreEqual(item.Results[i].NotificationTypeId, singleItem.Results[i].NotificationTypeId);
                                        Assert.AreEqual(item.Results[i].InProgress, singleItem.Results[i].InProgress);
                                        Assert.AreEqual(item.Results[i].Success, singleItem.Results[i].Success);
                                        Assert.AreEqual(item.Results[i].Message, singleItem.Results[i].Message);
                                    }
                                }
                            }
                        }
                    }
                }

                if (!foundHistoryItem)
                    Assert.Inconclusive("The provider did not return any alarm notification history items.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAlarmNotificationHistory()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                // force authentication before starting the timer
                await provider.ListMonitoringZonesAsync(null, null, cancellationTokenSource.Token);

                Stopwatch stopwatch = Stopwatch.StartNew();

                List<Task<bool>> tasks = new List<Task<bool>>();
                foreach (Entity entity in await ListAllEntitiesAsync(provider, null, cancellationTokenSource.Token))
                {
                    tasks.Add(TestGetAlarmNotificationHistory(provider, entity, cancellationTokenSource.Token));
                }

                if (tasks.Count > 0)
                    await Task.Factory.ContinueWhenAll((Task[])tasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);

                bool foundHistoryItem = tasks.Any(i => i.Result);

                Console.WriteLine("Elapsed time: {0}ms", stopwatch.ElapsedMilliseconds);

                if (!foundHistoryItem)
                    Assert.Inconclusive("The provider did not return any alarm notification history items.");
            }
        }

        private async Task<bool> TestGetAlarmNotificationHistory(IMonitoringService provider, Entity entity, CancellationToken cancellationToken)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (Alarm alarm in await ListAllAlarmsAsync(provider, entity.Id, null, cancellationToken))
            {
                tasks.Add(TestGetAlarmNotificationHistory(provider, entity, alarm, cancellationToken));
            }

            if (tasks.Count > 0)
                await Task.Factory.ContinueWhenAll((Task[])tasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);

            return tasks.Any(i => i.Result);
        }

        private async Task<bool> TestGetAlarmNotificationHistory(IMonitoringService provider, Entity entity, Alarm alarm, CancellationToken cancellationToken)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (CheckId checkId in await provider.DiscoverAlarmNotificationHistoryAsync(entity.Id, alarm.Id, cancellationToken))
            {
                tasks.Add(TestGetAlarmNotificationHistory(provider, entity, alarm, checkId, cancellationToken));
            }

            if (tasks.Count > 0)
                await Task.Factory.ContinueWhenAll((Task[])tasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);

            return tasks.Any(i => i.Result);
        }

        private async Task<bool> TestGetAlarmNotificationHistory(IMonitoringService provider, Entity entity, Alarm alarm, CheckId checkId, CancellationToken cancellationToken)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            AlarmNotificationHistoryItem[] alarmNotificationHistory = await ListAllAlarmNotificationHistoryAsync(provider, entity.Id, alarm.Id, checkId, null, cancellationToken);
            foreach (AlarmNotificationHistoryItem item in alarmNotificationHistory)
            {
                tasks.Add(TestGetAlarmNotificationHistory(provider, entity, alarm, checkId, item, cancellationToken));
            }

            if (tasks.Count > 0)
                await Task.Factory.ContinueWhenAll((Task[])tasks.ToArray(), TaskExtrasExtensions.PropagateExceptions);

            return tasks.Any(i => i.Result);
        }

        private async Task<bool> TestGetAlarmNotificationHistory(IMonitoringService provider, Entity entity, Alarm alarm, CheckId checkId, AlarmNotificationHistoryItem item, CancellationToken cancellationToken)
        {
            AlarmNotificationHistoryItem singleItem = await provider.GetAlarmNotificationHistoryAsync(entity.Id, alarm.Id, checkId, item.Id, cancellationToken);
            Assert.IsNotNull(singleItem);
            Assert.AreEqual(item.Id, singleItem.Id);
            Assert.AreEqual(item.NotificationPlanId, singleItem.NotificationPlanId);
            Assert.AreEqual(item.PreviousState, singleItem.PreviousState);
            Assert.AreEqual(item.State, singleItem.State);
            Assert.AreEqual(item.Status, singleItem.Status);
            Assert.AreEqual(item.Timestamp, singleItem.Timestamp);
            Assert.AreEqual(item.TransactionId, singleItem.TransactionId);
            if (item.Results == null)
            {
                Assert.IsNull(singleItem.Results);
            }
            else
            {
                Assert.AreEqual(item.Results.Count, singleItem.Results.Count);
                for (int i = 0; i < item.Results.Count; i++)
                {
                    Assert.AreEqual(item.Results[i].NotificationId, singleItem.Results[i].NotificationId);
                    Assert.AreEqual(item.Results[i].NotificationTypeId, singleItem.Results[i].NotificationTypeId);
                    Assert.AreEqual(item.Results[i].InProgress, singleItem.Results[i].InProgress);
                    Assert.AreEqual(item.Results[i].Success, singleItem.Results[i].Success);
                    Assert.AreEqual(item.Results[i].Message, singleItem.Results[i].Message);
                }
            }

            return true;
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTestNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestTestExistingNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListNotifications()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Notification[] notifications = ListAllNotifications(provider, null, cancellationTokenSource.Token).ToArray();
                if (notifications.Length == 0)
                    Assert.Inconclusive("The service did not report any notifications.");

                foreach (Notification notification in notifications)
                    Console.WriteLine("Notification '{0}' ({1})", notification.Label, notification.Id);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNotification()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Notification[] notifications = ListAllNotifications(provider, null, cancellationTokenSource.Token).ToArray();
                if (notifications.Length == 0)
                    Assert.Inconclusive("The service did not report any notifications.");

                foreach (Notification notification in notifications)
                {
                    Notification singleNotification = await provider.GetNotificationAsync(notification.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(singleNotification);
                    Assert.AreEqual(notification.Id, singleNotification.Id);
                    Assert.AreEqual(notification.Label, singleNotification.Label);
                    Assert.AreEqual(notification.Type, singleNotification.Type);

                    if (notification.Type != null)
                    {
                        NotificationType notificationType = await provider.GetNotificationTypeAsync(notification.Type, cancellationTokenSource.Token);
                        Assert.IsNotNull(notificationType);
                    }
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestRemoveNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListNotificationTypes()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                NotificationType[] notifications = ListAllNotificationTypes(provider, null, cancellationTokenSource.Token).ToArray();
                if (notifications.Length == 0)
                    Assert.Inconclusive("The service did not report any notification types.");

                foreach (NotificationType notificationType in notifications)
                {
                    Console.WriteLine("Notification '{0}'", notificationType.Id);
                    foreach (NotificationTypeField field in notificationType.Fields)
                        Console.WriteLine("    {0}{1} // {2}", field.Name, (field.Optional ?? false) ? " (optional)" : string.Empty, field.Description);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNotificationType()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                NotificationType[] notifications = ListAllNotificationTypes(provider, null, cancellationTokenSource.Token).ToArray();
                if (notifications.Length == 0)
                    Assert.Inconclusive("The service did not report any notification types.");

                foreach (NotificationType notificationType in notifications)
                {
                    NotificationType singleNotificationType = await provider.GetNotificationTypeAsync(notificationType.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(singleNotificationType);
                    Assert.AreEqual(notificationType.Id, singleNotificationType.Id);
                    if (notificationType.Fields == null)
                    {
                        Assert.IsNull(singleNotificationType.Fields);
                    }
                    else
                    {
                        Assert.AreEqual(notificationType.Fields.Count, singleNotificationType.Fields.Count);
                        for (int i = 0; i < notificationType.Fields.Count; i++)
                        {
                            Assert.AreEqual(notificationType.Fields[i].Name, singleNotificationType.Fields[i].Name);
                            Assert.AreEqual(notificationType.Fields[i].Optional, singleNotificationType.Fields[i].Optional);
                            Assert.AreEqual(notificationType.Fields[i].Description, singleNotificationType.Fields[i].Description);
                        }
                    }
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarmChangelogs()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                AlarmChangelog[] alarmChangelogs = ListAllAlarmChangelogs(provider, null, cancellationTokenSource.Token).ToArray();
                if (alarmChangelogs.Length == 0)
                    Assert.Inconclusive("The service did not report any alarm changelogs.");

                foreach (AlarmChangelog alarmChangelog in alarmChangelogs)
                    Console.WriteLine("Alarm changelog '{0}'", alarmChangelog.Id);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarmChangelogsWithEntityFilter()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetOverview()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetOverviewWithEntityFilter()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListAlarmExamples()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                AlarmExample[] alarmExamples = ListAllAlarmExamples(provider, null, cancellationTokenSource.Token).ToArray();
                if (alarmExamples.Length == 0)
                    Assert.Inconclusive("The provider did not return any alarm examples.");

                foreach (AlarmExample alarmExample in alarmExamples)
                    Console.WriteLine(alarmExample.Label);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAlarmExample()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                AlarmExample[] alarmExamples = ListAllAlarmExamples(provider, null, cancellationTokenSource.Token).ToArray();
                foreach (AlarmExample alarmExample in alarmExamples)
                {
                    AlarmExample example = await provider.GetAlarmExampleAsync(alarmExample.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(example);
                    Assert.AreEqual(alarmExample.Id, example.Id);
                    Assert.AreEqual(alarmExample.Label, example.Label);
                    Assert.AreEqual(alarmExample.Description, example.Description);
                    Assert.AreEqual(alarmExample.Criteria, example.Criteria);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestEvaluateAlarmExample()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListAgents()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Agent[] agents = ListAllAgents(provider, null, cancellationTokenSource.Token).ToArray();
                if (agents.Length == 0)
                    Assert.Inconclusive("The service did not report any agents.");

                foreach (Agent agent in agents)
                    Console.WriteLine("Agent {0} last connected {1}", agent.Id, agent.LastConnected);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAgent()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Agent[] agents = ListAllAgents(provider, null, cancellationTokenSource.Token).ToArray();
                if (agents.Length == 0)
                    Assert.Inconclusive("The service did not report any agents.");

                foreach (Agent agent in agents)
                {
                    Agent singleAgent = await provider.GetAgentAsync(agent.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(singleAgent);
                    Assert.AreEqual(agent.Id, singleAgent.Id);
                    Assert.IsTrue(agent.LastConnected <= singleAgent.LastConnected);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListAgentConnections()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Agent[] agents = ListAllAgents(provider, null, cancellationTokenSource.Token).ToArray();
                if (agents.Length == 0)
                    Assert.Inconclusive("The service did not report any agents.");

                bool foundConnection = false;
                foreach (Agent agent in agents)
                {
                    Console.WriteLine("Connections for Agent {0}", agent.Id);
                    AgentConnection[] connections = ListAllAgentConnections(provider, agent.Id, null, cancellationTokenSource.Token).ToArray();
                    foundConnection |= connections.Any();
                    foreach (AgentConnection connection in connections)
                    {
                        Assert.AreEqual(agent.Id, connection.AgentId);
                        Console.WriteLine("    {0}", connection.Id);
                    }
                }

                if (!foundConnection)
                    Assert.Inconclusive("The service did not report any agent connections.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAgentConnection()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                Agent[] agents = ListAllAgents(provider, null, cancellationTokenSource.Token).ToArray();
                if (agents.Length == 0)
                    Assert.Inconclusive("The service did not report any agents.");

                bool foundConnection = false;
                foreach (Agent agent in agents)
                {
                    AgentConnection[] connections = ListAllAgentConnections(provider, agent.Id, null, cancellationTokenSource.Token).ToArray();
                    foundConnection |= connections.Any();
                    foreach (AgentConnection connection in connections)
                    {
                        AgentConnection singleConnection = await provider.GetAgentConnectionAsync(agent.Id, connection.Id, cancellationTokenSource.Token);
                        Assert.IsNotNull(singleConnection);
                        Assert.AreEqual(connection.Id, singleConnection.Id);
                        Assert.AreEqual(connection.Guid, singleConnection.Guid);
                        Assert.AreEqual(connection.ProcessVersion, singleConnection.ProcessVersion);
                        Assert.AreEqual(connection.Endpoint, singleConnection.Endpoint);
                        Assert.AreEqual(connection.BundleVersion, singleConnection.BundleVersion);
                        Assert.AreEqual(connection.AgentAddress, singleConnection.AgentAddress);
                        Assert.AreEqual(connection.AgentId, singleConnection.AgentId);
                    }
                }

                if (!foundConnection)
                    Assert.Inconclusive("The service did not report any agent connections.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateAgentToken()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                string label = CreateRandomAgentTokenName();
                AgentTokenConfiguration configuration = new AgentTokenConfiguration(label);

                AgentTokenId agentTokenId = await provider.CreateAgentTokenAsync(configuration, cancellationTokenSource.Token);
                Assert.IsNotNull(agentTokenId);

                AgentToken agentToken = await provider.GetAgentTokenAsync(agentTokenId, cancellationTokenSource.Token);
                Assert.IsNotNull(agentToken);
                Assert.AreEqual(agentTokenId, agentToken.Id);
                Assert.AreEqual(label, agentToken.Label);

                await provider.RemoveAgentTokenAsync(agentTokenId, cancellationTokenSource.Token);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public void TestListAgentTokens()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                AgentToken[] agentTokens = ListAllAgentTokens(provider, null, cancellationTokenSource.Token).ToArray();
                if (agentTokens.Length == 0)
                    Assert.Inconclusive("The service did not report any agent tokens.");

                foreach (AgentToken agentToken in agentTokens)
                    Console.WriteLine("Agent Token {0} ({1})", agentToken.Label, agentToken.Id);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateAgentToken()
        {
            IMonitoringService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                string label = CreateRandomAgentTokenName();
                AgentTokenConfiguration configuration = new AgentTokenConfiguration(label);

                AgentTokenId agentTokenId = await provider.CreateAgentTokenAsync(configuration, cancellationTokenSource.Token);
                Assert.IsNotNull(agentTokenId);

                AgentToken agentToken = await provider.GetAgentTokenAsync(agentTokenId, cancellationTokenSource.Token);
                Assert.IsNotNull(agentToken);
                Assert.AreEqual(agentTokenId, agentToken.Id);
                Assert.AreEqual(label, agentToken.Label);

                string updatedLabel = CreateRandomAgentTokenName();
                AgentTokenConfiguration updateConfiguration = new AgentTokenConfiguration(updatedLabel);
                await provider.UpdateAgentTokenAsync(agentTokenId, updateConfiguration, cancellationTokenSource.Token);

                AgentToken updatedAgentToken = await provider.GetAgentTokenAsync(agentTokenId, cancellationTokenSource.Token);
                Assert.IsNotNull(updatedAgentToken);
                Assert.AreEqual(updatedLabel, updatedAgentToken.Label);
                Assert.AreEqual(agentToken.Id, updatedAgentToken.Id);
                Assert.AreEqual(agentToken.Token, updatedAgentToken.Token);

                await provider.RemoveAgentTokenAsync(agentTokenId, cancellationTokenSource.Token);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAgentHostInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetCpuInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetDiskInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetFilesystemInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetMemoryInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNetworkInterfaceInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetProcessInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetSystemInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetLoginInformation()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAgentCheckTargets()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        protected static IEnumerable<AlarmChangelog> ListAllAlarmChangelogs(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            AlarmChangelogId marker = null;

            do
            {
                ReadOnlyCollectionPage<AlarmChangelog, AlarmChangelogId> page = service.ListAlarmChangelogsAsync(marker, blockSize, cancellationToken).Result;
                foreach (AlarmChangelog alarmChangelog in page)
                    yield return alarmChangelog;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<AlarmExample> ListAllAlarmExamples(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            AlarmExampleId marker = null;

            do
            {
                ReadOnlyCollectionPage<AlarmExample, AlarmExampleId> page = service.ListAlarmExamplesAsync(marker, blockSize, cancellationToken).Result;
                foreach (AlarmExample example in page)
                    yield return example;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<AlarmNotificationHistoryItem> ListAllAlarmNotificationHistory(IMonitoringService service, EntityId entityId, AlarmId alarmId, CheckId checkId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (entityId == null)
                throw new ArgumentNullException("entityId");
            if (alarmId == null)
                throw new ArgumentNullException("alarmId");
            if (checkId == null)
                throw new ArgumentNullException("checkId");

            AlarmNotificationHistoryItemId marker = null;

            do
            {
                ReadOnlyCollectionPage<AlarmNotificationHistoryItem, AlarmNotificationHistoryItemId> page = service.ListAlarmNotificationHistoryAsync(entityId, alarmId, checkId, marker, blockSize, cancellationToken).Result;
                foreach (AlarmNotificationHistoryItem example in page)
                    yield return example;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static async Task<AlarmNotificationHistoryItem[]> ListAllAlarmNotificationHistoryAsync(IMonitoringService service, EntityId entityId, AlarmId alarmId, CheckId checkId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (entityId == null)
                throw new ArgumentNullException("entityId");
            if (alarmId == null)
                throw new ArgumentNullException("alarmId");
            if (checkId == null)
                throw new ArgumentNullException("checkId");

            List<AlarmNotificationHistoryItem> result = new List<AlarmNotificationHistoryItem>();
            AlarmNotificationHistoryItemId marker = null;

            do
            {
                ReadOnlyCollectionPage<AlarmNotificationHistoryItem, AlarmNotificationHistoryItemId> page = await service.ListAlarmNotificationHistoryAsync(entityId, alarmId, checkId, marker, blockSize, cancellationToken);
                result.AddRange(page);
                marker = page.Marker;
            } while (marker != null);

            return result.ToArray();
        }

        protected static IEnumerable<Entity> ListAllEntities(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            EntityId marker = null;

            do
            {
                ReadOnlyCollectionPage<Entity, EntityId> page = service.ListEntitiesAsync(marker, blockSize, cancellationToken).Result;
                foreach (Entity entity in page)
                    yield return entity;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static async Task<Entity[]> ListAllEntitiesAsync(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            List<Entity> result = new List<Entity>();
            EntityId marker = null;

            do
            {
                ReadOnlyCollectionPage<Entity, EntityId> page = await service.ListEntitiesAsync(marker, blockSize, cancellationToken);
                result.AddRange(page);
                marker = page.Marker;
            } while (marker != null);

            return result.ToArray();
        }

        protected static IEnumerable<Alarm> ListAllAlarms(IMonitoringService service, EntityId entityId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (entityId == null)
                throw new ArgumentNullException("entityId");

            AlarmId marker = null;

            do
            {
                ReadOnlyCollectionPage<Alarm, AlarmId> page = service.ListAlarmsAsync(entityId, marker, blockSize, cancellationToken).Result;
                foreach (Alarm alarm in page)
                    yield return alarm;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static async Task<Alarm[]> ListAllAlarmsAsync(IMonitoringService service, EntityId entityId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (entityId == null)
                throw new ArgumentNullException("entityId");

            List<Alarm> result = new List<Alarm>();
            AlarmId marker = null;

            do
            {
                ReadOnlyCollectionPage<Alarm, AlarmId> page = await service.ListAlarmsAsync(entityId, marker, blockSize, cancellationToken);
                result.AddRange(page);
                marker = page.Marker;
            } while (marker != null);

            return result.ToArray();
        }

        protected static IEnumerable<Check> ListAllChecks(IMonitoringService service, EntityId entityId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (entityId == null)
                throw new ArgumentNullException("entityId");

            CheckId marker = null;

            do
            {
                ReadOnlyCollectionPage<Check, CheckId> page = service.ListChecksAsync(entityId, marker, blockSize, cancellationToken).Result;
                foreach (Check check in page)
                    yield return check;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<MonitoringZone> ListAllMonitoringZones(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            MonitoringZoneId marker = null;

            do
            {
                ReadOnlyCollectionPage<MonitoringZone, MonitoringZoneId> page = service.ListMonitoringZonesAsync(marker, blockSize, cancellationToken).Result;
                foreach (MonitoringZone monitoringZone in page)
                    yield return monitoringZone;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<NotificationPlan> ListAllNotificationPlans(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            NotificationPlanId marker = null;

            do
            {
                ReadOnlyCollectionPage<NotificationPlan, NotificationPlanId> page = service.ListNotificationPlansAsync(marker, blockSize, cancellationToken).Result;
                foreach (NotificationPlan plan in page)
                    yield return plan;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<Notification> ListAllNotifications(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            NotificationId marker = null;

            do
            {
                ReadOnlyCollectionPage<Notification, NotificationId> page = service.ListNotificationsAsync(marker, blockSize, cancellationToken).Result;
                foreach (Notification notification in page)
                    yield return notification;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<NotificationType> ListAllNotificationTypes(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            NotificationTypeId marker = null;

            do
            {
                ReadOnlyCollectionPage<NotificationType, NotificationTypeId> page = service.ListNotificationTypesAsync(marker, blockSize, cancellationToken).Result;
                foreach (NotificationType notificationType in page)
                    yield return notificationType;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<Agent> ListAllAgents(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            AgentId marker = null;

            do
            {
                ReadOnlyCollectionPage<Agent, AgentId> page = service.ListAgentsAsync(marker, blockSize, cancellationToken).Result;
                foreach (Agent agent in page)
                    yield return agent;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<AgentConnection> ListAllAgentConnections(IMonitoringService service, AgentId agentId, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            AgentConnectionId marker = null;

            do
            {
                ReadOnlyCollectionPage<AgentConnection, AgentConnectionId> page = service.ListAgentConnectionsAsync(agentId, marker, blockSize, cancellationToken).Result;
                foreach (AgentConnection connection in page)
                    yield return connection;

                marker = page.Marker;
            } while (marker != null);
        }

        protected static IEnumerable<AgentToken> ListAllAgentTokens(IMonitoringService service, int? blockSize, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            AgentTokenId marker = null;

            do
            {
                ReadOnlyCollectionPage<AgentToken, AgentTokenId> page = service.ListAgentTokensAsync(marker, blockSize, cancellationToken).Result;
                foreach (AgentToken agentToken in page)
                    yield return agentToken;

                marker = page.Marker;
            } while (marker != null);
        }

        private TimeSpan TestTimeout(TimeSpan timeout)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Using extended timeout due to attached debugger.");
                return TimeSpan.FromDays(1);
            }

            return timeout;
        }

        /// <summary>
        /// Creates a random entity name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated entity name.</returns>
        internal static string CreateRandomEntityName()
        {
            return TestEntityPrefix + Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates a random check name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated check name.</returns>
        internal static string CreateRandomCheckName()
        {
            return Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates a random alarm name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated alarm name.</returns>
        internal static string CreateRandomAlarmName()
        {
            return Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates a random notification plan name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated notification plan name.</returns>
        internal static string CreateRandomNotificationPlanName()
        {
            return TestNotificationPlanPrefix + Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates a random notification name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated notification name.</returns>
        internal static string CreateRandomNotificationName()
        {
            return TestNotificationPrefix + Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates a random agent token name with the proper prefix for integration testing.
        /// </summary>
        /// <returns>A unique, randomly-generated agent token name.</returns>
        internal static string CreateRandomAgentTokenName()
        {
            return TestAgentTokenPrefix + Path.GetRandomFileName();
        }

        /// <summary>
        /// Creates an instance of <see cref="IMonitoringService"/> for testing using
        /// the <see cref="OpenstackNetSetings.TestIdentity"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IMonitoringService"/> for integration testing.</returns>
        internal static IMonitoringService CreateProvider()
        {
            var provider = new TestCloudMonitoringProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null);
            provider.BeforeAsyncWebRequest +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Request) {1} {2}", DateTime.Now, e.Request.Method, e.Request.RequestUri);
                };
            provider.AfterAsyncWebResponse +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Result {1}) {2}", DateTime.Now, e.Response.StatusCode, e.Response.ResponseUri);
                };

            //provider.ConnectionLimit = 8;
            return provider;
        }

        internal class TestCloudMonitoringProvider : CloudMonitoringProvider
        {
            public TestCloudMonitoringProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider)
                : base(defaultIdentity, defaultRegion, identityProvider)
            {
            }

            protected override Tuple<HttpWebResponse, string> ReadResultImpl(Task<WebResponse> task, CancellationToken cancellationToken)
            {
                try
                {
                    Tuple<HttpWebResponse, string> result = base.ReadResultImpl(task, cancellationToken);
                    if (!string.IsNullOrEmpty(result.Item2))
                        Console.Error.WriteLine("==> " + result.Item2);

                    return result;
                }
                catch (WebException ex)
                {
                    if (task.Result.ContentLength > 0)
                        Console.Error.WriteLine("==> " + ex.Message);

                    throw;
                }
            }
        }
    }
}
