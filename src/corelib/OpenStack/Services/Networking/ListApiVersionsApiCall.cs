namespace OpenStack.Services.Networking
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListApiVersionsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<ApiVersion>>
    {
        public ListApiVersionsApiCall(IHttpApiCall<ReadOnlyCollectionPage<ApiVersion>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
