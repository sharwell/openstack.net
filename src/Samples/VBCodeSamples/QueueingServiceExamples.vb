Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports OpenStack.Collections
Imports OpenStack.ObjectModel.JsonHome
Imports OpenStack.Security.Authentication
Imports OpenStack.Services.Queues.V1
Imports Rackspace.Threading

Public Class QueueingServiceExamples

    Dim region As String = Nothing
    Dim clientId = Guid.NewGuid
    Dim internalUrl = False
    Dim authenticationService As IAuthenticationService = Nothing

#Region "GetHomeAsync"
    Public Async Function PrepareGetHomeAsyncAwait() As Task
        ' #Region "PrepareGetHomeAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim apiCall As GetHomeApiCall = Await queuesService.PrepareGetHomeAsync(CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, HomeDocument) = Await apiCall.SendAsync(CancellationToken.None)
        Dim homeDocument As HomeDocument = apiResponse.Item2
        ' #End Region
    End Function

    Public Sub PrepareGetHome()
        ' #Region "PrepareGetHomeAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim homeDocumentTask As Task(Of HomeDocument) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareGetHomeAsync(CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
        ' #End Region
    End Sub

    Public Async Function GetHomeAsyncAwait() As Task
        ' #Region "GetHomeAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim homeDocument = Await queuesService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetHome()
        ' #Region "GetHomeAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim task = queuesService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Sub
#End Region

#Region "GetNodeHealthAsync"
    Public Async Function PrepareGetNodeHealthAsyncAwait() As Task
        ' #Region "PrepareGetNodeHealthAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim apiCall As GetNodeHealthApiCall = Await queuesService.PrepareGetNodeHealthAsync(CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, Boolean) = Await apiCall.SendAsync(CancellationToken.None)
        Dim operational As Boolean = apiResponse.Item2
        ' #End Region
    End Function

    Public Sub PrepareGetNodeHealth()
        ' #Region "PrepareGetNodeHealthAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim nodeHealthTask As Task(Of Boolean) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareGetNodeHealthAsync(CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
        ' #End Region
    End Sub

    Public Async Function GetNodeHealthAsyncAwait() As Task
        ' #Region "GetNodeHealthAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim operational As Boolean = Await queuesService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetNodeHealth()
        ' #Region "GetNodeHealthAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim task As Task(Of Boolean) = queuesService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Sub
#End Region

#Region "CreateQueueAsync"
    Public Async Function PrepareCreateQueueAsyncAwait() As Task
        ' #Region "PrepareCreateQueueAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim apiCall As CreateQueueApiCall = Await queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, Boolean) = Await apiCall.SendAsync(CancellationToken.None)
        Dim createdQueue As Boolean = apiResponse.Item2
        ' #End Region
    End Function

    Public Sub PrepareCreateQueue()
        ' #Region "PrepareCreateQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim queueCreatedTask As Task(Of Boolean) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareCreateQueueAsync(queueName, CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
        ' #End Region
    End Sub

    Public Async Function CreateQueueAsyncAwait() As Task
        ' #Region "CreateQueueAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim createdQueue = Await queuesService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub CreateQueue()
        ' #Region "CreateQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub
#End Region

#Region "RemoveQueueAsync"
    Public Async Function PrepareRemoveQueueAsyncAwait() As Task
        ' #Region "PrepareRemoveQueueAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim apiCall As RemoveQueueApiCall = Await queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, String) = Await apiCall.SendAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub PrepareRemoveQueue()
        ' #Region "PrepareRemoveQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim queueCreatedTask As Task =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareRemoveQueueAsync(queueName, CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None))
        ' #End Region
    End Sub

    Public Async Function RemoveQueueAsyncAwait() As Task
        ' #Region "RemoveQueueAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Await queuesService.RemoveQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub RemoveQueue()
        ' #Region "RemoveQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.RemoveQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub
#End Region

    Public Async Function ListQueuesAsyncAwait() As Task
        ' #Region "ListQueuesAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queuesPage = Await queuesService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queues = Await queuesPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub ListQueues()
        ' #Region "ListQueuesAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queuesPageTask = queuesService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queuesTask = queuesPageTask _
            .ContinueWith(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing)) _
            .Unwrap()
        ' #End Region
    End Sub

    Public Async Function QueueExistsAsyncAwait() As Task
        ' #Region "QueueExistsAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim exists = Await queuesService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub QueueExists()
        ' #Region "QueueExistsAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

End Class
