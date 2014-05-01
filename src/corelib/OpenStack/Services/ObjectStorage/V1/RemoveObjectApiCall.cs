namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class RemoveObjectApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveObjectApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
