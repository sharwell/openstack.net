namespace CSharpCodeSamples
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;
    using OpenStack.Collections;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Services.Queues.V1;

    public class QueueingServiceExamples
    {
        CloudIdentity identity =
            new CloudIdentity()
            {
                Username = "MyUser",
                APIKey = "API_KEY_HERE"
            };
        // use the default region for the account
        string region = null;
        // create a new client ID for this instance
        Guid clientId = Guid.NewGuid();
        // access Cloud Queues over the public Internet
        bool internalUrl = false;
        // use a default CloudIdentityProvider for authentication
        IIdentityProvider identityProvider = null;

        public async Task GetHomeAsyncAwait()
        {
            #region GetHomeAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            HomeDocument createdQueue = await queuesService.GetHomeAsync(CancellationToken.None);
            #endregion
        }

        public void GetHome()
        {
            #region GetHomeAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task<HomeDocument> task = queuesService.GetHomeAsync(CancellationToken.None);
            #endregion
        }

        public async Task GetNodeHealthAsyncAwait()
        {
            #region GetNodeHealthAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            await queuesService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }

        public void GetNodeHealth()
        {
            #region GetNodeHealthAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task task = queuesService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }

        public async Task CreateQueueAsyncAwait()
        {
            #region CreateQueueAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            bool createdQueue = await queuesService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void CreateQueue()
        {
            #region CreateQueueAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queuesService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task DeleteQueueAsyncAwait()
        {
            #region DeleteQueueAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            await queuesService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void DeleteQueue()
        {
            #region DeleteQueueAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task task = queuesService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task ListQueuesAsyncAwait()
        {
            #region ListQueuesAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            ReadOnlyCollectionPage<CloudQueue> queuesPage = await queuesService.ListQueuesAsync(null, null, true, CancellationToken.None);
            ReadOnlyCollection<CloudQueue> queues = await queuesPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void ListQueues()
        {
            #region ListQueuesAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task<ReadOnlyCollectionPage<CloudQueue>> queuesPageTask = queuesService.ListQueuesAsync(null, null, true, CancellationToken.None);
            Task<ReadOnlyCollection<CloudQueue>> queuesTask =
                queuesPageTask
                .ContinueWith(task => task.Result.GetAllPagesAsync(CancellationToken.None, null))
                .Unwrap();
            #endregion
        }

        public async Task QueueExistsAsyncAwait()
        {
            #region QueueExistsAsync (await)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            bool exists = await queuesService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void QueueExists()
        {
            #region QueueExistsAsync (TPL)
            IQueuesService queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queuesService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }
    }
}
