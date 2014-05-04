namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class UpdateSubnetApiCall : DelegatingHttpApiCall<SubnetResponse>
    {
        public UpdateSubnetApiCall(IHttpApiCall<SubnetResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
