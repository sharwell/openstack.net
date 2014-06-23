namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Rackspace.Threading;
    using CancellationToken = System.Threading.CancellationToken;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    /// <summary>
    /// Represents a claim of messages in a queue.
    /// </summary>
    /// <remarks>
    /// The claim is released when <see cref="Dispose()"/> or <see cref="DisposeAsync"/>
    /// is called. At that time, any messages belonging to this claim which have not
    /// been deleted will be eligible for claiming by another node in the system.
    /// Messages belonging to this claim may be deleted by calling
    /// <see cref="IQueuesService.PrepareRemoveMessageAsync"/> or
    /// <see cref="IQueuesService.PrepareRemoveMessagesAsync"/>.
    /// </remarks>
    /// <seealso cref="IQueuesService"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ClaimHandle : IDisposable
    {
        /// <summary>
        /// A private object used to ensure <see cref="_releaseTask"/> is only
        /// initialized once in <see cref="DisposeAsync"/>.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The queueing service instance used for commands related to this claim.
        /// </summary>
        private readonly IQueuesService _service;

        /// <summary>
        /// The name of the queue this claim belongs to.
        /// </summary>
        private readonly QueueName _queueName;

        /// <summary>
        /// The backing field for the <see cref="Location"/> property.
        /// </summary>
        private readonly Uri _location;

        /// <summary>
        /// The backing field for the <see cref="Claim"/> property.
        /// </summary>
        private Claim _claim;

        /// <summary>
        /// The <see cref="Task"/> object representing the asynchronous release of this claim.
        /// Prior to calling <see cref="Dispose()"/> or <see cref="DisposeAsync"/>, the value of
        /// this field is <see langword="null"/>.
        /// </summary>
        private Task _releaseTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimHandle"/> class using the provided values.
        /// </summary>
        /// <param name="service">The queueing service.</param>
        /// <param name="queueName">The name of the queue.</param>
        /// <param name="location">The absolute URI of the claim resource. If no claim was allocated by the server, this value is <see langword="null"/>.</param>
        /// <param name="claim">A <see cref="Claim"/> instance containing detailed information about the claim.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="service"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="queueName"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="claim"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="location"/> is not an absolute URI.</exception>
        public ClaimHandle(IQueuesService service, QueueName queueName, Uri location, Claim claim)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (queueName == null)
                throw new ArgumentNullException("queueName");
            if (claim == null)
                throw new ArgumentNullException("claim");
            if (location != null && !location.IsAbsoluteUri)
                throw new ArgumentException("location must be an absolute URI", "location");

            _service = service;
            _queueName = queueName;
            _location = location;
            _claim = claim;
        }

        /// <summary>
        /// Gets the claim ID.
        /// </summary>
        /// <remarks>
        /// The claim ID is derived from the <see cref="Location"/> property according to the
        /// URI template documented in the <see href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1">OpenStack Marconi API v1 Blueprint</see>.
        /// </remarks>
        /// <value>
        /// The ID of this claim. If the claim is empty (i.e. the queue did not have any unclaimed messages), this value is <see langword="null"/>.
        /// </value>
        public ClaimId Id
        {
            get
            {
                if (_location == null)
                    return null;

                string locationPath = _location.AbsolutePath;
                return new ClaimId(locationPath.Substring(locationPath.LastIndexOf('/') + 1));
            }
        }

        /// <summary>
        /// Gets the absolute URI for this claim.
        /// </summary>
        /// <value>
        /// The absolute URI of this claim. If the claim is empty (i.e. the queue did not have any unclaimed messages), this value is <see langword="null"/>.
        /// </value>
        public Uri Location
        {
            get
            {
                return _location;
            }
        }

        /// <summary>
        /// Gets a <see cref="V1.Claim"/> object containing detailed information about the claim.
        /// </summary>
        /// <value>
        /// A <see cref="V1.Claim"/> object containing detailed information about the claim.
        /// </value>
        public Claim Claim
        {
            get
            {
                return _claim;
            }
        }

        /// <summary>
        /// Refreshes the current claim.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="IQueuesService.PrepareQueryClaimAsync"/> to obtain updated
        /// information about the current claim, and then synchronously invokes <see cref="RefreshImpl"/>
        /// to update the current instance to match the results.
        /// </remarks>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        public Task RefreshAsync(CancellationToken cancellationToken)
        {
            Action<Task<Tuple<HttpResponseMessage, Tuple<Uri, Claim>>>> applyChanges =
                task =>
                {
                    Uri requestUri = task.Result.Item1.RequestMessage.RequestUri;
                    Uri location = new Uri(requestUri, task.Result.Item2.Item1);
                    RefreshImpl(Tuple.Create(location, task.Result.Item2.Item2));
                };
            Task<Tuple<HttpResponseMessage, Tuple<Uri, Claim>>> queryClaimTask =
                CoreTaskExtensions.Using(
                    () => _service.PrepareQueryClaimAsync(_queueName, Id, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken));
            return queryClaimTask.Select(applyChanges);
        }

        /// <summary>
        /// Renews the claim by resetting the age and updating the TTL for the claim.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="QueuesServiceExtensions.UpdateClaimAsync"/> to renew the
        /// current claim, and then synchronously updates the current instance to reflect
        /// the new age and time-to-live values.
        /// </remarks>
        /// <param name="timeToLive">
        /// The new Time-To-Live value for the claim. This value may differ from the original TTL of the claim.
        /// </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="timeToLive"/> is negative or <see cref="TimeSpan.Zero"/>.</exception>
        /// <exception cref="InvalidOperationException">If the claim is empty (i.e. the <see cref="Claim.Messages"/> property of <see cref="Claim"/> is empty).</exception>
        public Task RenewAsync(TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            if (timeToLive <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("timeToLive");
            if (_location == null)
                throw new InvalidOperationException("Empty claims cannot be renewed.");

            Action<Task> applyChanges =
                task =>
                {
                    _claim = new Claim(timeToLive, _claim.GracePeriod, TimeSpan.Zero, _claim.Messages, _claim.ExtensionData);
                };
            return _service.UpdateClaimAsync(_queueName, Id, timeToLive, cancellationToken).Select(applyChanges);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="QueuesServiceExtensions.ReleaseClaimAsync"/> to release messages
        /// claimed by this claim. To prevent other subscribers from re-claiming the messages, make
        /// sure to delete the messages before calling <see cref="Dispose()"/>.
        /// </remarks>
        /// <seealso cref="IQueuesService.PrepareReleaseClaimAsync"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Asynchronously releases resources owned by this <see cref="ClaimHandle"/>.
        /// </summary>
        /// <remarks>
        /// This method calls <see cref="QueuesServiceExtensions.ReleaseClaimAsync"/> to release messages
        /// claimed by this claim. To prevent other subscribers from re-claiming the messages, make
        /// sure to delete the messages before calling <see cref="DisposeAsync"/>.
        /// </remarks>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        public Task DisposeAsync(CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                if (_releaseTask == null)
                {
                    if (_claim.Messages == null || _claim.Messages.Count == 0)
                        _releaseTask = CompletedTask.Default;
                    else
                        _releaseTask = _service.ReleaseClaimAsync(_queueName, Id, cancellationToken);
                }
            }

            return _releaseTask;
        }

        /// <summary>
        /// Releases resources owned by this <see cref="ClaimHandle"/>.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> if this method was called from <see cref="Dispose()"/>; otherwise, <see langword="false"/> if this method was called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeAsync(CancellationToken.None).Wait();
            }
        }

        /// <summary>
        /// Refresh the current claim to match the updated information in <paramref name="claim"/>.
        /// </summary>
        /// <param name="claim">A <see cref="ClaimHandle"/> object containing updated claim information.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="claim"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If the specified <paramref name="claim"/> does not represent the same claim as the current instance.</exception>
        protected virtual void RefreshImpl(Tuple<Uri, Claim> claim)
        {
            if (claim == null)
                throw new ArgumentNullException("claim");
            if (Location != claim.Item1)
                throw new ArgumentException("The specified claim does not represent the same claim as the current instance.", "claim");

            _claim = claim.Item2;
        }
    }
}
