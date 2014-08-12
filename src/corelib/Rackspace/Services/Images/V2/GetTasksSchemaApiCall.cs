namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json.Schema;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to get the JSON schema associated with a
    /// list of image task resources returned by the <see cref="IImageService"/>. This schema
    /// describes the JSON representation returned by the <see cref="ListTasksApiCall"/> API
    /// call.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.PrepareGetTasksSchemaAsync"/>
    /// <seealso cref="ImageTasksExtensions.GetTasksSchemaAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetTasksSchemaApiCall : DelegatingHttpApiCall<JsonSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTasksSchemaApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetTasksSchemaApiCall(IHttpApiCall<JsonSchema> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
