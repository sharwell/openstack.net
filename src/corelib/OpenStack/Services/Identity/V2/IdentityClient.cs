namespace OpenStack.Services.Identity.V2
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Security.Authentication;
    using Rackspace.Net;
    using Rackspace.Threading;

    public class IdentityClient : ServiceClient, IIdentityService
    {
        private readonly Uri _baseUri;

        public IdentityClient(Uri baseUri)
            : base(new AuthenticationService(baseUri), null)
        {
            _baseUri = baseUri;
        }

        #region IIdentityService Members

        public Task<ReadOnlyCollectionPage<ExtensionData>> ListExtensionsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("v2.0/extensions");
            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Get, template, new Dictionary<string, string>(), cancellationToken);

            Func<Task<HttpRequestMessage>, Task<JObject>> requestResource =
                GetResponseAsyncFunc<JObject>(cancellationToken);

            Func<Task<JObject>, ReadOnlyCollectionPage<ExtensionData>> selectResult =
                task =>
                {
                    JToken extensions = task.Result["extensions"];
                    if (extensions == null)
                        return null;

                    ExtensionData[] extensionData = extensions.ToObject<ExtensionData[]>();
                    return new BasicReadOnlyCollectionPage<ExtensionData>(extensionData, null);
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(selectResult);
        }

        public Task<UserAccess> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("v2.0/tokens");
            Func<Task<Uri>, Task<HttpRequestMessage>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.Post, template, new Dictionary<string, string>(), request, cancellationToken);

            Func<Task<HttpRequestMessage>, Task<JObject>> requestResource =
                GetResponseAsyncFunc<JObject>(cancellationToken);

            Func<Task<JObject>, UserAccess> selectResult =
                task =>
                {
                    JToken access = task.Result["access"];
                    if (access == null)
                        return null;

                    return access.ToObject<UserAccess>();
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource)
                .Select(selectResult);
        }

        #endregion

        public override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            return CompletedTask.FromResult(_baseUri);
        }

        protected class AuthenticationService : IAuthenticationService
        {
            private readonly Uri _baseUri;

            public AuthenticationService(Uri baseUri)
            {
                if (baseUri == null)
                    throw new ArgumentNullException("baseUri");
                if (!baseUri.IsAbsoluteUri)
                    throw new ArgumentException("baseUri must be an absolute URI", "baseUri");

                _baseUri = baseUri;
            }

            #region IAuthenticationService Members

            public Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken)
            {
                return CompletedTask.FromResult(_baseUri);
            }

            public Task AuthenticateRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
            {
                return CompletedTask.Default;
            }

            #endregion
        }

    }
}
