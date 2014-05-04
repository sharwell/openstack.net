namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class GetMemberApiCall : DelegatingHttpApiCall<MemberResponse>
    {
        public GetMemberApiCall(IHttpApiCall<MemberResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
