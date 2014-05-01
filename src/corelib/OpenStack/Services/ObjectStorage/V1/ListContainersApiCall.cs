namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Collections;
    using OpenStack.Net;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public class ListContainersApiCall : DelegatingHttpApiCall<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>>
    {
        public ListContainersApiCall(IHttpApiCall<Tuple<AccountMetadata, ReadOnlyCollectionPage<Container>>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
