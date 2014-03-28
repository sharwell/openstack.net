namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Schema;
    using net.openstack.Providers.Rackspace.Objects.Images;
    using Newtonsoft.Json.Linq;
    using CloudIdentity = net.openstack.Core.Domain.CloudIdentity;
    using ContentType = System.Net.Mime.ContentType;
    using Endpoint = net.openstack.Core.Domain.Endpoint;
    using HttpMethod = JSIStudios.SimpleRESTServices.Client.HttpMethod;
    using HttpResponseCodeValidator = net.openstack.Providers.Rackspace.Validators.HttpResponseCodeValidator;
    using IdentityToken = net.openstack.Core.Domain.IdentityToken;
    using IHttpResponseCodeValidator = net.openstack.Core.Validators.IHttpResponseCodeValidator;
    using IIdentityProvider = net.openstack.Core.Providers.IIdentityProvider;
    using IRestService = JSIStudios.SimpleRESTServices.Client.IRestService;
    using JsonRestServices = JSIStudios.SimpleRESTServices.Client.Json.JsonRestServices;
    using ProjectId = net.openstack.Core.Domain.ProjectId;
    using StringBuilder = System.Text.StringBuilder;

    /// <summary>
    /// Provides an implementation of <see cref="IImageService"/> for operating
    /// with Rackspace's Cloud Images product.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/ch_image-service-dev-overview.html">Rackspace Cloud Images Developer Guide - API v2.2</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
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
            UriTemplate template;
            if (filter != null)
            {
                StringBuilder builder = new StringBuilder("/images?marker={marker}&limit={limit}");
                foreach (string key in filter.QueryParameters)
                {
                    builder.Append('&');
                    builder.Append(key).Append("={").Append(key).Append('}');
                }

                template = new UriTemplate(builder.ToString());
            }
            else
            {
                template = new UriTemplate("/images?marker={marker}&limit={limit}");
            }

            var parameters = new Dictionary<string, string>();
            if (marker != null)
                parameters.Add("marker", marker.Value);
            if (limit.HasValue)
                parameters.Add("limit", limit.ToString());
            if (filter != null)
            {
                foreach (var pair in filter.Values)
                    parameters.Add(pair.Key, pair.Value);
            }

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
            if (imageId == null)
                throw new ArgumentNullException("imageId");

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
        public Task<Image> UpdateImageAsync(ImageId imageId, IEnumerable<ImageUpdateOperation> operations, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");

            ImageUpdateOperation[] operationsArray = operations.ToArray();
            if (operationsArray.Contains(null))
                throw new ArgumentException("operations cannot contain any null values", "operations");

            UriTemplate template = new UriTemplate("/images/{imageId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, Task<HttpWebRequest>> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.PATCH, template, parameters, operationsArray);

            Func<Task<HttpWebRequest>, Task<Image>> requestResource =
                GetResponseAsyncFunc<Image>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Then(prepareRequest)
                .Then(requestResource);
        }

        /// <inheritdoc/>
        public Task RemoveImageAsync(ImageId imageId, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");

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

        /// <inheritdoc/>
        public virtual Task<ImportImageTask> ImportImageAsync(ImportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImportImageTask> progress)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

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

        /// <inheritdoc/>
        public virtual Task<ExportImageTask> ExportImageAsync(ExportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ExportImageTask> progress)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

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

            return ListImageMembersAsync(imageId, null, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(ImageId imageId, ImageMemberFilter filter, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");

            UriTemplate template;
            if (filter != null)
            {
                StringBuilder builder = new StringBuilder("/images/{imageId}/members");
                bool first = true;
                foreach (string key in filter.QueryParameters)
                {
                    builder.Append(first ? '?' : '&');
                    builder.Append(key).Append("={").Append(key).Append('}');
                    first = false;
                }

                template = new UriTemplate(builder.ToString());
            }
            else
            {
                template = new UriTemplate("/images/{imageId}/members");
            }

            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value } };
            if (filter != null)
            {
                foreach (var pair in filter.Values)
                    parameters.Add(pair.Key, pair.Value);
            }

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
        public Task<ImageMember> GetImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            if (imageId == null)
                throw new ArgumentNullException("imageId");
            if (memberId == null)
                throw new ArgumentNullException("memberId");

            UriTemplate template = new UriTemplate("/images/{imageId}/members/{memberId}");
            var parameters = new Dictionary<string, string>() { { "imageId", imageId.Value }, { "memberId", memberId.Value } };

            Func<Task<Tuple<IdentityToken, Uri>>, HttpWebRequest> prepareRequest =
                PrepareRequestAsyncFunc(HttpMethod.GET, template, parameters);

            Func<Task<HttpWebRequest>, Task<ImageMember>> requestResource =
                GetResponseAsyncFunc<ImageMember>(cancellationToken);

            return AuthenticateServiceAsync(cancellationToken)
                .Select(prepareRequest)
                .Then(requestResource);
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

        /// <summary>
        /// Gets a json-schema from a web service using a <see cref="HttpMethod.GET"/> request.
        /// </summary>
        /// <param name="template">The <see cref="UriTemplate"/> describing the location of the JSON schema.</param>
        /// <param name="parameters">A collection of named parameters to replace in the <paramref name="template"/> to form the final <see cref="Uri"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="template"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="parameters"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <inheritdoc/>
        /// <remarks>
        /// The <see cref="CloudImagesProvider"/> implementation of this method alters the
        /// resulting <see cref="HttpWebRequest"/> when the <paramref name="method"/> is
        /// <see cref="HttpMethod.PATCH"/> by properly setting the
        /// <see cref="HttpWebRequest.ContentType"/> property to
        /// <c>application/openstack-images-v2.1-json-patch</c>.
        /// </remarks>
        protected override HttpWebRequest PrepareRequestImpl(HttpMethod method, IdentityToken identityToken, UriTemplate template, Uri baseUri, IDictionary<string, string> parameters, Func<Uri, Uri> uriTransform)
        {
            HttpWebRequest request = base.PrepareRequestImpl(method, identityToken, template, baseUri, parameters, uriTransform);
            if (method == HttpMethod.PATCH)
                request.ContentType = new ContentType() { MediaType = "application/openstack-images-v2.1-json-patch", CharSet = "UTF-8" }.ToString();

            return request;
        }

        /// <summary>
        /// Create an image task to perform an asynchronous import operation within the Image Service.
        /// </summary>
        /// <remarks>
        /// The base implementation calls <see cref="CreateTaskAsync{TTask}"/> to provide the underlying
        /// functionality.
        /// </remarks>
        /// <param name="descriptor">An <see cref="ImportTaskDescriptor"/> providing the arguments used to create the import image task.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe. Note that this parameter only affects the asynchronous operation to <em>create</em> the image task; it does not affect the image task itself operating within the Image Service.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="ImportImageTask"/>
        /// object describing the asynchronous image import operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="descriptor"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        protected virtual Task<ImportImageTask> CreateImportTaskAsync(ImportTaskDescriptor descriptor, CancellationToken cancellationToken)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            return CreateTaskAsync<ImportImageTask>(descriptor, cancellationToken);
        }

        /// <summary>
        /// Create an image task to perform an asynchronous export operation within the Image Service.
        /// </summary>
        /// <remarks>
        /// The base implementation calls <see cref="CreateTaskAsync{TTask}"/> to provide the underlying
        /// functionality.
        /// </remarks>
        /// <param name="descriptor">An <see cref="ExportTaskDescriptor"/> providing the arguments used to create the export image task.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe. Note that this parameter only affects the asynchronous operation to <em>create</em> the image task; it does not affect the image task itself operating within the Image Service.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="ExportImageTask"/>
        /// object describing the asynchronous image export operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="descriptor"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        protected virtual Task<ExportImageTask> CreateExportTaskAsync(ExportTaskDescriptor descriptor, CancellationToken cancellationToken)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            return CreateTaskAsync<ExportImageTask>(descriptor, cancellationToken);
        }

        /// <summary>
        /// Create a generic image task to perform an asynchronous operation within the Image Service.
        /// </summary>
        /// <remarks>
        /// The base implementation always uses a <see cref="HttpMethod.POST"/> operation to create
        /// the task, based on an arbitrary <paramref name="descriptor"/>. The caller is responsible
        /// for providing a descriptor that the Image Service provider recognizes, and for using
        /// a <typeparamref name="TTask"/> type suitable for modeling the result. For general cases,
        /// the <see cref="GenericImageTask"/> may be used if a more specific type has not been
        /// defined.
        /// </remarks>
        /// <typeparam name="TTask">The specific <see cref="ImageTask"/> type used to model the JSON representation of the resulting type of task.</typeparam>
        /// <param name="descriptor">An object modeling the JSON representation of the descriptor used to create the image task.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe. Note that this parameter only affects the asynchronous operation to <em>create</em> the image task; it does not affect the image task itself operating within the Image Service.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <typeparamref name="TTask"/>
        /// object describing the image task.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="descriptor"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
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

        /// <summary>
        /// Creates a <see cref="Task"/> that will complete after an image task enters the
        /// <see cref="ImageTaskStatus.Success"/> or <see cref="ImageTaskStatus.Failure"/> state.
        /// </summary>
        /// <remarks>
        /// The task is considered complete as soon as a call to <see cref="IImageService.GetTaskAsync{TTask}"/>
        /// indicates that the task is in the <see cref="ImageTaskStatus.Success"/> or
        /// <see cref="ImageTaskStatus.Failure"/> state. The method does not perform any other checks
        /// related to the initial or final state of the task.
        ///
        /// <para>The polling intervals used for this method are provided by the <see cref="BackoffPolicy"/>
        /// property.</para>
        /// </remarks>
        /// <typeparam name="TTask">The specific <see cref="ImageTask"/> type used to model the JSON representation of the resulting type of task.</typeparam>
        /// <param name="taskId">The image task ID. This is obtained from <see cref="ImageTask.Id">ImageTask.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property will contain a
        /// <typeparamref name="TTask"/> object representing the final state of the task. In addition,
        /// the <see cref="ImageTask.Status"/> property of the task will be either
        /// <see cref="ImageTaskStatus.Success"/> or <see cref="ImageTaskStatus.Failure"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="taskId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If a REST request does not return successfully.</exception>
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
                        return Task.Factory.StartNewDelayed((int)backoffPolicy.Current.TotalMilliseconds, cancellationToken)
                            .Then(task => pollImageTask());
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
                    // use ContinueWith since the continuation handles cancellation and faulted antecedent tasks
                    currentTask.ContinueWith(continuation, TaskContinuationOptions.ExecuteSynchronously);
                };
            // use ContinueWith since the continuation handles cancellation and faulted antecedent tasks
            currentTask.ContinueWith(continuation, TaskContinuationOptions.ExecuteSynchronously);

            return taskCompletionSource.Task;
        }

        private Task<TTask> PollImageTaskAsync<TTask>(ImageTaskId taskId, CancellationToken cancellationToken, IProgress<TTask> progress)
            where TTask : ImageTask
        {
            Task<TTask> chain = GetTaskAsync<TTask>(taskId, cancellationToken);
            chain = chain.Select(
                task =>
                {
                    if (task.Result == null || task.Result.Id != taskId)
                        throw new InvalidOperationException("Could not obtain status for task");

                    return task.Result;
                }, true);

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
