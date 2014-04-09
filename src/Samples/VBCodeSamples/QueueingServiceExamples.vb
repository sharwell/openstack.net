Imports net.openstack.Core
Imports net.openstack.Core.Domain
Imports net.openstack.Core.Providers
Imports net.openstack.Core.Domain.Queues
Imports net.openstack.Providers.Rackspace
Imports System.Threading
Imports System.Threading.Tasks

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
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim homeDocument = Await queueingService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetHome()
        ' #Region "GetHomeAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim task = queueingService.GetHomeAsync(CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function GetNodeHealthAsyncAwait() As Task
        ' #Region "GetNodeHealthAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Await queueingService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Function

    Public Sub GetNodeHealth()
        ' #Region "GetNodeHealthAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim task = queueingService.GetNodeHealthAsync(CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function CreateQueueAsyncAwait() As Task
        ' #Region "CreateQueueAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim createdQueue = Await queueingService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub CreateQueue()
        ' #Region "CreateQueueAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queueingService.CreateQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function DeleteQueueAsyncAwait() As Task
        ' #Region "DeleteQueueAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Await queueingService.DeleteQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub DeleteQueue()
        ' #Region "DeleteQueueAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queueingService.DeleteQueueAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function ListQueuesAsyncAwait() As Task
        ' #Region "ListQueuesAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queuesPage = Await queueingService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queues = Await queuesPage.GetAllPagesAsync(CancellationToken.None, Nothing)
        ' #End Region
    End Function

    Public Sub ListQueues()
        ' #Region "ListQueuesAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queuesPageTask = queueingService.ListQueuesAsync(Nothing, Nothing, True, CancellationToken.None)
        Dim queuesTask = queuesPageTask _
            .ContinueWith(Function(task) task.Result.GetAllPagesAsync(CancellationToken.None, Nothing)) _
            .Unwrap()
        ' #End Region
    End Sub

    Public Async Function QueueExistsAsyncAwait() As Task
        ' #Region "QueueExistsAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim exists = Await queueingService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Function

    Public Sub QueueExists()
        ' #Region "QueueExistsAsync (TPL)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")
        Dim task = queueingService.QueueExistsAsync(queueName, CancellationToken.None)
        ' #End Region
    End Sub

    Public Async Function ClaimMessageAsyncAwait() As Task
        ' #Region "ClaimMessageAsync (await)"
        Dim queueingService As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")

        Await queueingService.PostMessagesAsync(queueName, CancellationToken.None, New Message(Of SampleMetadata)(TimeSpan.FromSeconds(120.0), New SampleMetadata(3, "yes")))

        Using claim As Claim = Await queueingService.ClaimMessageAsync(queueName, Nothing, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(1.0), CancellationToken.None)
            ' process the claimed messages
            For Each message As QueuedMessage In claim.Messages
                Dim metadata As SampleMetadata = message.Body.ToObject(Of SampleMetadata)()
                Console.WriteLine("Processed message ({0}, {1})", metadata.ValueA, metadata.ValueB)
            Next

            ' This call deletes the processed messages. Calling this before the claim is released
            ' ensures that the messages will not be re-claimed (which would lead to them being
            ' processed multiple times).
            '
            ' Note: If your code did not process all messages that were claimed, or if you need to
            ' allow some messages to be reclaimed after the current claim is released, do not pass
            ' the IDs of those messages to this call.
            Await queueingService.DeleteMessagesAsync(queueName, claim.Messages.[Select](Function(i As QueuedMessage) i.Id), CancellationToken.None)

            ' Include a call to DisposeAsync before leaving the `using` block if you need to pass
            ' a CancellationToken, or if you simply want to avoid blocking a thread while the
            ' asynchronous operation completes.
            Await claim.DisposeAsync(CancellationToken.None)
        End Using
        ' #End Region
    End Function

    Public Function ClaimMessage() As Task
        ' #Region "ClaimMessageAsync (TPL)"
        Dim provider As IQueueingService = New CloudQueuesProvider(identity, region, clientId, internalUrl, identityProvider)
        Dim queueName = New QueueName("ExampleQueue")

        ' this function asynchronously acquires the Claim
        Dim resource As Func(Of Task(Of Claim)) =
            Function()
                Return provider.ClaimMessageAsync(queueName, Nothing, TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(1.0), CancellationToken.None)
            End Function

        ' this function returns a task representing the asynchronous processing of
        ' claimed messages
        Dim body As Func(Of Task(Of Claim), Task) =
            Function(task) As Task
                Dim claim As Claim = task.Result
                Return Tasks.Task.Factory.StartNew(
                    Sub()
                        ' process the claimed messages
                        For Each message As QueuedMessage In claim.Messages
                            Dim metadata As SampleMetadata = message.Body.ToObject(Of SampleMetadata)()
                            Console.WriteLine("Processed message ({0}, {1})", metadata.ValueA, metadata.ValueB)
                        Next
                    End Sub) _
                .Then(
                    Function(ignored As Task) As Task
                        ' This call deletes the processed messages. Calling this before the claim is released
                        ' ensures that the messages will not be re-claimed (which would lead to them being
                        ' processed multiple times).
                        '
                        ' Note: If your code did not process all messages that were claimed, or if you need to
                        ' allow some messages to be reclaimed after the current claim is released, do not pass
                        ' the IDs of those messages to this call.
                        Return provider.DeleteMessagesAsync(queueName, claim.Messages.Select(Function(i) i.Id), CancellationToken.None)
                    End Function) _
                .Then(
                    Function(ignored As Task) As Task
                        ' Include a call to DisposeAsync before leaving the `using` block if you need to pass
                        ' a CancellationToken, or if you simply want to avoid blocking a thread while the
                        ' asynchronous operation completes.
                        Return claim.DisposeAsync(CancellationToken.None)
                    End Function)
            End Function

        Return provider.PostMessagesAsync(queueName, CancellationToken.None, New Message(Of SampleMetadata)(TimeSpan.FromSeconds(120.0), New SampleMetadata(3, "yes"))) _
            .Then(Function(task As Task) CoreTaskExtensions.Using(resource, body))
        ' #End Region
    End Function

#Region "SampleMetadata"
    Public Class SampleMetadata
        Public Property ValueA() As Integer
        Public Property ValueB() As String

        Public Sub New(valueA As Integer, valueB As String)
            Me.ValueA = valueA
            Me.ValueB = valueB
        End Sub
    End Class
#End Region

End Class
