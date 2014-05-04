namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class GetNetworkApiCall : DelegatingHttpApiCall<NetworkResponse>
    {
        public GetNetworkApiCall(IHttpApiCall<NetworkResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
