namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class RemoveRouterApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveRouterApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
