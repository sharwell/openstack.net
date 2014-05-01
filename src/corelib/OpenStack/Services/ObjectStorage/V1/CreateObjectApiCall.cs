namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class CreateObjectApiCall : DelegatingHttpApiCall<string>
    {
        public CreateObjectApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
