﻿namespace net.openstack.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Providers;
    using CancellationToken = System.Threading.CancellationToken;

    /// <summary>
    /// Represents a claim of messages in a queue.
    /// </summary>
    /// <remarks>
    /// The claim is released when <see cref="Dispose"/> or <see cref="DisposeAsync"/>
    /// is called. At that time, any messages belonging to this claim which have not
    /// been deleted will be eligible for claiming by another node in the system.
    /// Messages belonging to this claim may be deleted by calling
    /// <see cref="IQueueingService.DeleteMessageAsync"/> or
    /// <see cref="IQueueingService.DeleteMessagesAsync"/>.
    /// </remarks>
    /// <seealso cref="IQueueingService"/>
    public sealed class Claim : IDisposable
    {
        /// <summary>
        /// A private object used to ensure <see cref="_releaseTask"/> is only
        /// initialized once in <see cref="DisposeAsync"/>.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The queueing service instance used for commands related to this claim.
        /// </summary>
        private readonly IQueueingService _service;

        /// <summary>
        /// The name of the queue this claim belongs to.
        /// </summary>
        private readonly string _queueName;

        /// <summary>
        /// The backing field for the <see cref="Location"/> property.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        /// The backing field for the <see cref="Age"/> property.
        /// </summary>
        private readonly TimeSpan _age;

        /// <summary>
        /// The backing field for the <see cref="TimeToLive"/> property.
        /// </summary>
        private TimeSpan _timeToLive;

        /// <summary>
        /// The backing field for the <see cref="Messages"/> property.
        /// </summary>
        private readonly QueuedMessage[] _messages;

        /// <summary>
        /// The <see cref="Task"/> object representing the asynchronous release of this claim.
        /// Prior to calling <see cref="Dispose"/> or <see cref="DisposeAsync"/>, the value of
        /// this field is <c>null</c>.
        /// </summary>
        private Task _releaseTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class using the provided values.
        /// </summary>
        /// <param name="service">The queueing service.</param>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="location">The absolute URI of the claim resource. If no claim was allocated by the server, this value is <c>null</c>.</param>
        /// <param name="timeToLive">The time to live of the claim.</param>
        /// <param name="age">The age of the claim.</param>
        /// <param name="messages">A collection of messages belonging to the claim.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="messages"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="queueName"/> is empty.
        /// </exception>
        public Claim(IQueueingService service, string queueName, Uri location, TimeSpan timeToLive, TimeSpan age, IEnumerable<QueuedMessage> messages)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (messages == null)
                throw new ArgumentNullException("messages");

            _service = service;
            _queueName = queueName;
            _location = location;
            _timeToLive = timeToLive;
            _age = age;
            _messages = messages.ToArray();
        }

        /// <summary>
        /// Gets the claim ID.
        /// </summary>
        /// <remarks>
        /// The claim ID is derived from the <see cref="Location"/> property according to the
        /// URI template documented in the <see href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1">OpenStack Marconi API v1 Blueprint</see>.
        /// </remarks>
        /// <value>
        /// The ID of this claim. If the claim is empty (i.e. the queue did not have any unclaimed messages), this value is <c>null</c>.
        /// </value>
        public string Id
        {
            get
            {
                if (_location == null)
                    return null;

                string locationPath = _location.AbsolutePath;
                return locationPath.Substring(locationPath.LastIndexOf('/') + 1);
            }
        }

        /// <summary>
        /// Gets the absolute URI for this claim.
        /// </summary>
        /// <value>
        /// The absolute URI of this claim. If the claim is empty (i.e. the queue did not have any unclaimed messages), this value is <c>null</c>.
        /// </value>
        public Uri Location
        {
            get
            {
                return _location;
            }
        }

        /// <summary>
        /// Gets the age of the claim as returned by the server.
        /// </summary>
        /// <remarks>
        /// This value does not automatically update. To obtain the age of a claim after a period of time elapses,
        /// use <see cref="IQueueingService.QueryClaimAsync"/>.
        /// </remarks>
        public TimeSpan Age
        {
            get
            {
                return _age;
            }
        }

        /// <summary>
        /// Gets the Time To Live (TTL) of the claim.
        /// </summary>
        public TimeSpan TimeToLive
        {
            get
            {
                return _timeToLive;
            }

            private set
            {
                _timeToLive = value;
            }
        }

        /// <summary>
        /// Gets the messages which are included in this claim.
        /// </summary>
        public ReadOnlyCollection<QueuedMessage> Messages
        {
            get
            {
                return new ReadOnlyCollection<QueuedMessage>(_messages);
            }
        }

        /// <summary>
        /// Renews the claim by resetting the age and updating the TTL for the claim.
        /// </summary>
        /// <param name="timeToLive">
        /// The new Time To Live value for the claim. This value may differ from the original TTL of the claim.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeToLive"/> is negative or <see cref="TimeSpan.Zero"/>.</exception>
        /// <exception cref="InvalidOperationException">If the claim is empty (i.e. <see cref="Messages"/> is empty).</exception>
        public Task RenewAsync(TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            if (_location == null)
                throw new InvalidOperationException("Empty claims cannot be renewed.");

            Action<Task> applyChanges =
                task =>
                {
                    task.PropagateExceptions();
                    TimeToLive = timeToLive;
                };
            return _service.UpdateClaimAsync(_queueName, this, timeToLive, cancellationToken).ContinueWith(applyChanges);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IQueueingService.ReleaseClaimAsync"/> to release messages
        /// claimed by this claim. To prevent other subscribers from re-claiming the messages, make
        /// sure to delete the messages before calling <see cref="Dispose"/>.
        /// </remarks>
        /// <seealso cref="IQueueingService.ReleaseClaimAsync"/>
        public void Dispose()
        {
            DisposeAsync(CancellationToken.None).Wait();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Asynchronously releases resources owned by this <see cref="Claim"/>.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="IQueueingService.ReleaseClaimAsync"/> to release messages
        /// claimed by this claim. To prevent other subscribers from re-claiming the messages, make
        /// sure to delete the messages before calling <see cref="DisposeAsync"/>.
        /// </remarks>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        public Task DisposeAsync(CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                if (_releaseTask == null)
                {
                    if (_messages.Length == 0)
                        _releaseTask = InternalTaskExtensions.CompletedTask();
                    else
                        _releaseTask = _service.ReleaseClaimAsync(_queueName, this, cancellationToken);
                }
            }

            return _releaseTask;
        }
    }
}
