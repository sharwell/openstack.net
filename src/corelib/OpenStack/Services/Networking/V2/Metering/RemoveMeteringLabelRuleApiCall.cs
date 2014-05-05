namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class RemoveMeteringLabelRuleApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveMeteringLabelRuleApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
