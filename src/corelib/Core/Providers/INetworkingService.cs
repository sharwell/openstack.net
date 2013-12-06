namespace net.openstack.Core.Providers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using net.openstack.Core.Domain.Networking;
    using CancellationToken = System.Threading.CancellationToken;

    public interface INetworkingService
    {
        #region Networks

        Task<ReadOnlyCollection<Network>> ListNetworksAsync(NetworkId marker, int? limit, CancellationToken cancellationToken);

        Task<Network> GetNetworkAsync(NetworkId subnetId, CancellationToken cancellationToken);

        Task<Network> CreateNetworkAsync(NewNetworkConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollection<Network>> CreateNetworkRangeAsync(IEnumerable<NewNetworkConfiguration> configuration, CancellationToken cancellationToken);

        Task UpdateNetworkAsync(NetworkId subnetId, UpdateNetworkConfiguration configuration, CancellationToken cancellationToken);

        Task DeleteNetworkAsync(NetworkId subnetId, CancellationToken cancellationToken);

        #endregion Networks

        #region Subnets

        Task<ReadOnlyCollection<Subnet>> ListSubnetsAsync(CancellationToken cancellationToken);

        Task<Subnet> GetSubnetAsync(SubnetId subnetId, CancellationToken cancellationToken);

        Task<Subnet> CreateSubnetAsync(NewSubnetConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollection<Subnet>> CreateSubnetRangeAsync(IEnumerable<NewSubnetConfiguration> configuration, CancellationToken cancellationToken);

        Task UpdateSubnetAsync(SubnetId subnetId, UpdateSubnetConfiguration configuration, CancellationToken cancellationToken);

        Task DeleteSubnetAsync(SubnetId subnetId, CancellationToken cancellationToken);

        #endregion Subnets

        #region Ports

        Task<ReadOnlyCollection<Port>> ListPortsAsync(CancellationToken cancellationToken);

        Task<Port> GetPortAsync(PortId subnetId, CancellationToken cancellationToken);

        Task<Port> CreatePortAsync(NewPortConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollection<Port>> CreatePortRangeAsync(IEnumerable<NewPortConfiguration> configuration, CancellationToken cancellationToken);

        Task UpdatePortAsync(PortId subnetId, UpdatePortConfiguration configuration, CancellationToken cancellationToken);

        Task DeletePortAsync(PortId subnetId, CancellationToken cancellationToken);

        #endregion Ports

        #region Extensions

        Task<ReadOnlyCollection<NetworkingExtension>> ListExtensionsAsync(CancellationToken cancellationToken);

        #endregion Extensions
    }
}
