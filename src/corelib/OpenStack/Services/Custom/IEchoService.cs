namespace OpenStack.Services.Custom
{
    using OpenStack.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This is the core interface of the Echo service.
    /// </summary>
    public interface IEchoService : IHttpService
    {
        /// <summary>
        /// Prepare an <see cref="EchoApiCall"/>. The Echo API call simply returns an
        /// <see cref="EchoResponse"/> describing the HTTP request received by the server.
        /// </summary>
        /// <remarks>
        /// This method prepares but does not send an HTTP API call. After the call is prepared,
        /// it may optionally be modified, followed by sending the request to the server by calling
        /// <see cref="IHttpApiCall{T}.SendAsync"/>.
        /// </remarks>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property
        /// contains an <see cref="EchoApiCall"/> instance representing the prepared API
        /// call.
        /// </returns>
        Task<EchoApiCall> PrepareEchoAsync(CancellationToken cancellationToken);
    }
}
