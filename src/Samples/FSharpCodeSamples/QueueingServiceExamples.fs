module QueueingServiceExamples

open System
open System.Threading
open System.Threading.Tasks
open OpenStack.Collections
open OpenStack.ObjectModel.JsonHome
open OpenStack.Security.Authentication
open OpenStack.Services.Queues.V1

let region : string = null
let clientId = Guid.NewGuid()
let internalUrl = false
let authenticationService : IAuthenticationService = null

let getHomeAsyncAwait =
    async {
        //#region GetHomeAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let! homeDocument = queuesService.GetHomeAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let getHome =
    //#region GetHomeAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let task = queuesService.GetHomeAsync(CancellationToken.None)
    //#endregion
    ()

let getNodeHealthAsyncAwait =
    async {
        //#region GetNodeHealthAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        queuesService.GetNodeHealthAsync(CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let getNodeHealth =
    //#region GetNodeHealthAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let task = queuesService.GetNodeHealthAsync(CancellationToken.None)
    //#endregion
    ()

let createQueueAsyncAwait =
    async {
        //#region CreateQueueAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        let! createdQueue = queuesService.CreateQueueAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let createQueue =
    //#region CreateQueueAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.CreateQueueAsync(queueName, CancellationToken.None)
    //#endregion
    ()

let deleteQueueAsyncAwait =
    async {
        //#region DeleteQueueAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        queuesService.DeleteQueueAsync(queueName, CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let deleteQueue =
    //#region DeleteQueueAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.DeleteQueueAsync(queueName, CancellationToken.None)
    //#endregion
    ()

let listQueuesAsyncAwait =
    async {
        //#region ListQueuesAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let! queuesPage = queuesService.ListQueuesAsync(null, Nullable(), true, CancellationToken.None) |> Async.AwaitTask
        let! queues = queuesPage.GetAllPagesAsync(CancellationToken.None, null) |> Async.AwaitTask
        //#endregion
        ()
    }

let listQueues =
    //#region ListQueuesAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queuesPageTask = queuesService.ListQueuesAsync(null, Nullable(), true, CancellationToken.None)
    let queuesTask = queuesPageTask.ContinueWith(fun (task:Task<ReadOnlyCollectionPage<Queue>>) -> task.Result.GetAllPagesAsync(CancellationToken.None, null)) |> TaskExtensions.Unwrap
    //#endregion
    ()

let queueExistsAsyncAwait =
    async {
        //#region QueueExistsAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        let! exists = queuesService.QueueExistsAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let queueExists =
    //#region QueueExistsAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.QueueExistsAsync(queueName, CancellationToken.None)
    //#endregion
    ()
