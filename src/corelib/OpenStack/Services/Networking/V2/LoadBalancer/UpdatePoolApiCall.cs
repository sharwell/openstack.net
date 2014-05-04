namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class UpdatePoolApiCall : DelegatingHttpApiCall<PoolResponse>
    {
        public UpdatePoolApiCall(IHttpApiCall<PoolResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
