namespace OpenStack.Services.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Rackspace.Net;
    using Rackspace.Threading;
    using OpenStack.Net;

    /// <summary>
    /// This class defines extension methods for accessing the default behaviors provided
    /// by the <see cref="IEchoService"/> API.
    /// </summary>
    public static class EchoServiceExtensions
    {
        /// <summary>
        /// Echos an HTTP request back to the caller.
        /// </summary>
        /// <remarks>
        /// This method prepares and invokes the HTTP API with the default behavior, followed by returning
        /// the deserialized response body. If the application needs to modify the request or access additional
        /// properties of the <see cref="HttpResponseMessage"/>, the API should be invoked by separately
        /// preparing and sending the call.
        /// </remarks>
        /// <param name="service">The echo service.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains an <see cref="EchoResponse"/>
        /// instance containing information about the HTTP request received by the server.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <seealso cref="IEchoService.PrepareEchoAsync"/>
        /// <seealso cref="IHttpApiCall{T}.SendAsync"/>
        public static Task<EchoResponse> EchoAsync(this IEchoService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareEchoAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        /// <summary>
        /// Transforms an Echo API call by including a specified message as a query parameter.
        /// </summary>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare an <see cref="EchoApiCall"/>.</param>
        /// <param name="message">The message to include as a query parameter.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes
        /// successfully, the <see cref="Task{TResult}.Result"/> property contains the modified
        /// <see cref="EchoApiCall"/> instance (which now includes a query parameter named <c>m</c>
        /// with the value set to the specified <paramref name="message"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="task"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="message"/> is <see langword="null"/>.</para>
        /// </exception>
        public static Task<EchoApiCall> WithMessage(this Task<EchoApiCall> task, string message)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            if (message == null)
                throw new ArgumentNullException("message");

            return task.Select(
                innerTask =>
                {
                    Uri requestUri = innerTask.Result.RequestMessage.RequestUri;
                    UriTemplate template = new UriTemplate(string.IsNullOrEmpty(requestUri.Query) ? "{?m}" : "{&m}");
                    Dictionary<string, string> parameters = new Dictionary<string, string> { { "m", message } };
                    requestUri = new Uri(requestUri.OriginalString + template.BindByName(parameters).OriginalString);
                    innerTask.Result.RequestMessage.RequestUri = requestUri;
                    return innerTask.Result;
                });
        }
    }
}
