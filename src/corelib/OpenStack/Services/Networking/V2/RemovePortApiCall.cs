namespace OpenStack.Services.Networking.V2
{
    using OpenStack.Net;

    public class RemovePortApiCall : DelegatingHttpApiCall<string>
    {
        public RemovePortApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
