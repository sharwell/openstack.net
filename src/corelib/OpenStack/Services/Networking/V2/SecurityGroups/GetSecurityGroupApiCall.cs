namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class GetSecurityGroupApiCall : DelegatingHttpApiCall<SecurityGroupResponse>
    {
        public GetSecurityGroupApiCall(IHttpApiCall<SecurityGroupResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
