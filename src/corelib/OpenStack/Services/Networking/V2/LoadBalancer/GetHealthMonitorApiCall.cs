namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class GetHealthMonitorApiCall : DelegatingHttpApiCall<HealthMonitorResponse>
    {
        public GetHealthMonitorApiCall(IHttpApiCall<HealthMonitorResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
