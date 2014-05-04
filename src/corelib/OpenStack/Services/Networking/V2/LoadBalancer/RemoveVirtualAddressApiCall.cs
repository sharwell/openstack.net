namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class RemoveVirtualAddressApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveVirtualAddressApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
