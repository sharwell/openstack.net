namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class RemoveFloatingIpApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveFloatingIpApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
