namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class UpdateFloatingIpApiCall : DelegatingHttpApiCall<FloatingIpResponse>
    {
        public UpdateFloatingIpApiCall(IHttpApiCall<FloatingIpResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
