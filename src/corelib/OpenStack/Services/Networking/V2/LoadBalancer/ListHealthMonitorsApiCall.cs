namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListHealthMonitorsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<HealthMonitor>>
    {
        public ListHealthMonitorsApiCall(IHttpApiCall<ReadOnlyCollectionPage<HealthMonitor>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
