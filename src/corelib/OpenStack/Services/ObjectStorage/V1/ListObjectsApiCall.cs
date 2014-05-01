namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Collections;
    using OpenStack.Net;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public class ListObjectsApiCall : DelegatingHttpApiCall<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>>
    {
        public ListObjectsApiCall(IHttpApiCall<Tuple<ContainerMetadata, ReadOnlyCollectionPage<ContainerObject>>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
