namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class AddNetworkApiCall : DelegatingHttpApiCall<NetworkResponse>
    {
        public AddNetworkApiCall(IHttpApiCall<NetworkResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
