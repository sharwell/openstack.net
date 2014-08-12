namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

#if !NET40PLUS
    using Rackspace.Threading;
#endif

    public interface IComputeService : IHttpService
    {
        #region Servers

        Task<ListServersApiCall> PrepareListServersAsync(CancellationToken cancellationToken);

        Task<CreateServerApiCall> PrepareCreateServerAsync(ServerRequest request, CancellationToken cancellationToken);

        Task<GetServerApiCall> PrepareGetServerAsync(ServerId serverId, CancellationToken cancellationToken);

        Task<UpdateServerApiCall> PrepareUpdateServerAsync(ServerId serverId, ServerRequest request, CancellationToken cancellationToken);

        Task<RemoveServerApiCall> PrepareRemoveServerAsync(ServerId serverId, CancellationToken cancellationToken);

        #endregion

        #region Server Addresses

        Task<GetServerAddressesApiCall> PrepareGetServerAddressesAsync(ServerId serverId, CancellationToken cancellationToken);

        #endregion

        #region Server Actions

        Task<ChangePasswordApiCall> PrepareChangePasswordAsync(ServerId serverId, ChangePasswordRequest request, CancellationToken cancellationToken);

        Task<RebootServerApiCall> PrepareRebootServerAsync(ServerId serverId, RebootRequest request, CancellationToken cancellationToken);

        Task<RebuildServerApiCall> PrepareRebuildServerAsync(ServerId serverId, RebuildRequest request, CancellationToken cancellationToken);

        Task<ResizeServerApiCall> PrepareResizeServerAsync(ServerId serverId, ResizeRequest request, CancellationToken cancellationToken);

        Task<ConfirmServerResizeApiCall> PrepareConfirmServerResizeAsync(ServerId serverId, ConfirmServerResizeRequest request, CancellationToken cancellationToken);

        Task<RevertServerResizeApiCall> PrepareRevertServerResizeAsync(ServerId serverId, RevertServerResizeRequest request, CancellationToken cancellationToken);

        Task<CreateImageApiCall> PrepareCreateImageAsync(ServerId serverId, CreateImageRequest request, CancellationToken cancellationToken);

        #endregion

        #region Flavors

        Task<ListFlavorsApiCall> PrepareListFlavorsAsync(CancellationToken cancellationToken);

        Task<GetFlavorApiCall> PrepareGetFlavorAsync(FlavorId flavorId, CancellationToken cancellationToken);

        #endregion

        #region Images

        Task<ListImagesApiCall> PrepareListImagesAsync(CancellationToken cancellationToken);

        Task<GetImageApiCall> PrepareGetImageAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<RemoveImageApiCall> PrepareRemoveImageAsync(ImageId imageId, CancellationToken cancellationToken);

        #endregion

        #region Metadata

        Task<GetServerMetadataApiCall> PrepareGetServerMetadataAsync(ServerId serverId, CancellationToken cancellationToken);

        Task<SetServerMetadataApiCall> PrepareSetServerMetadataAsync(ServerId serverId, MetadataRequest request, CancellationToken cancellationToken);

        Task<GetServerMetadataItemApiCall> PrepareGetServerMetadataItemAsync(ServerId serverId, string key, CancellationToken cancellationToken);

        Task<SetServerMetadataItemApiCall> PrepareSetServerMetadataItemAsync(ServerId serverId, string key, MetadataRequest request, CancellationToken cancellationToken);

        Task<RemoveServerMetadataItemApiCall> PrepareRemoveServerMetadataItemAsync(ServerId serverId, string key, CancellationToken cancellationToken);

        Task<GetImageMetadataApiCall> PrepareGetImageMetadataAsync(ImageId imageId, CancellationToken cancellationToken);

        Task<SetImageMetadataApiCall> PrepareSetImageMetadataAsync(ImageId imageId, MetadataRequest request, CancellationToken cancellationToken);

        Task<GetImageMetadataItemApiCall> PrepareGetImageMetadataItemAsync(ImageId imageId, string key, CancellationToken cancellationToken);

        Task<SetImageMetadataItemApiCall> PrepareSetImageMetadataItemAsync(ImageId imageId, string key, MetadataRequest request, CancellationToken cancellationToken);

        Task<RemoveImageMetadataItemApiCall> PrepareRemoveImageMetadataItemAsync(ImageId imageId, string key, CancellationToken cancellationToken);

        #endregion

        #region Extensions

        Task<ListExtensionsApiCall> PrepareListExtensionsAsync(CancellationToken cancellationToken);

        Task<GetExtensionApiCall> PrepareGetExtensionAsync(ExtensionAlias alias, CancellationToken cancellationToken);

        #endregion

        #region Polling Operations

        Task<Server> WaitForServerStatusAsync(ServerId serverId, IEnumerable<ServerStatus> exitStatus, IProgress<Server> progress);

        Task<Image> WaitForImageStatusAsync(ImageId imageId, IEnumerable<ImageStatus> exitStatus, IProgress<Image> progress);

        #endregion
    }
}
