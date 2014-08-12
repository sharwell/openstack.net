namespace Rackspace.Services.Images.V2
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Services;
    using OpenStack.Services.Images.V2;
    using OpenStack.Threading;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class defines Rackspace-specific API extensions to the <see cref="IImageService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class ImageTasksExtensions
    {
        #region Image Tasks

        public static Task<ListTasksApiCall> PrepareListTasksAsync(this IImageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("tasks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<ImageTask<JObject>>>> deserializeResult = null;

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListTasksApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetTaskApiCall<TInput>> PrepareGetTaskAsync<TInput>(this IImageService service, ImageTaskId imageTaskId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("tasks/{task_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "task_id", imageTaskId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetTaskApiCall<TInput>(factory.CreateJsonApiCall<ImageTask<TInput>>(task.Result)));
        }

        public static Task<ImportImageApiCall> PrepareImportImageAsync(this IImageService service, ImportTaskData importTaskData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("tasks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, importTaskData, cancellationToken))
                .Select(task => new ImportImageApiCall(factory.CreateJsonApiCall<ImageTask<ImportTaskInput>>(task.Result)));
        }

        public static Task<ExportImageApiCall> PrepareExportImageAsync(this IImageService service, ExportTaskData exportTaskData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("tasks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, exportTaskData, cancellationToken))
                .Select(task => new ExportImageApiCall(factory.CreateJsonApiCall<ImageTask<ExportTaskInput>>(task.Result)));
        }

        #endregion

        #region Schemas

        public static Task<GetTaskSchemaApiCall> PrepareGetTaskSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("schemas/task");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetTaskSchemaApiCall(factory.CreateJsonApiCall<JsonSchema>(task.Result)));
        }

        public static Task<GetTasksSchemaApiCall> PrepareGetTasksSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            UriTemplate template = new UriTemplate("schemas/tasks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetTasksSchemaApiCall(factory.CreateJsonApiCall<JsonSchema>(task.Result)));
        }

        #endregion

        #region Image Tasks

        public static Task<ReadOnlyCollectionPage<ImageTask<JObject>>> ListTasksAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListTasksAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ImageTask<TInput>> GetTaskAsync<TInput>(this IImageService service, ImageTaskId imageTaskId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetTaskAsync<TInput>(imageTaskId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ImageTask<ImportTaskInput>> ImportImageAsync(this IImageService service, ImportTaskInput input, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImageTask<ImageTaskId>> progress)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareImportImageAsync(new ImportTaskData(input), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ImageTask<ExportTaskInput>> ExportImageAsync(this IImageService service, ExportTaskInput input, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImageTask<ExportTaskInput>> progress)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareExportImageAsync(new ExportTaskData(input), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion

        #region Schemas

        public static Task<JsonSchema> GetTaskSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetTaskSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<JsonSchema> GetTasksSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetTasksSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion
    }
}
