namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class AddPoolApiCall : DelegatingHttpApiCall<PoolResponse>
    {
        public AddPoolApiCall(IHttpApiCall<PoolResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
