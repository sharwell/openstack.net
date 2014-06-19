﻿#if !PORTABLE

namespace net.openstack.Core.Synchronous
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using net.openstack.Core.Domain;
    using OpenStack.Collections;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Services.Queues.V1;
    using CancellationToken = System.Threading.CancellationToken;
    using JObject = Newtonsoft.Json.Linq.JObject;
    using JsonSerializationException = Newtonsoft.Json.JsonSerializationException;
    using WebException = System.Net.WebException;

    /// <summary>
    /// Provides extension methods to allow synchronous calls to the methods in <see cref="IQueuesService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class QueueingServiceExtensions
    {
        #region Base endpoints

        /// <summary>
        /// Gets the home document describing the operations supported by the service.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <returns>A <see cref="HomeDocument"/> object describing the operations supported by the service.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Home_Document">Get Home Document (OpenStack Marconi API v1 Blueprint)</seealso>
        public static HomeDocument GetHome(this IQueuesService queuesService)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.GetHomeAsync(CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Checks the queueing service node status.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <returns>
        /// If the service is available, the operation will complete successfully. If the service
        /// is unavailable due to a storage driver failure or some other error, the operation will
        /// fail and the exception will contain the reason for the failure.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/wiki/Marconi/specs/api/v1#Check_Node_Health">Check Node Health (OpenStack Marconi API v1 Blueprint)</seealso>
        public static void GetNodeHealth(this IQueuesService queuesService)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.GetNodeHealthAsync(CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        #endregion Base endpoints

        #region Queues

        /// <summary>
        /// Creates a queue, if it does not already exist.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <returns><see langword="true"/> if the queue was created by the call, or <see langword="false"/> if the queue already existed.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Create_Queue">Create Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        public static bool CreateQueue(this IQueuesService queuesService, QueueName queueName)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.CreateQueueAsync(queueName, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Gets a list of queues.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="marker">The name of the last queue in the previous list. The resulting collection of queues will start with the first queue <em>after</em> this value, when sorted using <see cref="StringComparer.Ordinal"/>. If this value is <see langword="null"/>, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of queues to return. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="detailed"><see langword="true"/> to return detailed information about each queue; otherwise, <see langword="false"/>.</param>
        /// <returns><placeholder>placeholder</placeholder></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="limit"/> is less than or equal to 0.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#List_Queues">List Queues (OpenStack Marconi API v1 Blueprint)</seealso>
        public static ReadOnlyCollectionPage<Queue> ListQueues(this IQueuesService queuesService, QueueName marker, int? limit, bool detailed)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.ListQueuesAsync(marker, limit, detailed, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Checks for the existence of a queue with a particular name.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <returns><see langword="true"/> if queue with the specified name exists; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Checking_Queue_Existence">Checking Queue Existence (OpenStack Marconi API v1 Blueprint)</seealso>
        public static bool QueueExists(this IQueuesService queuesService, QueueName queueName)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.QueueExistsAsync(queueName, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Removes a queue.
        /// </summary>
        /// <remarks>
        /// The queue will be removed whether or not it is empty, even if one or more messages in the queue is currently claimed.
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Queue">Delete Queue (OpenStack Marconi API v1 Blueprint)</seealso>
        public static void RemoveQueue(this IQueuesService queuesService, QueueName queueName)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.RemoveQueueAsync(queueName, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        #endregion

        #region Queue metadata

        /// <summary>
        /// Sets the metadata associated with a queue.
        /// </summary>
        /// <typeparam name="T">The type of data to associate with the queue.</typeparam>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="metadata">The metadata to associate with the queue.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Set_Queue_Metadata">Set Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        public static void SetQueueMetadata<T>(this IQueuesService queuesService, QueueName queueName, T metadata)
            where T : class
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.SetQueueMetadataAsync(queueName, metadata, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Gets the metadata associated with a queue, as a strongly-typed object.
        /// </summary>
        /// <typeparam name="T">The type of metadata associated with the queue.</typeparam>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <returns>A deserialized object of type <typeparamref name="T"/> representing the metadata associated with the queue.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="JsonSerializationException">If an error occurs while deserializing the metadata.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Metadata">Get Queue Metadata (OpenStack Marconi API v1 Blueprint)</seealso>
        public static T GetQueueMetadata<T>(this IQueuesService queuesService, QueueName queueName)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.GetQueueMetadataAsync<T>(queueName, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Gets statistics for a queue.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <returns>A <see cref="QueueStatistics"/> object containing statistics for the queue.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Queue_Stats">Get Queue Stats (OpenStack Marconi API v1 Blueprint)</seealso>
        public static QueueStatistics GetQueueStatistics(this IQueuesService queuesService, QueueName queueName)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.GetQueueStatisticsAsync(queueName, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        #endregion Queue metadata

        #region Messages

        /// <summary>
        /// Gets a list of messages currently in a queue.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="marker">The identifier of the message list page to return. This is obtained from <see cref="QueuedMessageList.NextPageId"/>. If this value is <see langword="null"/>, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of messages to return. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="echo"><see langword="true"/> to include messages created by the current client; otherwise, <see langword="false"/>.</param>
        /// <param name="includeClaimed"><see langword="true"/> to include claimed messages; otherwise <see langword="false"/> to return only unclaimed messages.</param>
        /// <returns>A collection of <see cref="QueuedMessage"/> objects describing the messages in the queue.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="queueName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="limit"/> is less than or equal to 0.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#List_Messages">List Messages (OpenStack Marconi API v1 Blueprint)</seealso>
        public static QueuedMessageList ListMessages(this IQueuesService queuesService, QueueName queueName, QueuedMessageListId marker, int? limit, bool echo, bool includeClaimed)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.ListMessagesAsync(queueName, marker, limit, echo, includeClaimed, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Gets detailed information about a specific queued message.
        /// </summary>
        /// <remarks>
        /// This method will return information for the specified message regardless of the
        /// <literal>Client-ID</literal> or claim associated with the message.
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The message ID. This is obtained from <see cref="QueuedMessage.Id">QueuedMessage.Id</see>.</param>
        /// <returns>A <see cref="QueuedMessage"/> object containing detailed information about the specified message.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_a_Specific_Message">Get a Specific Message (OpenStack Marconi API v1 Blueprint)</seealso>
        public static QueuedMessage GetMessage(this IQueuesService queuesService, QueueName queueName, MessageId messageId)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.GetMessageAsync(queueName, messageId, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Get messages from a queue.
        /// </summary>
        /// <remarks>
        /// This method will return information for the specified message regardless of the
        /// <literal>Client-ID</literal> or claim associated with the message.
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageIds">The message IDs of messages to get.</param>
        /// <returns>A collection of <see cref="QueuedMessage"/> objects containing detailed information about the specified messages.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static ReadOnlyCollection<QueuedMessage> GetMessages(this IQueuesService queuesService, QueueName queueName, IEnumerable<MessageId> messageIds)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.GetMessagesAsync(queueName, messageIds, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static void PostMessages(this IQueuesService queuesService, QueueName queueName, IEnumerable<Message> messages)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.PostMessagesAsync(queueName, messages, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static void PostMessages(this IQueuesService queuesService, QueueName queueName, params Message[] messages)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.PostMessagesAsync(queueName, CancellationToken.None, messages).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <typeparam name="T">The class modeling the JSON representation of the messages to post in the queue.</typeparam>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static void PostMessages<T>(this IQueuesService queuesService, QueueName queueName, IEnumerable<Message<T>> messages)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.PostMessagesAsync(queueName, messages, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Posts messages to a queue.
        /// </summary>
        /// <typeparam name="T">The class modeling the JSON representation of the messages to post in the queue.</typeparam>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messages">The messages to post.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static void PostMessages<T>(this IQueuesService queuesService, QueueName queueName, params Message<T>[] messages)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.PostMessagesAsync(queueName, CancellationToken.None, messages).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Deletes a message from a queue.
        /// </summary>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageId">The ID of the message to delete. This is obtained from <see cref="QueuedMessage.Id">QueuedMessage.Id</see>.</param>
        /// <param name="claim">The claim for the message. If this value is <see langword="null"/>, the delete operation will fail if the message is claimed. If this value is non-<see langword="null"/>, the delete operation will fail if the message is not claimed by the specified claim.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="messageId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Delete_Message">Delete Message (OpenStack Marconi API v1 Blueprint)</seealso>
        public static void DeleteMessage(this IQueuesService queuesService, QueueName queueName, MessageId messageId, Claim claim)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.DeleteMessageAsync(queueName, messageId, claim, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Deletes messages from a queue.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// This method deletes messages from a queue whether or not they are currently claimed.
        /// </note>
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="messageIds">The IDs of messages to delete. These are obtained from <see cref="QueuedMessage.Id">QueuedMessage.Id</see>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static void DeleteMessages(this IQueuesService queuesService, QueueName queueName, IEnumerable<MessageId> messageIds)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.DeleteMessagesAsync(queueName, messageIds, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

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
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="limit">The maximum number of messages to claim. If this value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="timeToLive">The time to wait before the server automatically releases the claim.</param>
        /// <param name="gracePeriod">The time to wait, after the time-to-live for the claim expires, before the server allows the claimed messages to be deleted due to the individual message's time-to-live expiring.</param>
        /// <returns>A <see cref="Claim"/> object representing the claim.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
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
        public static Claim ClaimMessage(this IQueuesService queuesService, QueueName queueName, int? limit, TimeSpan timeToLive, TimeSpan gracePeriod)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.ClaimMessageAsync(queueName, limit, timeToLive, gracePeriod, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Gets detailed information about the current state of a claim.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.RefreshAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claim">The claim to query.</param>
        /// <returns>A <see cref="Claim"/> object representing the claim.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claim"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Query_Claim">Query Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Claim QueryClaim(this IQueuesService queuesService, QueueName queueName, Claim claim)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                return queuesService.QueryClaimAsync(queueName, claim, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Renews a claim, by updating the time-to-live and resetting the age of the claim to zero.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.RenewAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claim">The claim to renew.</param>
        /// <param name="timeToLive">The updated time-to-live for the claim.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claim"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeToLive"/> is negative.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Update_Claim">Update Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void UpdateClaim(this IQueuesService queuesService, QueueName queueName, Claim claim, TimeSpan timeToLive)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.UpdateClaimAsync(queueName, claim, timeToLive, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        /// <summary>
        /// Immediately release a claim, making any (remaining, non-deleted) messages associated
        /// with the claim available to other workers.
        /// </summary>
        /// <remarks>
        /// <note type="caller">Use <see cref="Claim.DisposeAsync"/> instead of calling this method directly.</note>
        /// </remarks>
        /// <param name="queuesService">The queueing service instance.</param>
        /// <param name="queueName">The queue name.</param>
        /// <param name="claim">The claim to release.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queuesService"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="queueName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="claim"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Release_Claim">Release Claim (OpenStack Marconi API v1 Blueprint)</seealso>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void ReleaseClaim(this IQueuesService queuesService, QueueName queueName, Claim claim)
        {
            if (queuesService == null)
                throw new ArgumentNullException("queuesService");

            try
            {
                queuesService.ReleaseClaimAsync(queueName, claim, CancellationToken.None).Wait();
            }
            catch (AggregateException ex)
            {
                ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
                if (innerExceptions.Count == 1)
                    throw innerExceptions[0];

                throw;
            }
        }

        #endregion
    }
}

#endif
