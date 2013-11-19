namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class SshCheckDetails : ConnectionCheckDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SshCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected SshCheckDetails()
        {
        }

        public SshCheckDetails(int? port = null)
            : base(port)
        {
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteSsh;
        }
    }
}
