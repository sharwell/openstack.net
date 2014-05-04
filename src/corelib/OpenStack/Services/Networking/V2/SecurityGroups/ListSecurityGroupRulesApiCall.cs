namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListSecurityGroupRulesApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<SecurityGroupRule>>
    {
        public ListSecurityGroupRulesApiCall(IHttpApiCall<ReadOnlyCollectionPage<SecurityGroupRule>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
