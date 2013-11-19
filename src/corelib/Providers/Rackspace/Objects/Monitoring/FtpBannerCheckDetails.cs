namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class FtpBannerCheckDetails : ConnectionCheckDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpBannerCheckDetails"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected FtpBannerCheckDetails()
        {
        }

        public FtpBannerCheckDetails(int? port)
            : base(port)
        {
        }

        protected internal override bool SupportsCheckType(CheckTypeId checkTypeId)
        {
            return checkTypeId == CheckTypeId.RemoteFtpBanner;
        }
    }
}
