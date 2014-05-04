namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class AddFloatingIpApiCall : DelegatingHttpApiCall<FloatingIpResponse>
    {
        public AddFloatingIpApiCall(IHttpApiCall<FloatingIpResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
