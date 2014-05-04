namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using OpenStack.Net;

    public class AddSecurityGroupApiCall : DelegatingHttpApiCall<SecurityGroupResponse>
    {
        public AddSecurityGroupApiCall(IHttpApiCall<SecurityGroupResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
