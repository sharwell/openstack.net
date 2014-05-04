namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class GetPortApiCall : DelegatingHttpApiCall<PortResponse>
    {
        public GetPortApiCall(IHttpApiCall<PortResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
