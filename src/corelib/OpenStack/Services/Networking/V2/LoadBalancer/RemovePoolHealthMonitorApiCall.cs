namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class RemovePoolHealthMonitorApiCall : DelegatingHttpApiCall<string>
    {
        public RemovePoolHealthMonitorApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
