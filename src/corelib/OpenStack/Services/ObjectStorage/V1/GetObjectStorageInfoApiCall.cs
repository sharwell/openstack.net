namespace OpenStack.Services.ObjectStorage.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;

#if !NET45PLUS
    using OpenStack.Collections;
#endif

    public class GetObjectStorageInfoApiCall : DelegatingHttpApiCall<ReadOnlyDictionary<string, JObject>>
    {
        public GetObjectStorageInfoApiCall(IHttpApiCall<ReadOnlyDictionary<string, JObject>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
