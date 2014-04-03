namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Providers;
    using net.openstack.Providers.Rackspace.Objects.Images;
    using Newtonsoft.Json.Schema;
    using ProjectId = net.openstack.Core.Domain.ProjectId;
    using Tenant = net.openstack.Core.Domain.Tenant;

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
        /// through <see cref="ImportImageAsync"/> or <see cref="IComputeProvider.CreateImage"/>,
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

        /// <summary>
        /// Get a list of available images, filtered according to the specified filter. This includes
        /// standard images, custom images created through <see cref="ImportImageAsync"/> or
        /// <see cref="IComputeProvider.CreateImage"/>, and shared images which have been accepted by
        /// calling <see cref="UpdateImageMemberAsync"/>.
        /// </summary>
        /// <param name="filter">An <see cref="ImageFilter"/> instance describing the parameters used for filtering the result list.</param>
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
        Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(ImageFilter filter, ImageId marker, int? limit, CancellationToken cancellationToken);

        /// <summary>
        /// Get detailed information about a specific image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="Image"/> object describing
        /// the image.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_getImage_v2_images__image_id__Image_Calls.html">Get Image Details (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<Image> GetImageAsync(ImageId imageId, CancellationToken cancellationToken);

        /// <summary>
        /// Update properties of an image resource.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="operations">A collection of <see cref="ImageUpdateOperation"/> objects describing the updates to apply to the image.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="Image"/> object describing
        /// the updated image.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="operations"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="operations"/> contains any <see langword="null"/> values.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/PATCH_updateImage_images__image_id__Image_Calls.html">Update Image (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<Image> UpdateImageAsync(ImageId imageId, IEnumerable<ImageUpdateOperation> operations, CancellationToken cancellationToken);

        /// <summary>
        /// Remove and delete an image from the image service.
        /// </summary>
        /// <param name="imageId">The image ID of the image to remove. This is obtained from <see cref="Image.Id">Image.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="imageId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/DELETE_deleteImage_v2_images__image_id__Image_Calls.html">Delete Images (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task RemoveImageAsync(ImageId imageId, CancellationToken cancellationToken);

        /// <summary>
        /// Import an image.
        /// </summary>
        /// <param name="descriptor">An <see cref="ImportTaskDescriptor"/> instance describing the image source location and target properties.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="ImportImageTask"/>
        /// object describing the asynchronous import operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the task will not be considered complete until
        /// the image task transitions to the <see cref="ImageTaskStatus.Success"/> or <see cref="ImageTaskStatus.Failure"/> state.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="descriptor"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_importTask_v2_tasks_Image_Task_Calls.html">Import Task (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ImportImageTask> ImportImageAsync(ImportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ImportImageTask> progress);

        /// <summary>
        /// Export an image.
        /// </summary>
        /// <param name="descriptor">An <see cref="ExportTaskDescriptor"/> instance describing the image source and target location properties.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="ExportImageTask"/>
        /// object describing the asynchronous export operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the task will not be considered complete until
        /// the image task transitions to the <see cref="ImageTaskStatus.Success"/> or <see cref="ImageTaskStatus.Failure"/> state.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="descriptor"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_importTask_v2_tasks_Image_Task_Calls.html">Import Task (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ExportImageTask> ExportImageAsync(ExportTaskDescriptor descriptor, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ExportImageTask> progress);

        #endregion Image Operations

        #region Image Sharing Operations

        /// <summary>
        /// List the image members for a particular image. Image members are resources that describe other project IDs
        /// which are granted access to the image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a collection of <see cref="ImageMember"/>
        /// objects describing the image members for the specified image.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="imageId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_listImageMembers_v2_images__image_id__members_Image_Both_Calls.html">List Image Members (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(ImageId imageId, CancellationToken cancellationToken);

        /// <summary>
        /// List the image members for a particular image. Image members are resources that describe other project IDs
        /// which are granted access to the image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="filter">An <see cref="ImageMemberFilter"/> instance describing the parameters used for filtering the result list.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a collection of <see cref="ImageMember"/>
        /// objects describing the image members for the specified image.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="imageId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_listImageMembers_v2_images__image_id__members_Image_Both_Calls.html">List Image Members (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(ImageId imageId, ImageMemberFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Gets an image member, which describes access granted to a particular project ID for a specific image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="memberId">The project ID which is allowed access to the specified image. The member ID in the image service is equivalent to the <see cref="Tenant.Id">Tenant.Id</see> property described in other services.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="ImageMember"/>
        /// object describing the image member.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="memberId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_createImageMember_v2_images__image_id__members_Image_Producer_Calls.html">Create Image Member (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ImageMember> GetImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        /// <summary>
        /// Creates an image member resource, which provides a particular project ID with access to a specific image.
        /// </summary>
        /// <remarks>
        /// When the image member is created, the <see cref="ImageMember.Status"/> is initially set to
        /// <see cref="MemberStatus.Pending"/>. A call to <see cref="UpdateImageMemberAsync"/> may be
        /// used to set the status to <see cref="MemberStatus.Accepted"/> or <see cref="MemberStatus.Rejected"/>.
        /// </remarks>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="memberId">The project ID which is allowed access to the specified image. The member ID in the image service is equivalent to the <see cref="Tenant.Id">Tenant.Id</see> property described in other services.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="ImageMember"/>
        /// object describing the new image member.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="memberId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/POST_createImageMember_v2_images__image_id__members_Image_Producer_Calls.html">Create Image Member (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ImageMember> CreateImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        /// <summary>
        /// Remove an image member resource, which removes a particular project ID's access to a specific image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="memberId">The project ID which is allowed access to the specified image. The member ID in the image service is equivalent to the <see cref="Tenant.Id">Tenant.Id</see> property described in other services.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="memberId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/DELETE_deleteImageMember_v2_images__image_id__members__member_id__Image_Producer_Calls.html">Delete Image Member (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task RemoveImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        /// <summary>
        /// Set the <see cref="MemberStatus"/> associated with an <see cref="ImageMember"/>.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id"/>.</param>
        /// <param name="memberId">The project ID which is allowed access to the specified image. The member ID in the image service is equivalent to the <see cref="Tenant.Id">Tenant.Id</see> property described in other services.</param>
        /// <param name="status">The new <see cref="MemberStatus"/> for the image member.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an <see cref="ImageMember"/>
        /// object describing the updated image member.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="memberId"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="status"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/PUT_updateImageMember_v2_images__image_id__members__member_id__Image_Consumer_Calls.html">Update Image Member (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ImageMember> UpdateImageMemberAsync(ImageId imageId, ProjectId memberId, MemberStatus status, CancellationToken cancellationToken);

        #endregion Image Sharing Operations

        #region Image Tag Operations

        /// <summary>
        /// Add a tag to an image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id">Image.Id</see>.</param>
        /// <param name="tag">The tag to add to the image.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="tag"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso cref="Image.Tags"/>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/PUT_addImageTag_v2_images__image_id__tags__tag__Image_Tag_Calls.html">Add Image Tag (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task AddImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken);

        /// <summary>
        /// Remove a tag from an image.
        /// </summary>
        /// <param name="imageId">The image ID. This is obtained from <see cref="Image.Id">Image.Id</see>.</param>
        /// <param name="tag">The tag to remove from the image.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="imageId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="tag"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso cref="Image.Tags"/>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/DELETE_deleteImageTag_v2_images__image_id__tags__tag__Image_Tag_Calls.html">Delete Image Tag (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task RemoveImageTagAsync(ImageId imageId, ImageTag tag, CancellationToken cancellationToken);

        #endregion Image Tag Operations

        #region Image Task Operations

        /// <summary>
        /// Get a list of current image tasks.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an collection of <see cref="GenericImageTask"/>
        /// objects describing the image tasks.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_listTasks_v2_tasks_Image_Task_Calls.html">List Tasks (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<ReadOnlyCollectionPage<GenericImageTask>> ListTasksAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get detailed information about a specific image tasks.
        /// </summary>
        /// <typeparam name="TTask">The specific <see cref="ImageTask"/> type used to model the JSON representation of the resulting type of task.</typeparam>
        /// <param name="taskId">The image task ID. This is obtained from <see cref="ImageTask.Id">ImageTask.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return an instance of <typeparamref name="TTask"/>
        /// describing the image task.
        /// </returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/images/api/v2/ci-devguide/content/GET_listTasks_v2_tasks_Image_Task_Calls.html">List Tasks (Rackspace Cloud Images Developer Guide - API v2.2)</seealso>
        Task<TTask> GetTaskAsync<TTask>(ImageTaskId taskId, CancellationToken cancellationToken)
            where TTask : ImageTask;

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
