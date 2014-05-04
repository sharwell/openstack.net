namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class AddRouterApiCall : DelegatingHttpApiCall<RouterResponse>
    {
        public AddRouterApiCall(IHttpApiCall<RouterResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
