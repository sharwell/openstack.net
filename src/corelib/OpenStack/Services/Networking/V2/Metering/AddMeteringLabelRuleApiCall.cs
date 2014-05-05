namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class AddMeteringLabelRuleApiCall : DelegatingHttpApiCall<MeteringLabelRuleResponse>
    {
        public AddMeteringLabelRuleApiCall(IHttpApiCall<MeteringLabelRuleResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
