namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class GetSubnetApiCall : DelegatingHttpApiCall<SubnetResponse>
    {
        public GetSubnetApiCall(IHttpApiCall<SubnetResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
