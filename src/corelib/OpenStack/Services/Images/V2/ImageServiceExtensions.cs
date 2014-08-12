namespace OpenStack.Services.Images.V2
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Schema;
    using OpenStack.Collections;
    using OpenStack.Services.Identity;
    using Rackspace.Threading;
    using Stream = System.IO.Stream;

    public static class ImageServiceExtensions
    {
        #region Images

        public static Task<Image> CreateImageAsync(this IImageService service, ImageData imageData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateImageAsync(imageData, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListImagesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Image> GetImageAsync(this IImageService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageAsync(imageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Image> UpdateImageAsync(this IImageService service, ImageId imageId, ImageData imageData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateImageAsync(imageId, new UpdateImageRequest(imageData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task RemoveImageAsync(this IImageService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageAsync(imageId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Image Sharing

        public static Task<ImageMember> CreateImageMemberAsync(this IImageService service, ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateImageMemberAsync(imageId, new CreateImageMemberRequest(memberId), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ReadOnlyCollectionPage<ImageMember>> ListImageMembersAsync(this IImageService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListImageMembersAsync(imageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ImageMember> GetImageMemberAsync(this IImageService service, ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageMemberAsync(imageId, memberId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<ImageMember> UpdateImageMemberAsync(this IImageService service, ImageId imageId, ProjectId memberId, ImageMemberStatus status, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateImageMemberAsync(imageId, memberId, new UpdateImageMemberRequest(status), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task RemoveImageMemberAsync(this IImageService service, ImageId imageId, ProjectId memberId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageMemberAsync(imageId, memberId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Image Tags

        public static Task CreateImageTagAsync(this IImageService service, ImageId imageId, ImageTag imageTag, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareCreateImageTagAsync(imageId, imageTag, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RemoveImageTagAsync(this IImageService service, ImageId imageId, ImageTag imageTag, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageTagAsync(imageId, imageTag, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Schemas

        public static Task<JsonSchema> GetImageSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<JsonSchema> GetImagesSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImagesSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<JsonSchema> GetImageMemberSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageMemberSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<JsonSchema> GetImageMembersSchemaAsync(this IImageService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageMembersSchemaAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion

        #region Image Data

        public static Task SetImageDataAsync(this IImageService service, ImageId imageId, Stream stream, CancellationToken cancellationToken, IProgress<long> progress)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareSetImageDataAsync(imageId, stream, cancellationToken, progress),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Stream> GetImageDataAsync(this IImageService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageDataAsync(imageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion
    }
}
