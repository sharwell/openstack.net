namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class RemoveRouterInterfaceApiCall : DelegatingHttpApiCall<RemoveRouterInterfaceResponse>
    {
        public RemoveRouterInterfaceApiCall(IHttpApiCall<RemoveRouterInterfaceResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
