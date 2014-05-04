namespace OpenStack.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Net;

    public interface IHttpService
    {
        Func<Task<Uri>, Task<HttpRequestMessage>> PrepareRequestAsyncFunc<T>(HttpMethod method, UriTemplate template, IDictionary<string, T> parameters, CancellationToken cancellationToken);

        Func<Task<Uri>, Task<HttpRequestMessage>> PrepareRequestAsyncFunc<T, TBody>(HttpMethod method, UriTemplate template, IDictionary<string, T> parameters, TBody body, CancellationToken cancellationToken);

        Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken);
    }
}
