namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class RemovePoolApiCall : DelegatingHttpApiCall<string>
    {
        public RemovePoolApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
