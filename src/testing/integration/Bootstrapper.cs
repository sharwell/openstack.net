using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using JSIStudios.SimpleRESTServices.Client;
using JSIStudios.SimpleRESTServices.Client.Json;
using net.openstack.Core.Domain;
using net.openstack.Core.Providers;
using net.openstack.Providers.Rackspace;
using net.openstack.Providers.Rackspace.Objects;

namespace Net.OpenStack.Testing.Integration
{
    public class Bootstrapper
    {
        private static OpenstackNetSetings _settings;
        public static OpenstackNetSetings Settings
        {
            get
            {
                if(_settings == null)
                    Initialize();

                return _settings;
            }
        }

        public static void Initialize()
        {
            var homeDir = Environment.ExpandEnvironmentVariables("C:\\");

            var path = Path.Combine(homeDir, ".openstack_net");

            var contents = new StringBuilder();

            using(var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using(var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if(!line.Trim().StartsWith("//"))
                            contents.Append(line);
                    }
                }
            }

            var appCredentials = Newtonsoft.Json.JsonConvert.DeserializeObject<OpenstackNetSetings>(contents.ToString());

            _settings = appCredentials;
        }

        public static IIdentityProvider CreateIdentityProvider()
        {
            return CreateIdentityProvider(Bootstrapper.Settings.TestIdentity);
        }

        public static IIdentityProvider CreateIdentityProvider(CloudIdentity identity)
        {
            return new CloudIdentityProvider(identity);
        }

        public static IComputeProvider CreateComputeProvider()
        {
            return new CloudServersProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null, null);
        }

        public static INetworksProvider CreateNetworksProvider()
        {
            return new CloudNetworksProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null, null);
        }

        public static IBlockStorageProvider CreateBlockStorageProvider()
        {
            return new CloudBlockStorageProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null, null);
        }

        public static IObjectStorageProvider CreateObjectStorageProvider()
        {
            IRestService restService = new ExtendedJsonRestServices();
            return new ExtendedCloudFilesProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null, restService);
        }
    }

    public class OpenstackNetSetings
    {
        public ExtendedCloudIdentity TestIdentity { get; set; }

        public ExtendedCloudIdentity TestAdminIdentity { get; set; }

        public ExtendedRackspaceCloudIdentity TestDomainIdentity { get; set; }

        public string RackspaceExtendedIdentityUrl { get; set; }

        public string DefaultRegion
        {
            get;
            set;
        }
    }

    public class ExtendedCloudIdentity : CloudIdentity
    {
        public string TenantId { get; set; }

        public string Domain { get; set; }
    }

    public class ExtendedRackspaceCloudIdentity : RackspaceCloudIdentity
    {
        public string TenantId { get; set; }

        public ExtendedRackspaceCloudIdentity()
        {
            
        }

        public ExtendedRackspaceCloudIdentity(ExtendedCloudIdentity cloudIdentity)
        {
            this.APIKey = cloudIdentity.APIKey;
            this.Password = cloudIdentity.Password;
            this.Username = cloudIdentity.Username;
            this.TenantId = cloudIdentity.TenantId;
            this.Domain = string.IsNullOrEmpty(cloudIdentity.Domain) ? null : new Domain(cloudIdentity.Domain);
        }
    }

    public class ExtendedJsonRestServices : JsonRestServices
    {
        public override Response Stream(Uri url, HttpMethod method, Func<HttpWebResponse, bool, Response> responseBuilderCallback, Stream content, int bufferSize, long maxReadLength, Dictionary<string, string> headers, Dictionary<string, string> queryStringParameters, RequestSettings settings, Action<long> progressUpdated)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (content == null)
                throw new ArgumentNullException("content");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize");
            if (maxReadLength < 0)
                throw new ArgumentOutOfRangeException("maxReadLength");

            return ExecuteRequest(url, method, responseBuilderCallback, headers, queryStringParameters, settings, (req) =>
            {
                long bytesWritten = 0;

                if (settings.ChunkRequest || maxReadLength > 0)
                {
                    req.SendChunked = settings.ChunkRequest;
                    req.AllowWriteStreamBuffering = false;
                    req.ContentLength = maxReadLength > 0 && content.Length > maxReadLength ? maxReadLength : content.Length;
                }

                using (Stream stream = req.GetRequestStream())
                {
                    var buffer = new byte[bufferSize];
                    int count;
                    while (!req.HaveResponse && (count = content.Read(buffer, 0, maxReadLength > 0 ? (int)Math.Min(bufferSize, maxReadLength - bytesWritten) : bufferSize)) > 0)
                    {
                        bytesWritten += count;
                        stream.Write(buffer, 0, count);

                        if (progressUpdated != null)
                            progressUpdated(bytesWritten);

                        if (maxReadLength > 0 && bytesWritten >= maxReadLength)
                            break;
                    }
                }

                return "[STREAM CONTENT]";
            });
        }
    }

    public class ExtendedCloudFilesProvider : CloudFilesProvider
    {
        public ExtendedCloudFilesProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider, IRestService restService)
            : base(defaultIdentity, defaultRegion, identityProvider, restService)
        {
        }

        protected override RequestSettings BuildDefaultRequestSettings(IEnumerable<HttpStatusCode> non200SuccessCodes = null)
        {
            RequestSettings result = base.BuildDefaultRequestSettings(non200SuccessCodes);
            // Do not implicitly retry the operation if a failure occurs.
            result.RetryCount = 0;
            return result;
        }
    }
}
