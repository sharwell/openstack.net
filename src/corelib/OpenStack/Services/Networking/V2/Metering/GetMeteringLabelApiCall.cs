namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class GetMeteringLabelApiCall : DelegatingHttpApiCall<MeteringLabelResponse>
    {
        public GetMeteringLabelApiCall(IHttpApiCall<MeteringLabelResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
