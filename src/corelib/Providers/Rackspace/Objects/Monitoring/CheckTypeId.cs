namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using net.openstack.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the unique identifier of a <placeholder>item placeholder</placeholder> in the <see cref="IMonitoringService"/>.
    /// </summary>
    /// <seealso cref="CheckType.Id"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonConverter(typeof(CheckTypeId.Converter))]
    public sealed class CheckTypeId : ResourceIdentifier<CheckTypeId>
    {
        private static readonly CheckTypeId _remoteDns = new CheckTypeId("remote.dns");
        private static readonly CheckTypeId _remoteFtpBanner = new CheckTypeId("remote.ftp-banner");
        private static readonly CheckTypeId _remoteHttp = new CheckTypeId("remote.http");
        private static readonly CheckTypeId _remoteImapBanner = new CheckTypeId("remote.imap-banner");
        private static readonly CheckTypeId _remoteMssqlBanner = new CheckTypeId("remote.mssql-banner");
        private static readonly CheckTypeId _remoteMysqlBanner = new CheckTypeId("remote.mysql-banner");
        private static readonly CheckTypeId _remotePing = new CheckTypeId("remote.ping");
        private static readonly CheckTypeId _remotePop3Banner = new CheckTypeId("remote.pop3-banner");
        private static readonly CheckTypeId _remotePostgresqlBanner = new CheckTypeId("remote.postgresql-banner");
        private static readonly CheckTypeId _remoteSmtpBanner = new CheckTypeId("remote.smtp-banner");
        private static readonly CheckTypeId _remoteSmtp = new CheckTypeId("remote.smtp");
        private static readonly CheckTypeId _remoteSsh = new CheckTypeId("remote.ssh");
        private static readonly CheckTypeId _remoteTcp = new CheckTypeId("remote.tcp");
        private static readonly CheckTypeId _remoteTelnetBanner = new CheckTypeId("remote.telnet-banner");

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckTypeId"/> class
        /// with the specified identifier value.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        public CheckTypeId(string id)
            : base(id)
        {
        }

        public static CheckTypeId RemoteDns
        {
            get
            {
                return _remoteDns;
            }
        }

        public static CheckTypeId RemoteFtpBanner
        {
            get
            {
                return _remoteFtpBanner;
            }
        }

        public static CheckTypeId RemoteHttp
        {
            get
            {
                return _remoteHttp;
            }
        }

        public static CheckTypeId RemoteImapBanner
        {
            get
            {
                return _remoteImapBanner;
            }
        }

        public static CheckTypeId RemoteMssqlBanner
        {
            get
            {
                return _remoteMssqlBanner;
            }
        }

        public static CheckTypeId RemoteMysqlBanner
        {
            get
            {
                return _remoteMysqlBanner;
            }
        }

        public static CheckTypeId RemotePing
        {
            get
            {
                return _remotePing;
            }
        }

        public static CheckTypeId RemotePop3Banner
        {
            get
            {
                return _remotePop3Banner;
            }
        }

        public static CheckTypeId RemotePostgresqlBanner
        {
            get
            {
                return _remotePostgresqlBanner;
            }
        }

        public static CheckTypeId RemoteSmtpBanner
        {
            get
            {
                return _remoteSmtpBanner;
            }
        }

        public static CheckTypeId RemoteSmtp
        {
            get
            {
                return _remoteSmtp;
            }
        }

        public static CheckTypeId RemoteSsh
        {
            get
            {
                return _remoteSsh;
            }
        }

        public static CheckTypeId RemoteTcp
        {
            get
            {
                return _remoteTcp;
            }
        }

        public static CheckTypeId RemoteTelnetBanner
        {
            get
            {
                return _remoteTelnetBanner;
            }
        }

        /// <summary>
        /// Provides support for serializing and deserializing <see cref="CheckTypeId"/>
        /// objects to JSON string values.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        private sealed class Converter : ConverterBase
        {
            /// <inheritdoc/>
            protected override CheckTypeId FromValue(string id)
            {
                return new CheckTypeId(id);
            }
        }
    }
}
