namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class AddMeteringLabelApiCall : DelegatingHttpApiCall<MeteringLabelResponse>
    {
        public AddMeteringLabelApiCall(IHttpApiCall<MeteringLabelResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
