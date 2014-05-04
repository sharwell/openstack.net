namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class RemoveSubnetApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveSubnetApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
