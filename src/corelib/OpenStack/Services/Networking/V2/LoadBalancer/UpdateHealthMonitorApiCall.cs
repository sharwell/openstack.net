namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class UpdateHealthMonitorApiCall : DelegatingHttpApiCall<HealthMonitorResponse>
    {
        public UpdateHealthMonitorApiCall(IHttpApiCall<HealthMonitorResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
