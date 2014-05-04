namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class GetRouterApiCall : DelegatingHttpApiCall<RouterResponse>
    {
        public GetRouterApiCall(IHttpApiCall<RouterResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
