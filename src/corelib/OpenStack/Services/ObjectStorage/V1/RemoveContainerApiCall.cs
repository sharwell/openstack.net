namespace OpenStack.Services.ObjectStorage.V1
{
    using OpenStack.Net;

    public class RemoveContainerApiCall : DelegatingHttpApiCall<string>
    {
        public RemoveContainerApiCall(IHttpApiCall<string> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
