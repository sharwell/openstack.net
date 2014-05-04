namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class RemoveHealthMonitorApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveHealthMonitorApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
