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

        Task<GetServerAddressesApiCall> PrepareGetServerAddresses(ServerId serverId, CancellationToken cancellationToken);

        #endregion

        #region Server Actions

        Task<ChangePasswordApiCall> PrepareChangePasswordAsync();

        Task<RebootServerApiCall> PrepareRebootServerAsync();

        Task<RebuildServerApiCall> PrepareRebuildServerAsync();

        Task<ResizeServerApiCall> PrepareResizeServerAsync();

        Task<ConfirmServerResizedApiCall> PrepareConfirmServerResizedAsync();

        Task<RevertResizedServerApiCall> PrepareRevertResizedServerAsync();

        Task<CreateImageApiCall> PrepareCreateImageAsync();

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
