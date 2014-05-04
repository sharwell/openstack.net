namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class GetFloatingIpApiCall : DelegatingHttpApiCall<FloatingIpResponse>
    {
        public GetFloatingIpApiCall(IHttpApiCall<FloatingIpResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
