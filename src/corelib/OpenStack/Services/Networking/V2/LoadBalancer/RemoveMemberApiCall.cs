namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class RemoveMemberApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveMemberApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
