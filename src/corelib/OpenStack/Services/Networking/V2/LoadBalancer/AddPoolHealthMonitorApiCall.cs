namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class AddPoolHealthMonitorApiCall : DelegatingHttpApiCall<HealthMonitorResponse>
    {
        public AddPoolHealthMonitorApiCall(IHttpApiCall<HealthMonitorResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
