namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json.Schema;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to get the JSON schema associated with an
    /// image task resource returned by the <see cref="IImageService"/>. This schema
    /// describes the JSON representation returned by the <see cref="GetTaskSchemaApiCall"/> API
    /// call.
    /// </summary>
    /// <seealso cref="ImageTasksExtensions.PrepareGetTaskSchemaAsync"/>
    /// <seealso cref="ImageTasksExtensions.GetTaskSchemaAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetTaskSchemaApiCall : DelegatingHttpApiCall<JsonSchema>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTaskSchemaApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetTaskSchemaApiCall(IHttpApiCall<JsonSchema> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
