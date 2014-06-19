namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using Rackspace.Threading;

    public static class QueuesServiceExtensions
    {
        #region Base endpoints

        /// <summary>
        /// Get a <see cref="HomeDocument"/> describing the operations supported by the service.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// a <see cref="HomeDocument"/> instance describing the operations supported by the
        /// service, or <see langword="null"/> if no home document was returned by the API.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the client, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetHomeAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetHomeAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetHomeAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetHomeAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetHomeAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="GetHomeAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetHomeAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="HomeDocument"/>
        /// <seealso cref="IQueuesService.PrepareGetHomeAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Home_Document">Get Home Document (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<HomeDocument> GetHomeAsync(this IQueuesService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetHomeAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Check the queueing service node status.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// <see langword="true"/> if the service is operational, or <see langword="false"/>
        /// if the service is currently down.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the client, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetNodeHealthAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetNodeHealthAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetNodeHealthAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetNodeHealthAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetNodeHealthAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="GetNodeHealthAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetNodeHealthAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="IQueuesService.PrepareGetNodeHealthAsync"/>
        /// <seealso href="https://wiki.openstack.org/wiki/Marconi/specs/api/v1#Check_Node_Health">Check Node Health (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<bool> GetNodeHealthAsync(this IQueuesService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetNodeHealthAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion Base endpoints

        #region Queues

        /// <summary>
        /// Creates a queue, if it does not already exist.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will
        /// contain <see langword="true"/> if the queue was created by the call, or
        /// <see langword="false"/> if the queue already existed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="CreateQueueAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="CreateQueueAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="CreateQueueAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="CreateQueueAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="CreateQueueAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="CreateQueueAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="CreateQueueAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="IQueuesService.PrepareCreateQueueAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Create_Queue">Create Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<bool> CreateQueueAsync(this IQueuesService service, QueueName queueName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateQueueAsync(queueName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Check for the existence of a queue with a particular name.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// <see langword="true"/> if a queue with the specified name exists; otherwise,
        /// <see langword="false"/> if no queue with the specified name exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="QueueExistsAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="QueueExistsAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="QueueExistsAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="QueueExistsAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="QueueExistsAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="QueueExistsAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="QueueExistsAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="IQueuesService.PrepareQueueExistsAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Checking_Queue_Existence">Checking Queue Existence (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<bool> QueueExistsAsync(this IQueuesService service, QueueName queueName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareQueueExistsAsync(queueName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Remove a queue.
        /// </summary>
        /// <remarks>
        /// The queue will be remove whether or not it is empty, even if one or more messages in the queue is currently claimed.
        /// </remarks>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the provider, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="RemoveQueueAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="RemoveQueueAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="RemoveQueueAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="RemoveQueueAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="RemoveQueueAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="RemoveQueueAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="RemoveQueueAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="IQueuesService.PrepareRemoveQueueAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Queue">Delete Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task RemoveQueueAsync(this IQueuesService service, QueueName queueName, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveQueueAsync(queueName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Queue metadata

        /// <summary>
        /// Set the metadata associated with a queue.
        /// </summary>
        /// <typeparam name="T">The type modeling the metadata to associate with the queue.</typeparam>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="metadata">The metadata to associate with the queue.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="metadata"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareSetQueueMetadataAsync{T}"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Set_Queue_Metadata">Set Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task SetQueueMetadataAsync<T>(this IQueuesService service, QueueName queueName, T metadata, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetQueueMetadataAsync(queueName, metadata, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Get the metadata associated with a queue.
        /// </summary>
        /// <typeparam name="T">The type modeling the metadata associated with the queue. Use <see cref="JObject"/> for generic JSON metadata.</typeparam>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the deserialized metadata associated with the specified queue.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareGetQueueMetadataAsync{T}"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Metadata">Get Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<T> GetQueueMetadataAsync<T>(this IQueuesService service, QueueName queueName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetQueueMetadataAsync<T>(queueName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Get statistics for a queue.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// a <see cref="QueueStatistics"/> object containing statistics information for
        /// the queue.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareGetQueueStatisticsAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Stats">Get Queue Stats (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<QueueStatistics> GetQueueStatisticsAsync(this IQueuesService service, QueueName queueName, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetQueueStatisticsAsync(queueName, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion Queue metadata

        #region Messages

        /// <summary>
        /// Get detailed information about a specific queued message.
        /// </summary>
        /// <remarks>
        /// This method will return information for the specified message regardless of the
        /// <literal>Client-ID</literal> or claim associated with the message.
        /// </remarks>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The ID of the message.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// the prepared API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="messageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareGetMessageAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_a_Specific_Message">Get a Specific Message (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<QueuedMessage> GetMessageAsync(this IQueuesService service, QueueName queueName, MessageId messageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetMessageAsync(queueName, messageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Remove a message from a queue.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The ID of the message to remove.</param>
        /// <param name="claimId">The ID of the claim for the message. If this value is <see langword="null"/>, the remove operation will fail if the message is claimed. If this value is non-<see langword="null"/>, the remove operation will fail if the message is not claimed by the specified claim.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareRemoveMessageAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Message">Delete Message (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task RemoveMessageAsync(this IQueuesService service, QueueName queueName, MessageId messageId, ClaimId claimId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveMessageAsync(queueName, messageId, claimId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion Messages

        #region Claims

        /// <summary>
        /// Renew a claim, by updating the time-to-live and resetting the age of the claim to zero.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.RenewAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claimId">The ID of the claim to update.</param>
        /// <param name="timeToLive">The time to live of the claim.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="claimId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareUpdateClaimAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Update_Claim">Update Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task UpdateClaimAsync(this IQueuesService service, QueueName queueName, ClaimId claimId, TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareUpdateClaimAsync(queueName, claimId, new ClaimData(timeToLive), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        /// <summary>
        /// Release a claim, making any (remaining, non-deleted) messages associated
        /// with the claim available to other workers.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.DisposeAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claimId">The ID of the claim to release.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="claimId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso cref="IQueuesService.PrepareReleaseClaimAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Release_Claim">Release Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task ReleaseClaimAsync(this IQueuesService service, QueueName queueName, ClaimId claimId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareReleaseClaimAsync(queueName, claimId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion
    }
}
