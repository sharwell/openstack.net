namespace net.openstack.Core.Domain.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;

    public abstract class PortConfiguration
    {
        private NetworkId _networkId;

        private string _name;

        private bool? _adminStateUp;

        private PhysicalAddress _physicalAddress;

        private object _fixedAddresses;

        private string _deviceId;

        private string _deviceOwner;

        private ProjectId _tanantId;

        private object _securityGroups;
    }
}
