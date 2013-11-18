namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class LoginInformation
    {
        [JsonProperty("user")]
        private string _user;

        [JsonProperty("device")]
        private string _device;

        [JsonProperty("time")]
        private long? _time;

        [JsonProperty("host")]
        private string _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInformation"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LoginInformation()
        {
        }

        /// <summary>
        /// Gets the username of the logged in user.
        /// </summary>
        public string User
        {
            get
            {
                return _user;
            }
        }

        /// <summary>
        /// Gets the device the user is attached to.
        /// </summary>
        public string Device
        {
            get
            {
                return _device;
            }
        }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        public DateTimeOffset? Time
        {
            get
            {
                if (_time == null)
                    return null;

                return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero).AddMilliseconds(_time.Value);
            }
        }

        /// <summary>
        /// Gets the originating host of the login.
        /// </summary>
        public string Host
        {
            get
            {
                return _host;
            }
        }
    }
}
