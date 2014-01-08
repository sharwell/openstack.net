namespace net.openstack.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHadoopAdminService
    {
        #region Limits Management

        Task ListLimitsAsync();

        Task PostLimitsAsync();

        Task ListTenantLimitsAsync();

        #endregion Limits Management

        #region Clusters Management

        Task ListClustersAsync();

        Task GetClusterAsync();

        Task RemoveClusterAsync();

        Task ListClusterLogsAsync();

        Task PostClusterActionAsync();

        Task GetClusterStatusAsync();

        Task PostClusterNodeActionAsync();

        #endregion Clusters Management

        #region Tenant Management

        Task ListTenantsAsync();

        Task GetTenantAsync();

        Task ListTenantClustersAsync();

        #endregion Tenant Management

        #region Host Management

        Task ListHostsAsync();

        #endregion Host Management

        #region Server Management

        Task ListServersAsync();

        #endregion Server Management
    }
}
