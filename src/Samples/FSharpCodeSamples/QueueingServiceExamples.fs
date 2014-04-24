module QueueingServiceExamples

open System
open System.Threading
open System.Threading.Tasks
open net.openstack.Core.Domain
open net.openstack.Core.Providers
open net.openstack.Providers.Rackspace
open OpenStack.Collections
open OpenStack.Services.Queues.V1

let identity = new CloudIdentity (Username = "MyUser", APIKey = "API_KEY_HERE")
let region : string = null
let clientId = Guid.NewGuid()
let internalUrl = false
let identityProvider : IIdentityProvider = null

let getHomeAsyncAwait =
    async {
        //#region GetHomeAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        let! homeDocument = queuesService.GetHomeAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let getHome =
    //#region GetHomeAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let task = queuesService.GetHomeAsync(CancellationToken.None)
    //#endregion
    ()

let getNodeHealthAsyncAwait =
    async {
        //#region GetNodeHealthAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        queuesService.GetNodeHealthAsync(CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let getNodeHealth =
    //#region GetNodeHealthAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let task = queuesService.GetNodeHealthAsync(CancellationToken.None)
    //#endregion
    ()

let createQueueAsyncAwait =
    async {
        //#region CreateQueueAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        let queueName = new QueueName("ExampleQueue")
        let! createdQueue = queuesService.CreateQueueAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let createQueue =
    //#region CreateQueueAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.CreateQueueAsync(queueName, CancellationToken.None)
    //#endregion
    ()

let deleteQueueAsyncAwait =
    async {
        //#region DeleteQueueAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        let queueName = new QueueName("ExampleQueue")
        queuesService.DeleteQueueAsync(queueName, CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let deleteQueue =
    //#region DeleteQueueAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.DeleteQueueAsync(queueName, CancellationToken.None)
    //#endregion
    ()

let listQueuesAsyncAwait =
    async {
        //#region ListQueuesAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        let! queuesPage = queuesService.ListQueuesAsync(null, Nullable(), true, CancellationToken.None) |> Async.AwaitTask
        let! queues = queuesPage.GetAllPagesAsync(CancellationToken.None, null) |> Async.AwaitTask
        //#endregion
        ()
    }

let listQueues =
    //#region ListQueuesAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let queuesPageTask = queuesService.ListQueuesAsync(null, Nullable(), true, CancellationToken.None)
    let queuesTask = queuesPageTask.ContinueWith(fun (task:Task<ReadOnlyCollectionPage<CloudQueue>>) -> task.Result.GetAllPagesAsync(CancellationToken.None, null)) |> TaskExtensions.Unwrap
    //#endregion
    ()

let queueExistsAsyncAwait =
    async {
        //#region QueueExistsAsync (await)
        let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        let queueName = new QueueName("ExampleQueue")
        let! exists = queuesService.QueueExistsAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let queueExists =
    //#region QueueExistsAsync (TPL)
    let queuesService = new CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.QueueExistsAsync(queueName, CancellationToken.None)
    //#endregion
    ()
