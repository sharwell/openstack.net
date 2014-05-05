namespace OpenStack.Services.Networking.V2.Metering
{
    using OpenStack.Net;

    public class GetMeteringLabelRuleApiCall : DelegatingHttpApiCall<MeteringLabelRuleResponse>
    {
        public GetMeteringLabelRuleApiCall(IHttpApiCall<MeteringLabelRuleResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
