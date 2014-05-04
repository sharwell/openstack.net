namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class AddSubnetApiCall : DelegatingHttpApiCall<SubnetResponse>
    {
        public AddSubnetApiCall(IHttpApiCall<SubnetResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
