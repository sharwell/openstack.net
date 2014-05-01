namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class CopyObjectApiCall : DelegatingHttpApiCall<string>
    {
        public CopyObjectApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
