﻿namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to get the metadata associated with an object.
    /// </summary>
    /// <seealso cref="IObjectStorageService.PrepareGetObjectMetadataAsync"/>
    /// <seealso cref="ObjectStorageServiceExtensions.GetObjectMetadataAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetObjectMetadataApiCall : DelegatingHttpApiCall<ObjectMetadata>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetObjectMetadataApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetObjectMetadataApiCall(IHttpApiCall<ObjectMetadata> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
