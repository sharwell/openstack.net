namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class UpdateRouterApiCall : DelegatingHttpApiCall<RouterResponse>
    {
        public UpdateRouterApiCall(IHttpApiCall<RouterResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
