namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class UpdateVirtualAddressApiCall : DelegatingHttpApiCall<VirtualAddressResponse>
    {
        public UpdateVirtualAddressApiCall(IHttpApiCall<VirtualAddressResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
