namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class RemoveSecurityGroupApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveSecurityGroupApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
