﻿namespace CSharpCodeSamples
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

        public async Task DeleteQueueAsyncAwait()
        {
            #region DeleteQueueAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            await queuesService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void DeleteQueue()
        {
            #region DeleteQueueAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            QueueName queueName = new QueueName("ExampleQueue");
            Task task = queuesService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task ListQueuesAsyncAwait()
        {
            #region ListQueuesAsync (await)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            ReadOnlyCollectionPage<Queue> queuesPage = await queuesService.ListQueuesAsync(null, null, true, CancellationToken.None);
            ReadOnlyCollection<Queue> queues = await queuesPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void ListQueues()
        {
            #region ListQueuesAsync (TPL)
            IQueuesService queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl);
            Task<ReadOnlyCollectionPage<Queue>> queuesPageTask = queuesService.ListQueuesAsync(null, null, true, CancellationToken.None);
            Task<ReadOnlyCollection<Queue>> queuesTask =
                queuesPageTask
                .ContinueWith(task => task.Result.GetAllPagesAsync(CancellationToken.None, null))
                .Unwrap();
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
    }
}
