namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Schema;
    using net.openstack.Providers.Rackspace.Objects.Images;
    using Newtonsoft.Json.Linq;
    using CloudIdentity = net.openstack.Core.Domain.CloudIdentity;
    using Endpoint = net.openstack.Core.Domain.Endpoint;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;
    using HttpResponseCodeValidator = net.openstack.Providers.Rackspace.Validators.HttpResponseCodeValidator;
    using IdentityToken = net.openstack.Core.Domain.IdentityToken;
    using IHttpResponseCodeValidator = net.openstack.Core.Validators.IHttpResponseCodeValidator;
    using IIdentityProvider = net.openstack.Core.Providers.IIdentityProvider;
    using IRestService = JSIStudios.SimpleRESTServices.Client.IRestService;
    using JsonRestServices = JSIStudios.SimpleRESTServices.Client.Json.JsonRestServices;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    public class CloudImagesProvider : ProviderBase<IImageService>, IImageService
    {
        /// <summary>
        /// This field caches the base URI used for accessing the Auto Scale service.
        /// </summary>
        /// <seealso cref="GetBaseUriAsync"/>
        private Uri _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudAutoScaleProvider"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        public CloudImagesProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider)
            : base(defaultIdentity, defaultRegion, identityProvider, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudMonitoringProvider"/> class with
        /// the specified values.
        /// </summary>
        /// <param name="defaultIdentity">The default identity to use for calls that do not explicitly specify an identity. If this value is <see langword="null"/>, no default identity is available so all calls must specify an explicit identity.</param>
        /// <param name="defaultRegion">The default region to use for calls that do not explicitly specify a region. If this value is <see langword="null"/>, the default region for the user will be used; otherwise if the service uses region-specific endpoints all calls must specify an explicit region.</param>
        /// <param name="identityProvider">The identity provider to use for authenticating requests to this provider. If this value is <see langword="null"/>, a new instance of <see cref="CloudIdentityProvider"/> is created using <paramref name="defaultIdentity"/> as the default identity.</param>
        /// <param name="restService">The implementation of <see cref="IRestService"/> to use for executing synchronous REST requests. If this value is <see langword="null"/>, the provider will use a new instance of <see cref="JsonRestServices"/>.</param>
        /// <param name="httpStatusCodeValidator">The HTTP status code validator to use for synchronous REST requests. If this value is <see langword="null"/>, the provider will use <see cref="HttpResponseCodeValidator.Default"/>.</param>
        protected CloudImagesProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider, IRestService restService, IHttpResponseCodeValidator httpStatusCodeValidator)
            : base(defaultIdentity, defaultRegion, identityProvider, restService, httpStatusCodeValidator)
        {
        }

        #region IImageService Members

        /// <inheritdoc/>
        public Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(ImageId marker, int? limit, CancellationToken cancellationToken)
        {
            return ListImagesAsync(null, marker, limit, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(ImageFilter filter, ImageId marker, int? limit, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/images?marker={marker}&limit={limit}");
            var parameters = new Dictionary<string, string>();
            if (marker != null)
                parameters.Add("marker", marker.Value);
            if (limit.HasValue)
                parameters.Add("limit", limit.ToString());

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<JToken>> requestResource =
                GetResponseAsyncFunc<JToken>(cancellationToken);

            Func<Task<JToken>, ReadOnlyCollectionPage<Image>> resultSelector =
                task =>
                {
                    if (task.Result == null)
                        return ReadOnlyCollectionPage<Image>.Empty;

                    JToken imagesToken = task.Result["images"];
                    if (imagesToken == null)
                        return ReadOnlyCollectionPage<Image>.Empty;

                    Image[] images = imagesToken.ToObject<Image[]>();
                    if (images.Length == 0 || task.Result["next"] == null)
                        return new BasicReadOnlyCollectionPage<Image>(images, null);

                    Func<CancellationToken, Task<ReadOnlyCollectionPage<Image>>> getNextPageAsync =
                        nextCancellationToken => ListImagesAsync(filter, images[images.Length - 1].Id, limit, nextCancellationToken);

                    return new BasicReadOnlyCollectionPage<Image>(images, getNextPageAsync);
                };

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource)
                .Select(resultSelector);
        }

        /// <inheritdoc/>
        public Task<Image> GetImageAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/images/{imageId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<Image>> requestResource =
                GetResponseAsyncFunc<Image>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task UpdateImageAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task RemoveImageAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/images/{imageId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.DELETE, template, parameters);

            Func<Task<HttpWebRequest>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        public virtual Task<ImportImageTask> ImportImageAsync(ImportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImportImageTask> progress)
        {
            Task<ImportImageTask> initialTask = CreateImportTaskAsync(descriptor, cancellationToken);
            if (completionOption != AsyncCompletionOption.RequestCompleted)
                return initialTask;

            Func<Task<ImportImageTask>, Task<ImportImageTask>> resultSelector =
                task =>
                {
                    ImportImageTask imageTask = task.Result;
                    if (imageTask != null)
                        return WaitForTaskCompletionAsync<ImportImageTask>(imageTask.Id, cancellationToken, progress);

                    return task;
                };

            return initialTask.Then(resultSelector);
        }

        public virtual Task<ExportImageTask> ExportImageAsync(ExportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ExportImageTask> progress)
        {
            Task<ExportImageTask> initialTask = CreateExportTaskAsync(descriptor, cancellationToken);
            if (completionOption != AsyncCompletionOption.RequestCompleted)
                return initialTask;

            Func<Task<ExportImageTask>, Task<ExportImageTask>> resultSelector =
                task =>
                {
                    ExportImageTask imageTask = task.Result;
                    if (imageTask != null)
                        return WaitForTaskCompletionAsync(imageTask.Id, cancellationToken, progress);

                    return task;
                };

            return initialTask.Then(resultSelector);
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");

            UriTemplate template = new UriTemplate("/images/{imageId}/members");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<JObject>> requestResource =
                GetResponseAsyncFunc<JObject>(cancellationToken);

            Func<Task<JObject>, ReadOnlyCollectionPage<ImageMember>> selector =
                task =>
                {
                    JToken membersArray = task.Result["members"];
                    ImageMember[] members = null;
                    if (membersArray != null)
                        members = membersArray.ToObject<ImageMember[]>();

                    return new BasicReadOnlyCollectionPage<ImageMember>(members, null);
                };

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource)
                .Select(selector);
        }

        /// <inheritdoc/>
        public Task<ImageMember> CreateImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (memberId == null)
                throw new ArgumentNullException("memberId");

            UriTemplate template = new UriTemplate("/images/{imageId}/members");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };

            JObject body =
                new JObject(
                    new JProperty("member", memberId.Value));
            Func<Task<Tuple<IdentityToken, Uri>>, Task<HttpWebRequest>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.POST, template, parameters, body);

            Func<Task<HttpWebRequest>, Task<ImageMember>> requestResource =
                GetResponseAsyncFunc<ImageMember>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task RemoveImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (memberId == null)
                throw new ArgumentNullException("memberId");

            UriTemplate template = new UriTemplate("/images/{imageId}/members/{memberId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value }, { "memberId", memberId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.DELETE, template, parameters);

            Func<Task<HttpWebRequest>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task<ImageMember> UpdateImageMemberAsync(ImageId imageId, ProjectId memberId, MemberStatus status, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (memberId == null)
                throw new ArgumentNullException("memberId");
            if (status == null)
                throw new ArgumentNullException("status");

            UriTemplate template = new UriTemplate("/images/{imageId}/members/{memberId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value }, { "memberId", memberId.Value } };

            JObject body =
                new JObject(
                    new JProperty("status", JToken.FromObject(status)));
            Func<Task<Tuple<IdentityToken, Uri>>, Task<HttpWebRequest>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.PUT, template, parameters, body);

            Func<Task<HttpWebRequest>, Task<ImageMember>> requestResource =
                GetResponseAsyncFunc<ImageMember>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task AddImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (tag == null)
                throw new ArgumentNullException("tag");

            UriTemplate template = new UriTemplate("/images/{imageId}/tags/{tag}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value }, { "tag", tag.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.PUT, template, parameters);

            Func<Task<HttpWebRequest>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task RemoveImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (tag == null)
                throw new ArgumentNullException("tag");

            UriTemplate template = new UriTemplate("/images/{imageId}/tags/{tag}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value }, { "tag", tag.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.DELETE, template, parameters);

            Func<Task<HttpWebRequest>, Task<string>> requestResource =
                GetResponseAsyncFunc(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollectionPage<GenericImageTask>> ListTasksAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/tasks");
            var parameters = new Dictionary<string, string>();

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<JObject>> requestResource =
                GetResponseAsyncFunc<JObject>(cancellationToken);

            Func<Task<JObject>, ReadOnlyCollectionPage<GenericImageTask>> selector =
                task =>
                {
                    JToken tasksArray = task.Result["tasks"];
                    GenericImageTask[] tasks = null;
                    if (tasksArray != null)
                        tasks = tasksArray.ToObject<GenericImageTask[]>();

                    return new BasicReadOnlyCollectionPage<GenericImageTask>(tasks, null);
                };

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource)
                .Select(selector);
        }

        /// <inheritdoc/>
        public Task<TTask> GetTaskAsync<TTask>(ImageTaskId taskId, CancellationToken cancellationToken)
            where TTask : ImageTask
        {
            UriTemplate template = new UriTemplate("/tasks/{taskId}");
            var parameters = new Dictionary<string, string>() { { "taskId", taskId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<TTask>> requestResource =
                GetResponseAsyncFunc<TTask>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetImagesSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/images");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetImageSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/image");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetImageMembersSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/members");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetImageMemberSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/member");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetTasksSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/tasks");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<JsonSchema> GetTaskSchemaAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("/schemas/task");
            var parameters = new Dictionary<string, string>();
            return GetSchemaAsync(template, parameters, cancellationToken);
        }

        protected virtual Task<JsonSchema> GetSchemaAsync(UriTemplate template, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<JsonSchema>> requestResource =
                GetResponseAsyncFunc<JsonSchema>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
        }

        #endregion

        protected virtual Task<ImportImageTask> CreateImportTaskAsync(ImportTaskDescriptor descriptor, CancellationToken cancellationToken)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            return CreateTaskAsync<ImportImageTask>(descriptor, cancellationToken);
        }

        protected virtual Task<ExportImageTask> CreateExportTaskAsync(ExportTaskDescriptor descriptor, CancellationToken cancellationToken)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            return CreateTaskAsync<ExportImageTask>(descriptor, cancellationToken);
        }

        protected virtual Task<TTask> CreateTaskAsync<TTask>(object descriptor, CancellationToken cancellationToken)
            where TTask : ImageTask
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            UriTemplate template = new UriTemplate("/tasks");
            var parameters = new Dictionary<string, string>();

            Func<Task<Tuple<IdentityToken, Uri>>, Task<HttpWebRequest>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.POST, template, parameters, descriptor);

            Func<Task<HttpWebRequest>, Task<TTask>> requestResource =
                GetResponseAsyncFunc<TTask>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        protected Task<TTask> WaitForTaskCompletionAsync<TTask>(ImageTaskId taskId, CancellationToken cancellationToken, IProgress<TTask> progress)
            where TTask : ImageTask
        {
            if (taskId == null)
                throw new ArgumentNullException("taskId");

            TaskCompletionSource<TTask> taskCompletionSource = new TaskCompletionSource<TTask>();
            Func<Task<TTask>> pollImageTask = () => PollImageTaskAsync(taskId, cancellationToken, progress);

            IEnumerator<TimeSpan> backoffPolicy = BackoffPolicy.GetBackoffIntervals().GetEnumerator();
            Func<Task<TTask>> moveNext =
                () =>
                {
                    if (!backoffPolicy.MoveNext())
                        throw new OperationCanceledException();

                    if (backoffPolicy.Current == TimeSpan.Zero)
                    {
                        return pollImageTask();
                    }
                    else
                    {
                        return Task.Factory.StartNewDelayed((int)backoffPolicy.Current.TotalMilliseconds, cancellationToken).ContinueWith(
                           task =>
                           {
                               task.PropagateExceptions();
                               return pollImageTask();
                           }, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
                    }
                };

            Task<TTask> currentTask = moveNext();
            Action<Task<TTask>> continuation = null;
            continuation =
                previousTask =>
                {
                    if (previousTask.Status != TaskStatus.RanToCompletion)
                    {
                        taskCompletionSource.SetFromTask(previousTask);
                        return;
                    }

                    TTask result = previousTask.Result;
                    if (result == null || result.Status == ImageTaskStatus.Failure || result.Status == ImageTaskStatus.Success)
                    {
                        // finished waiting
                        taskCompletionSource.SetResult(result);
                        return;
                    }

                    // reschedule
                    currentTask = moveNext();
                    currentTask.ContinueWith(continuation, TaskContinuationOptions.ExecuteSynchronously);
                };
            currentTask.ContinueWith(continuation, TaskContinuationOptions.ExecuteSynchronously);

            return taskCompletionSource.Task;
        }

        private Task<TTask> PollImageTaskAsync<TTask>(ImageTaskId taskId, CancellationToken cancellationToken, IProgress<TTask> progress)
            where TTask : ImageTask
        {
            Task<TTask> chain = GetTaskAsync<TTask>(taskId, cancellationToken);
            chain = chain.ContinueWith(
                task =>
                {
                    if (task.Result == null || task.Result.Id != taskId)
                        throw new InvalidOperationException("Could not obtain status for task");

                    return task.Result;
                }, TaskContinuationOptions.ExecuteSynchronously);

            if (progress != null)
            {
                chain = chain.Select(
                    task =>
                    {
                        progress.Report(task.Result);
                        return task.Result;
                    });
            }

            return chain;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method returns a cached base address if one is available. If no cached address is
        /// available, <see cref="ProviderBase{TProvider}.GetServiceEndpoint"/> is called to obtain
        /// an <see cref="Endpoint"/> with the type <c>image</c> and preferred name <c>cloudImages</c>.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsync(CancellationToken cancellationToken)
        {
            if (_baseUri != null)
            {
                return InternalTaskExtensions.CompletedTask(_baseUri);
            }

            return Task.Factory.StartNew(
                () =>
                {
                    Endpoint endpoint = GetServiceEndpoint(null, "image", "cloudImages", null);
                    _baseUri = new Uri(endpoint.PublicURL);
                    return _baseUri;
                });
        }
    }
}
