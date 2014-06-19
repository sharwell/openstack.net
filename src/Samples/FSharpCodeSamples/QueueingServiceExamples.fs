﻿module QueueingServiceExamples

open System
open System.Net.Http
open System.Threading
open System.Threading.Tasks
open OpenStack.Collections
open OpenStack.ObjectModel.JsonHome
open OpenStack.Security.Authentication
open OpenStack.Services.Queues.V1
open Rackspace.Threading

let region : string = null
let clientId = Guid.NewGuid()
let internalUrl = false
let authenticationService : IAuthenticationService = null

//
// GetHomeAsync
//

let prepareGetHomeAsyncAwait =
    async {
        //#region PrepareGetHomeAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let! apiCall = queuesService.PrepareGetHomeAsync(CancellationToken.None) |> Async.AwaitTask
        let! responseMessage, homeDocument = apiCall.SendAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let prepareGetHome =
    //#region PrepareGetHomeAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let homeDocumentTask =
        CoreTaskExtensions.Using(
            (fun () -> queuesService.PrepareGetHomeAsync(CancellationToken.None)),
            (fun (task : Task<GetHomeApiCall>) -> task.Result.SendAsync(CancellationToken.None)))
          .Select(fun (task : Task<HttpResponseMessage * HomeDocument>) -> snd task.Result)
    //#endregion
    ()

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

//
// GetNodeHealthAsync
//

let prepareGetNodeHealthAsyncAwait =
    async {
        //#region PrepareGetNodeHealthAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let! apiCall = queuesService.PrepareGetNodeHealthAsync(CancellationToken.None) |> Async.AwaitTask
        let! responseMessage, operational = apiCall.SendAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let prepareGetNodeHealth =
    //#region PrepareGetNodeHealthAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let nodeHealthTask =
        CoreTaskExtensions.Using(
            (fun () -> queuesService.PrepareGetNodeHealthAsync(CancellationToken.None)),
            (fun (task : Task<GetNodeHealthApiCall>) -> task.Result.SendAsync(CancellationToken.None)))
          .Select(fun (task : Task<HttpResponseMessage * bool>) -> snd task.Result)
    //#endregion
    ()

let getNodeHealthAsyncAwait =
    async {
        //#region GetNodeHealthAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let operational = queuesService.GetNodeHealthAsync(CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let getNodeHealth =
    //#region GetNodeHealthAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let task = queuesService.GetNodeHealthAsync(CancellationToken.None)
    //#endregion
    ()

//
// CreateQueueAsync
//

let prepareCreateQueueAsyncAwait =
    async {
        //#region PrepareCreateQueueAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        let! apiCall = queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        let! responseMessage, createdQueue = apiCall.SendAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let prepareCreateQueue =
    //#region PrepareCreateQueueAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let createdQueueTask =
        CoreTaskExtensions.Using(
            (fun () -> queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None)),
            (fun (task : Task<CreateQueueApiCall>) -> task.Result.SendAsync(CancellationToken.None)))
          .Select(fun (task : Task<HttpResponseMessage * bool>) -> snd task.Result)
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

//
// RemoveQueueAsync
//

let prepareRemoveQueueAsyncAwait =
    async {
        //#region PrepareRemoveQueueAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        let! apiCall = queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None) |> Async.AwaitTask
        let! responseMessage, rawBody = apiCall.SendAsync(CancellationToken.None) |> Async.AwaitTask
        //#endregion
        ()
    }

let prepareRemoveQueue =
    //#region PrepareRemoveQueueAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let task =
        CoreTaskExtensions.Using(
            (fun () -> queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None)),
            (fun (task : Task<RemoveQueueApiCall>) -> task.Result.SendAsync(CancellationToken.None)))
    //#endregion
    ()

let removeQueueAsyncAwait =
    async {
        //#region RemoveQueueAsync (await)
        let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
        let queueName = new QueueName("ExampleQueue")
        queuesService.RemoveQueueAsync(queueName, CancellationToken.None) |> Async.AwaitIAsyncResult |> ignore
        //#endregion
        ()
    }

let removeQueue =
    //#region RemoveQueueAsync (TPL)
    let queuesService = new QueuesClient(authenticationService, region, clientId, internalUrl)
    let queueName = new QueueName("ExampleQueue")
    let task = queuesService.RemoveQueueAsync(queueName, CancellationToken.None)
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
