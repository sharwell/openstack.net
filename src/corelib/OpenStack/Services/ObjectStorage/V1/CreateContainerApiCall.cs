namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class CreateContainerApiCall : DelegatingHttpApiCall<string>
    {
        public CreateContainerApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
