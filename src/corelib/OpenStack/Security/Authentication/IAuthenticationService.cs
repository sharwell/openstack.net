namespace OpenStack.Security.Authentication
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAuthenticationService
    {
        Task<Uri> GetBaseAddressAsync(string serviceType, string serviceName, string region, bool internalAddress, CancellationToken cancellationToken);

        Task AuthenticateRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken);
    }
}
