namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class AddPortApiCall : DelegatingHttpApiCall<PortResponse>
    {
        public AddPortApiCall(IHttpApiCall<PortResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
