namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class AddVirtualAddressApiCall : DelegatingHttpApiCall<VirtualAddressResponse>
    {
        public AddVirtualAddressApiCall(IHttpApiCall<VirtualAddressResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
