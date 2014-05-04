namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class GetVirtualAddressApiCall : DelegatingHttpApiCall<VirtualAddressResponse>
    {
        public GetVirtualAddressApiCall(IHttpApiCall<VirtualAddressResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
