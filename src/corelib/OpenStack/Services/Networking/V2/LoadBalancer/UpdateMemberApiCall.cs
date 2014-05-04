namespace OpenStack.Services.Networking.V2.LoadBalancer
{
    using OpenStack.Net;

    public class UpdateMemberApiCall : DelegatingHttpApiCall<MemberResponse>
    {
        public UpdateMemberApiCall(IHttpApiCall<MemberResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
