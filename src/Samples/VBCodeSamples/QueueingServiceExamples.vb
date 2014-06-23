Imports System.Collections.ObjectModel
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

#Region "ListQueuesAsync"
    Public Async Function PrepareListQueuesAsyncAwait() As Task
        ' #Region "PrepareListQueuesAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim apiCall As ListQueuesApiCall = Await queuesService.PrepareListQueuesAsync(CancellationToken.None).WithDetails()
        Dim apiResponse As Tuple(Of HttpResponseMessage, ReadOnlyCollectionPage(Of Queue)) = Await apiCall.SendAsync(CancellationToken.None)
        Dim firstPage As ReadOnlyCollectionPage(Of Queue) = apiResponse.Item2
        Dim queues As ReadOnlyCollection(Of Queue) = Await firstPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub PrepareListQueues()
        ' #Region "PrepareListQueuesAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim listQueuesTask As Task(Of ReadOnlyCollectionPage(Of Queue)) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareListQueuesAsync(CancellationToken.None).WithDetails(),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
        Dim allQueuesTask As Task(Of ReadOnlyCollection(Of Queue)) =
            listQueuesTask.Then(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing))
        ' #End Region
    End Sub

    Public Async Function ListQueuesAsyncAwait() As Task
        ' #Region "ListQueuesAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queuesPage = Await queuesService.ListQueuesAsync(True, CancellationToken.None)
        Dim queues = Await queuesPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub ListQueues()
        ' #Region "ListQueuesAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queuesPageTask = queuesService.ListQueuesAsync(True, CancellationToken.None)
        Dim queuesTask = queuesPageTask.Then(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing))
        ' #End Region
    End Sub
#End Region

#Region "QueueExistsAsync"
    Public Async Function PrepareQueueExistsAsyncAwait() As Task
        ' #Region "PrepareQueueExistsAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim apiCall As QueueExistsApiCall = Await queuesService.PrepareQueueExistsAsync(queueName, CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, Boolean) = Await apiCall.SendAsync(CancellationToken.None)
        Dim queueExists As Boolean = apiResponse.Item2
        ' #End Region
    End Function

    Public Sub PrepareQueueExists()
        ' #Region "PrepareQueueExistsAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim queueExistsTask As Task(Of Boolean) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareQueueExistsAsync(queueName, CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
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
#End Region

#Region "ListMessagesAsync"
    Public Async Function PrepareListMessagesAsyncAwait() As Task
        ' #Region "PrepareListMessagesAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim apiCall As ListMessagesApiCall = Await queuesService.PrepareListMessagesAsync(queueName, CancellationToken.None)
        Dim apiResponse As Tuple(Of HttpResponseMessage, ReadOnlyCollectionPage(Of QueuedMessage)) = Await apiCall.SendAsync(CancellationToken.None)
        Dim firstPage As ReadOnlyCollectionPage(Of QueuedMessage) = apiResponse.Item2
        Dim messages As ReadOnlyCollection(Of QueuedMessage) = Await firstPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub PrepareListMessages()
        ' #Region "PrepareListMessagesAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim listMessagesTask As Task(Of ReadOnlyCollectionPage(Of QueuedMessage)) =
            CoreTaskExtensions.Using(
                Function() queuesService.PrepareListMessagesAsync(queueName, CancellationToken.None),
                Function(task) task.Result.SendAsync(CancellationToken.None)) _
            .Select(Function(task) task.Result.Item2)
        Dim allMessagesTask As Task(Of ReadOnlyCollection(Of QueuedMessage)) =
            listMessagesTask.Then(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing))
        ' #End Region
    End Sub

    Public Async Function ListMessagesAsyncAwait() As Task
        ' #Region "ListMessagesAsync (await)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim messagesPage = Await queuesService.ListMessagesAsync(queueName, CancellationToken.None)
        Dim messages = Await messagesPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub ListMessages()
        ' #Region "ListMessagesAsync (TPL)"
        Dim queuesService As IQueuesService = New QueuesClient(authenticationService, region, clientId, internalUrl)
        Dim queueName = New QueueName("ExampleQueue")
        Dim messagesPageTask = queuesService.ListMessagesAsync(queueName, CancellationToken.None)
        Dim messagesTask = messagesPageTask.Then(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing))
        ' #End Region
    End Sub
#End Region

End Class
