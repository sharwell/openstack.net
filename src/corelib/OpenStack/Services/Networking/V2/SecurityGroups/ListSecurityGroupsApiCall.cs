namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Collections;
    using OpenStack.Net;

    public class ListSecurityGroupsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<SecurityGroup>>
    {
        public ListSecurityGroupsApiCall(IHttpApiCall<ReadOnlyCollectionPage<SecurityGroup>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
