namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class GetPoolApiCall : DelegatingHttpApiCall<PoolResponse>
    {
        public GetPoolApiCall(IHttpApiCall<PoolResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
