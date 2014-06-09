namespace OpenStack.Services.Compute.V2
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IComputeService : IHttpService
    {
        #region Servers

        Task<ListServersApiCall> PrepareListServersAsync(CancellationToken cancellationToken);

        Task<CreateServerApiCall> PrepareCreateServerAsync(ServerRequest request, CancellationToken cancellationToken);

        Task<GetServerApiCall> PrepareGetServerAsync(ServerId serverId, CancellationToken cancellationToken);

        Task<UpdateServerApiCall> PrepareUpdateServerAsync(ServerRequest request, CancellationToken cancellationToken);

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
    }
}
