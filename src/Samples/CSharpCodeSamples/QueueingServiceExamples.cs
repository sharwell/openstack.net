namespace CSharpCodeSamples
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain;
    using net.openstack.Core.Domain.Queues;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace;

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
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            HomeDocument createdQueue = await queueingService.GetHomeAsync(CancellationToken.None);
            #endregion
        }

        public void GetHome()
        {
            #region GetHomeAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task<HomeDocument> task = queueingService.GetHomeAsync(CancellationToken.None);
            #endregion
        }

        public async Task GetNodeHealthAsyncAwait()
        {
            #region GetNodeHealthAsync (await)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            await queueingService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }

        public void GetNodeHealth()
        {
            #region GetNodeHealthAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task task = queueingService.GetNodeHealthAsync(CancellationToken.None);
            #endregion
        }

        public async Task CreateQueueAsyncAwait()
        {
            #region CreateQueueAsync (await)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            bool createdQueue = await queueingService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void CreateQueue()
        {
            #region CreateQueueAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queueingService.CreateQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task DeleteQueueAsyncAwait()
        {
            #region DeleteQueueAsync (await)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            await queueingService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void DeleteQueue()
        {
            #region DeleteQueueAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task task = queueingService.DeleteQueueAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task ListQueuesAsyncAwait()
        {
            #region ListQueuesAsync (await)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            ReadOnlyCollectionPage<CloudQueue> queuesPage = await queueingService.ListQueuesAsync(null, null, true, CancellationToken.None);
            ReadOnlyCollection<CloudQueue> queues = await queuesPage.GetAllPagesAsync(CancellationToken.None, null);
            #endregion
        }

        public void ListQueues()
        {
            #region ListQueuesAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            Task<ReadOnlyCollectionPage<CloudQueue>> queuesPageTask = queueingService.ListQueuesAsync(null, null, true, CancellationToken.None);
            Task<ReadOnlyCollection<CloudQueue>> queuesTask =
                queuesPageTask
                .ContinueWith(task => task.Result.GetAllPagesAsync(CancellationToken.None, null))
                .Unwrap();
            #endregion
        }

        public async Task QueueExistsAsyncAwait()
        {
            #region QueueExistsAsync (await)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            bool exists = await queueingService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }

        public void QueueExists()
        {
            #region QueueExistsAsync (TPL)
            IQueueingService queueingService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");
            Task<bool> task = queueingService.QueueExistsAsync(queueName, CancellationToken.None);
            #endregion
        }

        public async Task ClaimMessageAsyncAwait()
        {
            #region ClaimMessageAsync (await)
            IQueueingService provider = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");

            await provider.PostMessagesAsync(queueName, CancellationToken.None, new Message<SampleMetadata>(TimeSpan.FromSeconds(120), new SampleMetadata(3, "yes")));

            using (Claim claim = await provider.ClaimMessageAsync(queueName, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(1), CancellationToken.None))
            {
                // process the claimed messages
                foreach (QueuedMessage message in claim.Messages)
                {
                    SampleMetadata metadata = message.Body.ToObject<SampleMetadata>();

                    Console.WriteLine("Processed message ({0}, {1})", metadata.ValueA, metadata.ValueB);
                }

                /* This call deletes the processed messages. Calling this before the claim is released
                 * ensures that the messages will not be re-claimed (which would lead to them being
                 * processed multiple times).
                 *
                 * Note: If your code did not process all messages that were claimed, or if you need to
                 * allow some messages to be reclaimed after the current claim is released, do not pass
                 * the IDs of those messages to this call.
                 */
                await provider.DeleteMessagesAsync(queueName, claim.Messages.Select(i => i.Id), CancellationToken.None);

                /* Include a call to DisposeAsync before leaving the `using` block if you need to pass
                 * a CancellationToken, or if you simply want to avoid blocking a thread while the
                 * asynchronous operation completes.
                 */
                await claim.DisposeAsync(CancellationToken.None);
            }
            #endregion
        }

        public Task ClaimMessage()
        {
            #region ClaimMessageAsync (TPL)
            IQueueingService provider = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider);
            QueueName queueName = new QueueName("ExampleQueue");

            // this function asynchronously acquires the Claim
            Func<Task<Claim>> resource = () => provider.ClaimMessageAsync(queueName, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(1), CancellationToken.None);
            // this function returns a task representing the asynchronous processing of
            // claimed messages
            Func<Task<Claim>, Task> body =
                task =>
                {
                    Claim claim = task.Result;
                    return Task.Factory.StartNew(
                        () =>
                        {
                            // process the claimed messages
                            foreach (QueuedMessage message in claim.Messages)
                            {
                                SampleMetadata metadata = message.Body.ToObject<SampleMetadata>();
                                Console.WriteLine("Processed message ({0}, {1})", metadata.ValueA, metadata.ValueB);
                            }
                        })
                        /* This call deletes the processed messages. Calling this before the claim is released
                         * ensures that the messages will not be re-claimed (which would lead to them being
                         * processed multiple times).
                         *
                         * Note: If your code did not process all messages that were claimed, or if you need to
                         * allow some messages to be reclaimed after the current claim is released, do not pass
                         * the IDs of those messages to this call.
                         */
                        .Then(_ => provider.DeleteMessagesAsync(queueName, claim.Messages.Select(i => i.Id), CancellationToken.None))
                        /* Include a call to DisposeAsync before leaving the `using` block if you need to pass
                         * a CancellationToken, or if you simply want to avoid blocking a thread while the
                         * asynchronous operation completes.
                         */
                        .Then(_ => claim.DisposeAsync(CancellationToken.None));
                };

            return
                provider.PostMessagesAsync(queueName, CancellationToken.None, new Message<SampleMetadata>(TimeSpan.FromSeconds(120), new SampleMetadata(3, "yes")))
                .Then(task => CoreTaskExtensions.Using(resource, body));
            #endregion
        }

        #region SampleMetadata
        public class SampleMetadata
        {
            public SampleMetadata(int valueA, string valueB)
            {
                ValueA = valueA;
                ValueB = valueB;
            }

            public int ValueA
            {
                get;
                private set;
            }

            public string ValueB
            {
                get;
                private set;
            }
        }
        #endregion
    }
}
