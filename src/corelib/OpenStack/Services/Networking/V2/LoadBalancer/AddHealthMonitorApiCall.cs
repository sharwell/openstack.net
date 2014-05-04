namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class AddHealthMonitorApiCall : DelegatingHttpApiCall<HealthMonitorResponse>
    {
        public AddHealthMonitorApiCall(IHttpApiCall<HealthMonitorResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
