namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class UpdatePortApiCall : DelegatingHttpApiCall<PortResponse>
    {
        public UpdatePortApiCall(IHttpApiCall<PortResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
