namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Providers;
    using net.openstack.Core.Schema;
    using net.openstack.Providers.Rackspace.Objects.Images;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    /// <summary>
    /// Represents a provider for the Rackspace Cloud Images service.
    /// </summary>
    /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/ch_image-service-dev-overview.html">Rackspace Cloud Images Developer Guide - API v2.2</seealso>
    /// <preliminary/>
    public interface IImageService
    {
        #region Image Operations

        /// <summary>
        /// Get a list of available images. This includes standard images, custom images created
        /// through <see cref="CreateImportTaskAsync"/> or <see cref="IComputeProvider.CreateImage"/>,
        /// and shared images which have been accepted by calling <see cref="UpdateImageMemberAsync"/>.
        /// </summary>
        /// <param name="marker">The image ID of the last <see cref="Image"/> in the previous page of results. If the value is <see langword="null"/>, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of <see cref="Image"/> objects to return in a single page of results. If the value is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an collection of <see cref="Image"/>
        /// objects describing the images.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="limit"/> is less than or equal to 0.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_listImages_v2_images_Image_Calls.html">List Images (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(ImageId marker, int? limit, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(ImageFilter filter, ImageId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Get detailed information about a specific image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="Image"/> object describing
        /// the images.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImage_v2_images__image_id__Image_Calls.html">Get Image Details (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<Image> GetImageAsync(ImageId imageId, CancellationToken cancellationToken);

        Task UpdateImageAsync();

        Task RemoveImageAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<ImageTask> ImportImageAsync(ImportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImageTask> progress);

        Task<ImageTask> ExportImageAsync(ExportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImageTask> progress);

        #endregion Image Operations

        #region Image Sharing Operations

        Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<ImageMember> CreateImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        Task RemoveImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        Task<ImageMember> UpdateImageMemberAsync(ImageId imageId, ProjectId memberId, MemberStatus status, CancellationToken cancellationToken);

        #endregion Image Sharing Operations

        #region Image Tag Operations

        Task AddImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken);

        Task RemoveImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken);

        #endregion Image Tag Operations

        #region Image Task Operations

        Task<ReadOnlyCollectionPage<ImageTask>> ListTasksAsync(CancellationToken cancellationToken);

        Task<ImageTask> GetTaskAsync(ImageTaskId taskId, CancellationToken cancellationToken);

        #endregion Image Task Operations

        #region JSON Schema Operations

        /// <summary>
        /// Gets a json-schema document that represents a collection of <see cref="Image"/> entities.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImagesSchema_v2_schemas_images_Schema_Calls.html">Get Images Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetImagesSchemaAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a json-schema document that represents a single <see cref="Image"/> entity.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImageSchema_v2_schemas_image_Schema_Calls.html">Get Image Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetImageSchemaAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a json-schema document that represents a collection of <see cref="ImageMember"/> entities.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImageMembersSchemas_v2_schemas_members_Schema_Calls.html">Get Image Members Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetImageMembersSchemaAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a json-schema document that represents a single <see cref="ImageMember"/> entity.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImageMemberSchema_v2_schemas_member_Schema_Calls.html">Get Image Member Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetImageMemberSchemaAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a json-schema document that represents a collection of <see cref="ImageTask"/> entities.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getTasksSchemas_v2_schemas_members_Schema_Calls.html">Get Tasks Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetTasksSchemaAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a json-schema document that represents a single <see cref="ImageTask"/> entity.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the operation
        /// completes, the <see cref="Task{TResult}.Result"/> property will contain a <see cref="JsonSchema"/>
        /// object describing the JSON schema.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getTaskSchema_v2_schemas_member_Schema_Calls.html">Get Task Schema (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<JsonSchema> GetTaskSchemaAsync(CancellationToken cancellationToken);

        #endregion JSON Schema Operations
    }
}
