namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class RemoveMeteringLabelApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveMeteringLabelApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
