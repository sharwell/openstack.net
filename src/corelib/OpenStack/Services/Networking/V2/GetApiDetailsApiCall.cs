namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class GetApiDetailsApiCall : DelegatingHttpApiCall<ApiDetails>
    {
        public GetApiDetailsApiCall(IHttpApiCall<ApiDetails> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
