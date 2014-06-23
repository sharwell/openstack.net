namespace CSharpCodeSamples
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Queues.V1;
    using Rackspace.Threading;

    public class QueueingServiceExamples
    {
        // use the default region for the account
        string region = null;
        // create a new client ID for this instance
        Guid clientId = Guid.NewGuid();
        // access Cloud Queues over the public Internet
        bool internalUrl = false;
        // use a default RackspaceAuthenticationClient for authentication
        IAuthenticationService authenticationService = null;

        #region GetHomeAsync
        public async Task PrepareGetHomeAsyncAwait()
        {
            #region PrepareGetHomeAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            GetHomeApiCall apiCall = await queuesService.PrepareGetHomeAsync(CancellationToken.None);
            Tuple<HttpResponseMessage, HomeDocument> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            HomeDocument homeDocument = apiResponse.Item2;
            #endregion
        }

        public void PrepareGetHome()
        {
            #region PrepareGetHomeAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<HomeDocument> homeDocumentTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareGetHomeAsync(CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            #endregion
        }

        public async Task GetHomeAsyncAwait()
        {
            #region GetHomeAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            HomeDocument createdQueue = await queuesService.GetHomeAsync(CancellationToken.None);
            #endregion
        }

        public void GetHome()
        {
            #region GetHomeAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<HomeDocument> task = queuesService.GetHomeAsync(CancellationToken.None);
            #endregion
        }
        #endregion

        #region GetNodeHealthAsync
        public async Task PrepareGetNodeHealthAsyncAwait()
        {
            #region PrepareGetNodeHealthAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            GetNodeHealthApiCall apiCall = await queuesService.PrepareGetNodeHealthAsync(CancellationToken.None);
            Tuple<HttpResponseMessage, bool> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            bool operational = apiResponse.Item2;
            #endregion
        }

        public void PrepareGetNodeHealth()
        {
            #region PrepareGetNodeHealthAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<bool> nodeHealthTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareGetNodeHealthAsync(CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            #endregion
        }

        public async Task GetNodeHealthAsyncAwait()
        {
            #region GetNodeHealthAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            bool operational = await queuesService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }

        public void GetNodeHealth()
        {
            #region GetNodeHealthAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<bool> task = queuesService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }
        #endregion

        #region CreateQueueAsync
        public async Task PrepareCreateQueueAsyncAwait()
        {
            #region PrepareCreateQueueAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            CreateQueueApiCall apiCall = await queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None);
            Tuple<HttpResponseMessage, bool> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            bool createdQueue = apiResponse.Item2;
            #endregion
        }

        public void PrepareCreateQueue()
        {
            #region PrepareCreateQueueAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> createQueueTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            #endregion
        }

        public async Task CreateQueueAsyncAwait()
        {
            #region CreateQueueAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            bool createdQueue = await queuesService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void CreateQueue()
        {
            #region CreateQueueAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queuesService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }
        #endregion

        #region RemoveQueueAsync
        public async Task PrepareRemoveQueueAsyncAwait()
        {
            #region PrepareRemoveQueueAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            RemoveQueueApiCall apiCall = await queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None);
            Tuple<HttpResponseMessage, string> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            #endregion
        }

        public void PrepareRemoveQueue()
        {
            #region PrepareRemoveQueueAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task removeQueueTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None));
            #endregion
        }

        public async Task RemoveQueueAsyncAwait()
        {
            #region RemoveQueueAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            await queuesService.RemoveQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void RemoveQueue()
        {
            #region RemoveQueueAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task task = queuesService.RemoveQueueAsync(queueName, CancellationToken.None);
            #endregion
        }
        #endregion

        #region ListQueuesAsync
        public async Task PrepareListQueuesAsyncAwait()
        {
            #region PrepareListQueuesAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            ListQueuesApiCall apiCall = await queuesService.PrepareListQueuesAsync(CancellationToken.None).WithDetails();
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Queue>> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            ReadOnlyCollectionPage<Queue> firstPage = apiResponse.Item2;
            ReadOnlyCollection<Queue> queues = await firstPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void PrepareListQueues()
        {
            #region PrepareListQueuesAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<ReadOnlyCollectionPage<Queue>> listQueuesTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareListQueuesAsync(CancellationToken.None).WithDetails(),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            Task<ReadOnlyCollection<Queue>> allQueuesTask =
                listQueuesTask.Then(task => task.Result.GetAllPagesAsync(CancellationToken.None, null));
            #endregion
        }

        public async Task ListQueuesAsyncAwait()
        {
            #region ListQueuesAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            ReadOnlyCollectionPage<Queue> queuesPage = await queuesService.ListQueuesAsync(true, CancellationToken.None);
            ReadOnlyCollection<Queue> queues = await queuesPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void ListQueues()
        {
            #region ListQueuesAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<ReadOnlyCollectionPage<Queue>> queuesPageTask = queuesService.ListQueuesAsync(true, CancellationToken.None);
            Task<ReadOnlyCollection<Queue>> queuesTask =
                queuesPageTask.Then(task => task.Result.GetAllPagesAsync(CancellationToken.None, null));
            #endregion
        }
        #endregion

        #region QueueExistsAsync
        public async Task PrepareQueueExistsAsyncAwait()
        {
            #region PrepareQueueExistsAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            QueueExistsApiCall apiCall = await queuesService.PrepareQueueExistsAsync(queueName, CancellationToken.None);
            Tuple<HttpResponseMessage, bool> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            bool queueExists = apiResponse.Item2;
            #endregion
        }

        public void PrepareQueueExists()
        {
            #region PrepareQueueExistsAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> queueExistsTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareQueueExistsAsync(queueName, CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            #endregion
        }

        public async Task QueueExistsAsyncAwait()
        {
            #region QueueExistsAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            bool exists = await queuesService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void QueueExists()
        {
            #region QueueExistsAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queuesService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }
        #endregion

        #region ListMessagesAsync
        public async Task PrepareListMessagesAsyncAwait()
        {
            #region PrepareListMessagesAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            ListMessagesApiCall apiCall = await queuesService.PrepareListMessagesAsync(queueName, CancellationToken.None);
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<QueuedMessage>> apiResponse = await apiCall.SendAsync(CancellationToken.None);
            ReadOnlyCollectionPage<QueuedMessage> firstPage = apiResponse.Item2;
            ReadOnlyCollection<QueuedMessage> messages = await firstPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void PrepareListMessages()
        {
            #region PrepareListMessagesAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<ReadOnlyCollectionPage<QueuedMessage>> listMessagesTask =
                CoreTaskExtensions.Using(
                    () => queuesService.PrepareListMessagesAsync(queueName, CancellationToken.None),
                    task => task.Result.SendAsync(CancellationToken.None))
                .Select(task => task.Result.Item2);
            Task<ReadOnlyCollection<QueuedMessage>> allMessagesTask =
                listMessagesTask.Then(task => task.Result.GetAllPagesAsync(CancellationToken.None, null));
            #endregion
        }

        public async Task ListMessagesAsyncAwait()
        {
            #region ListMessagesAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            ReadOnlyCollectionPage<QueuedMessage> messagesPage = await queuesService.ListMessagesAsync(queueName, CancellationToken.None);
            ReadOnlyCollection<QueuedMessage> messages = await messagesPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void ListMessages()
        {
            #region ListMessagesAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<ReadOnlyCollectionPage<QueuedMessage>> messagesPageTask = queuesService.ListMessagesAsync(queueName, CancellationToken.None);
            Task<ReadOnlyCollection<QueuedMessage>> messagesTask =
                messagesPageTask.Then(task => task.Result.GetAllPagesAsync(CancellationToken.None, null));
            #endregion
        }
        #endregion
    }
}
