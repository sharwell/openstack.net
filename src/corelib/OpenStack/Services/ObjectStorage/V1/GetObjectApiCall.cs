namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;
    using Stream = System.IO.Stream;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public class GetObjectApiCall : DelegatingHttpApiCall<Tuple<ObjectMetadata, Stream>>
    {
        public GetObjectApiCall(IHttpApiCall<Tuple<ObjectMetadata, Stream>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
