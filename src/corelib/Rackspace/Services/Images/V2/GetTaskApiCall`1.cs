namespace Rackspace.Services.Images.V2
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Net;
    using OpenStack.Services.Images.V2;

    /// <summary>
    /// This class represents a prepared API call to get a task resource from the
    /// <see cref="IImageService"/>.
    /// </summary>
    /// <typeparam name="TInput">
    /// The type modeling the JSON representation of the task input parameters. This may be <see cref="JObject"/>
    /// for an non-specific task (e.g. the values returned by <see cref="ListTasksApiCall"/>), or
    /// a specific type such as <see cref="ImportTaskInput"/> or <see cref="ExportTaskInput"/> when
    /// the task type is known in advance.
    /// </typeparam>
    /// <seealso cref="ImageTasksExtensions.PrepareGetTaskAsync{TInput}"/>
    /// <seealso cref="ImageTasksExtensions.GetTaskAsync{TInput}"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class GetTaskApiCall<TInput> : DelegatingHttpApiCall<ImageTask<TInput>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTaskApiCall{TInput}"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public GetTaskApiCall(IHttpApiCall<ImageTask<TInput>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
