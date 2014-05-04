namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class GetSecurityGroupRuleApiCall : DelegatingHttpApiCall<SecurityGroupRuleResponse>
    {
        public GetSecurityGroupRuleApiCall(IHttpApiCall<SecurityGroupRuleResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
