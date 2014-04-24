Imports System.Threading
Imports System.Threading.Tasks
Imports net.openstack.Core.Domain
Imports net.openstack.Core.Providers
Imports net.openstack.Providers.Rackspace
Imports OpenStack.Collections
Imports OpenStack.Services.Queues.V1

Public Class QueueingServiceExamples

    Dim identity = New CloudIdentity With
                   {
                       .Username = "MyUser",
                       .APIKey = "API_KEY_HERE"
                   }
    Dim region As String = Nothing
    Dim clientId = Guid.NewGuid
    Dim internalUrl = False
    Dim identityProvider As IIdentityProvider = Nothing

    Public Async Function GetHomeAsyncAwait() As Task
        ' #Region "GetHomeAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim homeDocument = Await queuesService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetHome()
        ' #Region "GetHomeAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim task = queuesService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function GetNodeHealthAsyncAwait() As Task
        ' #Region "GetNodeHealthAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Await queuesService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetNodeHealth()
        ' #Region "GetNodeHealthAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim task = queuesService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function CreateQueueAsyncAwait() As Task
        ' #Region "CreateQueueAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim createdQueue = Await queuesService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub CreateQueue()
        ' #Region "CreateQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function DeleteQueueAsyncAwait() As Task
        ' #Region "DeleteQueueAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Await queuesService.DeleteQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub DeleteQueue()
        ' #Region "DeleteQueueAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.DeleteQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function ListQueuesAsyncAwait() As Task
        ' #Region "ListQueuesAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queuesPage = Await queuesService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queues = Await queuesPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub ListQueues()
        ' #Region "ListQueuesAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queuesPageTask = queuesService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queuesTask = queuesPageTask _
            .ContinueWith(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing)) _
            .Unwrap()
        ' #End Region
    End Sub

    Public Async Function QueueExistsAsyncAwait() As Task
        ' #Region "QueueExistsAsync (await)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim exists = Await queuesService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub QueueExists()
        ' #Region "QueueExistsAsync (TPL)"
        Dim queuesService As IQueuesService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queuesService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

End Class
