namespace OpenStack.Services.Identity.V2
{
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;

    public interface IIdentityService : IHttpService
    {
        #region Client API Operations

        Task<ReadOnlyCollectionPage<ExtensionData>> ListExtensionsAsync(CancellationToken cancellationToken);

        Task<UserAccess> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken);

        #endregion

#if false
        #region Administrative API Operations

        #region Token Operations

        Task<UserAccess> ValidateTokenAsync(TokenId tokenId, ProjectId belongsTo, CancellationToken cancellationToken);

        Task CheckTokenAsync(TokenId tokenId, ProjectId belongsTo, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<AdminEndpoint>> ListEndpointsForTokenAsync(TokenId tokenId, CancellationToken cancellationToken);

        #endregion

        #region User Operations

        #endregion

        #region Tenant Operations

        #endregion

        #endregion
#endif
    }
}
