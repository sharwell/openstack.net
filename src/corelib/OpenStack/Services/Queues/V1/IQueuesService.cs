namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using CancellationToken = System.Threading.CancellationToken;
    using JsonSerializationException = Newtonsoft.Json.JsonSerializationException;
    using WebException = System.Net.WebException;

    /// <summary>
    /// Represents a provider for asynchronous operations on the OpenStack Marconi (Cloud Queues) Service.
    /// </summary>
    /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1&amp;oldid=30943">OpenStack Marconi API v1 Blueprint</seealso>
    /// <preliminary/>
    public interface IQueuesService : IHttpService
    {
        #region Base endpoints

        /// <summary>
        /// Prepare an API call to get a <see cref="HomeDocument"/> describing the operations supported by the service.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the client, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareGetHomeAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareGetHomeAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareGetHomeAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareGetHomeAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareGetHomeAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="PrepareGetHomeAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareGetHomeAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="GetHomeApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.GetHomeAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Home_Document">Get Home Document (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<GetHomeApiCall> PrepareGetHomeAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to check the queueing service node status.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the client, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareGetNodeHealthAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareGetNodeHealthAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareGetNodeHealthAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareGetNodeHealthAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareGetNodeHealthAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="PrepareGetNodeHealthAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareGetNodeHealthAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="GetNodeHealthApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.GetNodeHealthAsync"/>
        /// <seealso href="https://wiki.openstack.org/wiki/Marconi/specs/api/v1#Check_Node_Health">Check Node Health (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<GetNodeHealthApiCall> PrepareGetNodeHealthAsync(CancellationToken cancellationToken);

        #endregion Base endpoints

        #region Queues

        /// <summary>
        /// Prepare an API call to create a queue.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareCreateQueueAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareCreateQueueAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareCreateQueueAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareCreateQueueAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareCreateQueueAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="PrepareCreateQueueAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareCreateQueueAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="CreateQueueApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.CreateQueueAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Create_Queue">Create Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<CreateQueueApiCall> PrepareCreateQueueAsync(QueueName queueName, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a list of queues.
        /// </summary>
        /// <param name="marker">The name of the last queue in the previous list. The resulting collection of queues will start with the first queue <em>after</em> this value, when sorted using <see cref="StringComparer.Ordinal"/>. If this value is <see langword="null"/>, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of queues to return. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="detailed"><see langword="true"/> to return detailed information about each queue; otherwise, <see langword="false"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will contain <placeholder>placeholder</placeholder>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="limit"/> is less than or equal to 0.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="ListQueuesAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="ListQueuesAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="ListQueuesAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="ListQueuesAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="ListQueuesAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="ListQueuesAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="ListQueuesAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#List_Queues">List Queues (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<ReadOnlyCollectionPage<Queue>> ListQueuesAsync(QueueName marker, int? limit, bool detailed, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to check for the existence of a queue with a particular name.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareQueueExistsAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareQueueExistsAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareQueueExistsAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareQueueExistsAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareQueueExistsAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="PrepareQueueExistsAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareQueueExistsAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="QueueExistsApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.QueueExistsAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Checking_Queue_Existence">Checking Queue Existence (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<QueueExistsApiCall> PrepareQueueExistsAsync(QueueName queueName, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to remove a queue.
        /// </summary>
        /// <remarks>
        /// The queue will be removed whether or not it is empty, even if one or more messages in the queue is currently claimed.
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareRemoveQueueAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareRemoveQueueAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareRemoveQueueAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="PrepareRemoveQueueAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="PrepareRemoveQueueAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="PrepareRemoveQueueAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="PrepareRemoveQueueAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="RemoveQueueApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.RemoveQueueAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Queue">Delete Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<RemoveQueueApiCall> PrepareRemoveQueueAsync(QueueName queueName, CancellationToken cancellationToken);

        #endregion

        #region Queue metadata

        /// <summary>
        /// Prepare an API call to set the metadata associated with a queue.
        /// </summary>
        /// <typeparam name="T">The type modeling the metadata to associate with the queue.</typeparam>
        /// <param name="queueName">The queue name.</param>
        /// <param name="metadata">The metadata to associate with the queue.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="SetQueueMetadataApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.SetQueueMetadataAsync{T}"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Set_Queue_Metadata">Set Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<SetQueueMetadataApiCall> PrepareSetQueueMetadataAsync<T>(QueueName queueName, T metadata, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get the metadata associated with a queue.
        /// </summary>
        /// <typeparam name="T">The type modeling the metadata associated with the queue. Use <see cref="JObject"/> for generic JSON metadata.</typeparam>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GetQueueMetadataApiCall{T}"/>
        /// <seealso cref="QueuesServiceExtensions.GetQueueMetadataAsync{T}"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Metadata">Get Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<GetQueueMetadataApiCall<T>> PrepareGetQueueMetadataAsync<T>(QueueName queueName, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get statistics for a queue.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GetQueueStatisticsApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.GetQueueStatisticsAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Stats">Get Queue Stats (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<GetQueueStatisticsApiCall> PrepareGetQueueStatisticsAsync(QueueName queueName, CancellationToken cancellationToken);

        #endregion Queue metadata

        #region Messages

        /// <summary>
        /// Gets a list of messages currently in a queue.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="marker">The identifier of the message list page to return. This is obtained from <see cref="QueuedMessageList.NextPageId"/>. If this value is <see langword="null"/>, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of messages to return. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="echo"><see langword="true"/> to include messages created by the current client; otherwise, <see langword="false"/>.</param>
        /// <param name="includeClaimed"><see langword="true"/> to include claimed messages; otherwise <see langword="false"/> to return only unclaimed messages.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will contain a collection of <see cref="QueuedMessage"/> objects describing the messages in the queue.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="limit"/> is less than or equal to 0.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#List_Messages">List Messages (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<QueuedMessageList> ListMessagesAsync(QueueName queueName, QueuedMessageListId marker, int? limit, bool echo, bool includeClaimed, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to get detailed information about a specific queued message.
        /// </summary>
        /// <remarks>
        /// This method will return information for the specified message regardless of the
        /// <literal>Client-ID</literal> or claim associated with the message.
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The ID of the message.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="GetMessageApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.GetMessageAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_a_Specific_Message">Get a Specific Message (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<GetMessageApiCall> PrepareGetMessageAsync(QueueName queueName, MessageId messageId, CancellationToken cancellationToken);

        /// <summary>
        /// Get messages from a queue.
        /// </summary>
        /// <remarks>
        /// This method will return information for the specified message regardless of the
        /// <literal>Client-ID</literal> or claim associated with the message.
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageIds">The message IDs of messages to get.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will contain a collection of <see cref="QueuedMessage"/> objects containing detailed information about the specified messages.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageIds"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messageIds"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_a_Set_of_Messages_by_ID">Get a Set of Messages by ID (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<ReadOnlyCollection<QueuedMessage>> GetMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken);

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <remarks>
        /// If <paramref name="messages"/> is empty, this call is equivalent to <see cref="QueuesServiceExtensions.QueueExistsAsync"/>.
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messages"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messages"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Post_Message.28s.29">Post Message(s) (OpenStack Marconi API v1 Blueprint)</seealso>
        Task PostMessagesAsync(QueueName queueName, IEnumerable<Message> messages, CancellationToken cancellationToken);

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <remarks>
        /// If <paramref name="messages"/> is empty, this call is equivalent to <see cref="QueuesServiceExtensions.QueueExistsAsync"/>.
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="messages">The messages to post.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messages"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messages"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Post_Message.28s.29">Post Message(s) (OpenStack Marconi API v1 Blueprint)</seealso>
        Task PostMessagesAsync(QueueName queueName, CancellationToken cancellationToken, params Message[] messages);

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <remarks>
        /// If <paramref name="messages"/> is empty, this call is equivalent to <see cref="QueuesServiceExtensions.QueueExistsAsync"/>.
        /// </remarks>
        /// <typeparam name="T">The class modeling the JSON representation of the messages to post in the queue.</typeparam>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messages"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messages"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Post_Message.28s.29">Post Message(s) (OpenStack Marconi API v1 Blueprint)</seealso>
        Task PostMessagesAsync<T>(QueueName queueName, IEnumerable<Message<T>> messages, CancellationToken cancellationToken);

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <remarks>
        /// If <paramref name="messages"/> is empty, this call is equivalent to <see cref="QueuesServiceExtensions.QueueExistsAsync"/>.
        /// </remarks>
        /// <typeparam name="T">The class modeling the JSON representation of the messages to post in the queue.</typeparam>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="messages">The messages to post.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messages"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messages"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Post_Message.28s.29">Post Message(s) (OpenStack Marconi API v1 Blueprint)</seealso>
        Task PostMessagesAsync<T>(QueueName queueName, CancellationToken cancellationToken, params Message<T>[] messages);

        /// <summary>
        /// Prepares an API call to remove a message from a queue.
        /// </summary>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The ID of the message to remove.</param>
        /// <param name="claimId">The ID of the claim for the message. If this value is <see langword="null"/>, the remove operation will fail if the message is claimed. If this value is non-<see langword="null"/>, the remove operation will fail if the message is not claimed by the specified claim.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="RemoveMessageApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.RemoveMessageAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Message">Delete Message (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<RemoveMessageApiCall> PrepareRemoveMessageAsync(QueueName queueName, MessageId messageId, ClaimId claimId, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes messages from a queue.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// This method deletes messages from a queue whether or not they are currently claimed.
        /// </note>
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageIds">The IDs of messages to delete. These are obtained from <see cref="QueuedMessage.Id">QueuedMessage.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageIds"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="messageIds"/> contains a <see langword="null"/> value.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_a_Set_of_Messages_by_ID">Delete a Set of Messages by ID (OpenStack Marconi API v1 Blueprint)</seealso>
        Task DeleteMessagesAsync(QueueName queueName, IEnumerable<MessageId> messageIds, CancellationToken cancellationToken);

        #endregion Messages

        #region Claims

        /// <summary>
        /// Claim messages from a queue.
        /// </summary>
        /// <remarks>
        /// When the claim is no longer required, the code should call <see cref="Claim.DisposeAsync"/>
        /// or <see cref="Claim.Dispose()"/> to ensure the following actions are taken.
        /// <list type="bullet">
        /// <item>Messages which are part of this claim which were not processed are made available to other nodes.</item>
        /// <item>The claim resource is cleaned up without waiting for the time-to-live to expire.</item>
        /// </list>
        ///
        /// <para>Messages which are not deleted before the claim is released will be eligible for
        /// reclaiming by another process.</para>
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="limit">The maximum number of messages to claim. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="timeToLive">The time to wait before the server automatically releases the claim.</param>
        /// <param name="gracePeriod">The time to wait, after the time-to-live for the claim expires, before the server allows the claimed messages to be deleted due to the individual message's time-to-live expiring.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will contain <see cref="Claim"/> object representing the claim.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="limit"/> is less than or equal to 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="timeToLive"/> is negative or <see cref="TimeSpan.Zero"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="gracePeriod"/> is negative.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Claim_Messages">Claim Messages (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<Claim> ClaimMessageAsync(QueueName queueName, int? limit, TimeSpan timeToLive, TimeSpan gracePeriod, CancellationToken cancellationToken);

        /// <summary>
        /// Gets detailed information about the current state of a claim.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.RefreshAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claim">The claim to query.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="Claim"/> object representing the claim.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claim"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Query_Claim">Query Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Task<Claim> QueryClaimAsync(QueueName queueName, Claim claim, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to renew a claim, by updating the time-to-live and resetting the age of the claim to zero.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.RenewAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claimId">The ID of the claim to update.</param>
        /// <param name="claimData">The updated data for the claim.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claimId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="claimData"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="UpdateClaimApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.UpdateClaimAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Update_Claim">Update Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<UpdateClaimApiCall> PrepareUpdateClaimAsync(QueueName queueName, ClaimId claimId, ClaimData claimData, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare an API call to release a claim, making any (remaining, non-deleted) messages associated
        /// with the claim available to other workers.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.DisposeAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claimId">The ID of the claim to release.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claimId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the preparation of this API call.</exception>
        /// <seealso cref="ReleaseClaimApiCall"/>
        /// <seealso cref="QueuesServiceExtensions.ReleaseClaimAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Release_Claim">Release Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        Task<ReleaseClaimApiCall> PrepareReleaseClaimAsync(QueueName queueName, ClaimId claimId, CancellationToken cancellationToken);

        #endregion
    }
}
