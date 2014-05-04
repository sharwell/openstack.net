namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class UpdateNetworkApiCall : DelegatingHttpApiCall<NetworkResponse>
    {
        public UpdateNetworkApiCall(IHttpApiCall<NetworkResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
