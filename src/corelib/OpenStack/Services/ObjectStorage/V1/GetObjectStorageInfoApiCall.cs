namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;

#if !NET45PLUS
    using OpenStack.Collections;
#endif

    /// <summary>
    /// This class represents a prepared API call to get information about the object storage service
    /// installation, configuration, and available extensions.
    /// </summary>
    /// <seealso cref="IObjectStorageService.PrepareGetObjectStorageInfoAsync"/>
    /// <seealso cref="ObjectStorageServiceExtensions.GetObjectStorageInfoAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetObjectStorageInfoApiCall : DelegatingHttpApiCall<ReadOnlyDictionary<string, JObject>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetObjectStorageInfoApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetObjectStorageInfoApiCall(IHttpApiCall<ReadOnlyDictionary<string, JObject>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
