namespace OpenStack.Services.Networking.V2.Layer3
{
    using OpenStack.Net;

    public class AddRouterInterfaceApiCall : DelegatingHttpApiCall<AddRouterInterfaceResponse>
    {
        public AddRouterInterfaceApiCall(IHttpApiCall<AddRouterInterfaceResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
