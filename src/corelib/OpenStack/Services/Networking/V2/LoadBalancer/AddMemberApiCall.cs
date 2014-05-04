namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class AddMemberApiCall : DelegatingHttpApiCall<MemberResponse>
    {
        public AddMemberApiCall(IHttpApiCall<MemberResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
