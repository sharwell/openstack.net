namespace OpenStack.Services.Compute.V2
{
    using OpenStack.Net;

    public class GetExtensionApiCall : DelegatingHttpApiCall<ExtensionResponse>
    {
        public GetExtensionApiCall(IHttpApiCall<ExtensionResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
