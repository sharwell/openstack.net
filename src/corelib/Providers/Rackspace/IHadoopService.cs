namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Providers.Rackspace.Objects.Hadoop;
    using FlavorId = net.openstack.Providers.Rackspace.Objects.Databases.FlavorId;

    public interface IHadoopService
    {
        #region Profiles

        Task GetProfileAsync();

        Task UpdateProfileAsync();

        #endregion Profiles

        #region Clusters

        Task CreateClusterAsync();

        Task ListClustersAsync();

        Task GetClusterAsync();

        Task RemoveClusterAsync();

        Task ResizeClusterAsync(ClusterId clusterId, ResizeClusterConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Cluster> progress);

        #endregion Clusters

        #region Nodes

        Task<ReadOnlyCollection<HadoopNode>> ListNodesAsync(ClusterId clusterId, CancellationToken cancellationToken);

        Task<HadoopNode> GetNodeAsync(ClusterId clusterId, HadoopNodeId nodeId, CancellationToken cancellationToken);

        #endregion Nodes

        #region Flavors

        Task<ReadOnlyCollection<HadoopFlavor>> ListFlavorsAsync(CancellationToken cancellationToken);

        Task<HadoopFlavor> GetFlavorAsync(FlavorId flavorId, CancellationToken cancellationToken);

        Task<ClusterType> GetSupportedTypesAsync(FlavorId flavorId, CancellationToken cancellationToken);

        #endregion Flavors

        #region Types

        Task<ReadOnlyCollection<ClusterType>> ListClusterTypesAsync(CancellationToken cancellationToken);

        Task<ClusterType> GetClusterTypeAsync(ClusterTypeId clusterTypeId, CancellationToken cancellationToken);

        Task<ReadOnlyCollection<HadoopFlavor>> GetSupportedFlavorsAsync(ClusterTypeId clusterTypeId, CancellationToken cancellationToken);

        #endregion Types

        #region Resource Limits

        Task GetResourceLimitsAsync();

        #endregion Resource Limits
    }
}
