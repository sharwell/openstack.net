namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
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

                    cleanupTasks.Add(provider.RemoveEntityAsync(entity.Id, cancellationTokenSource.Token));
                }

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

                    cleanupTasks.Add(provider.RemoveNotificationPlansAsync(plan.Label, cancellationTokenSource.Token));
                }

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

                    cleanupTasks.Add(provider.RemoveNotificationAsync(notification.Id, cancellationTokenSource.Token));
                }

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
                    if (agentToken.Label == null || !agentToken.Label.StartsWith(TestNotificationPrefix, StringComparison.OrdinalIgnoreCase))
                        continue;

                    cleanupTasks.Add(provider.RemoveAgentTokenAsync(agentToken.Id, cancellationTokenSource.Token));
                }

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
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListEntities()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetEntity()
        {
            Assert.Inconclusive("Not yet implemented.");
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
        public async Task TestRemoveEntity()
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
            Assert.Inconclusive("Not yet implemented.");
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
            Assert.Inconclusive("Not yet implemented.");
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
        public async Task TestDiscoverAlarmNotificationHistory()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarmNotificationHistory()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAlarmNotificationHistory()
        {
            Assert.Inconclusive("Not yet implemented.");
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
        public async Task TestListNotifications()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNotification()
        {
            Assert.Inconclusive("Not yet implemented.");
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
        public async Task TestListNotificationTypes()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetNotificationType()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAlarmChangelogs()
        {
            Assert.Inconclusive("Not yet implemented.");
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
        public async Task TestListAgentConnections()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAgentConnection()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestCreateAgentToken()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestListAgentTokens()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestGetAgentToken()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestUpdateAgentToken()
        {
            Assert.Inconclusive("Not yet implemented.");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Monitoring)]
        public async Task TestRemoveAgentToken()
        {
            Assert.Inconclusive("Not yet implemented.");
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
                    Console.WriteLine("{0} (Request) {1} {2}", DateTime.Now, e.Request.Method, e.Request.RequestUri);
                };
            provider.AfterAsyncWebResponse +=
                (sender, e) =>
                {
                    Console.WriteLine("{0} (Result {1}) {2}", DateTime.Now, e.Response.StatusCode, e.Response.ResponseUri);
                };

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
                        Console.WriteLine("==> " + result.Item2);

                    return result;
                }
                catch (WebException ex)
                {
                    if (task.Result.ContentLength > 0)
                        Console.WriteLine("==> " + ex.Message);

                    throw;
                }
            }
        }
    }
}
