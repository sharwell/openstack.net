namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class RemoveNetworkApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveNetworkApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
