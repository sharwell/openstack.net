namespace net.openstack.Providers.Rackspace.Objects.Hadoop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain;
    using Newtonsoft.Json.Linq;

    public class HadoopNode
    {
        private HadoopNodeId _id;

        private DateTimeOffset? _created;

        private string _role;

        private string _name;

        private string _postInitScriptStatus;

        private string _status;

        private ReadOnlyDictionary<string, ReadOnlyCollection<IPAddress>> _addresses;

        private JObject[] _services;

        private Link[] _links;
    }
}
