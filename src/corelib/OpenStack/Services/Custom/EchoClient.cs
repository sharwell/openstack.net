namespace OpenStack.Services.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This is the base client implementation for the <see cref="IEchoService"/>.
    /// </summary>
    public class EchoClient : ServiceClient, IEchoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EchoClient"/> class.
        /// </summary>
        public EchoClient()
            : base(new NoAuthenticationService(), null)
        {
        }

        /// <inheritdoc/>
        public Task<EchoApiCall> PrepareEchoAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/get");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new EchoApiCall(CreateJsonApiCall<EchoResponse>(task.Result)));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This service always uses the base endpoint address http://httpbin.org.
        /// </remarks>
        public override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            return CompletedTask.FromResult(new Uri("http://httpbin.org"));
        }

        /// <summary>
        /// This example client does not require authentication for its requests, so we create
        /// a trivial implementation of <see cref="IAuthenticationService"/> which does not
        /// modify the request message in <see cref="AuthenticateRequestAsync"/>.
        /// </summary>
        private class NoAuthenticationService : IAuthenticationService
        {
            /// <inheritdoc/>
            /// <remarks>
            /// This implementation always returns without altering the <paramref name="requestMessage"/>.
            /// </remarks>
            public Task AuthenticateRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
            {
                return CompletedTask.Default;
            }

            /// <inheritdoc/>
            /// <remarks>
            /// This implementation always throws <see cref="NotSupportedException"/>.
            /// </remarks>
            public Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
            {
                throw new NotSupportedException();
            }
        }
    }
}
