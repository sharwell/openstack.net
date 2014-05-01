namespace OpenStack.Net
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public interface IHttpApiCall<T> : IHttpApiRequest
    {
        Task<Tuple<HttpResponseMessage, T>> SendAsync(CancellationToken cancellationToken);
    }
}
