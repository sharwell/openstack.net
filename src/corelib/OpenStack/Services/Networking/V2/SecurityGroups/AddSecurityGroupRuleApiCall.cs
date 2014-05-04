namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class AddSecurityGroupRuleApiCall : DelegatingHttpApiCall<SecurityGroupRuleResponse>
    {
        public AddSecurityGroupRuleApiCall(IHttpApiCall<SecurityGroupRuleResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
