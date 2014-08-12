namespace OpenStack.Services.Images.V2
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Services.Identity;
    using Stream = System.IO.Stream;

    public interface IImageService : IHttpService
    {
        #region Images

        Task<CreateImageApiCall> PrepareCreateImageAsync(ImageData imageData, CancellationToken cancellationToken);

        Task<ListImagesApiCall> PrepareListImagesAsync(CancellationToken cancellationToken);

        Task<GetImageApiCall> PrepareGetImageAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<UpdateImageApiCall> PrepareUpdateImageAsync(ImageId imageId, UpdateImageRequest request, CancellationToken cancellationToken);

        Task<RemoveImageApiCall> PrepareRemoveImageAsync(ImageId imageId, CancellationToken cancellationToken);

        #endregion

        #region Image Sharing

        Task<CreateImageMemberApiCall> PrepareCreateImageMemberAsync(ImageId imageId, CreateImageMemberRequest request, CancellationToken cancellationToken);

        Task<ListImageMembersApiCall> PrepareListImageMembersAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<GetImageMemberApiCall> PrepareGetImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        Task<UpdateImageMemberApiCall> PrepareUpdateImageMemberAsync(ImageId imageId, ProjectId memberId, UpdateImageMemberRequest request, CancellationToken cancellationToken);

        Task<RemoveImageMemberApiCall> PrepareRemoveImageMemberAsync(ImageId imageId, ProjectId memberId, CancellationToken cancellationToken);

        #endregion

        #region Image Tags

        Task<CreateImageTagApiCall> PrepareCreateImageTagAsync(ImageId imageId, ImageTag imageTag, CancellationToken cancellationToken);

        Task<RemoveImageTagApiCall> PrepareRemoveImageTagAsync(ImageId imageId, ImageTag imageTag, CancellationToken cancellationToken);

        #endregion

        #region Schemas

        Task<GetImageSchemaApiCall> PrepareGetImageSchemaAsync(CancellationToken cancellationToken);

        Task<GetImagesSchemaApiCall> PrepareGetImagesSchemaAsync(CancellationToken cancellationToken);

        Task<GetImageMemberSchemaApiCall> PrepareGetImageMemberSchemaAsync(CancellationToken cancellationToken);

        Task<GetImageMembersSchemaApiCall> PrepareGetImageMembersSchemaAsync(CancellationToken cancellationToken);

        #endregion

        #region Image Data

        Task<SetImageDataApiCall> PrepareSetImageDataAsync(ImageId imageId, Stream stream, CancellationToken cancellationToken, IProgress<long> progress);

        Task<GetImageDataApiCall> PrepareGetImageDataAsync(ImageId imageId, CancellationToken cancellationToken);

        #endregion
    }
}
