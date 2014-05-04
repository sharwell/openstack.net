namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class RemoveSecurityGroupRuleApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveSecurityGroupRuleApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
