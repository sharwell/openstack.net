namespace OpenStack.Services.Networking.V2.ExtendedAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Rackspace.Threading;

    public static class ProviderExtensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("provider");

        public static Task<bool> SupportsNetworkProviderAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static string GetProviderPhysicalNetwork(this NetworkData network)
        {
            if (network == null)
                throw new ArgumentNullException("network");

            JToken value;
            if (!network.ExtensionData.TryGetValue("provider:physical_network", out value))
                return null;

            return value.ToObject<string>();
        }

        public static NetworkType GetProviderNetworkType(this NetworkData network)
        {
            if (network == null)
                throw new ArgumentNullException("network");

            JToken value;
            if (!network.ExtensionData.TryGetValue("provider:network_type", out value))
                return null;

            return value.ToObject<NetworkType>();
        }

        public static SegmentationId GetProviderSegmentationId(this NetworkData network)
        {
            if (network == null)
                throw new ArgumentNullException("network");

            JToken value;
            if (!network.ExtensionData.TryGetValue("provider:segmentation_id", out value))
                return null;

            return value.ToObject<SegmentationId>();
        }

        public static NetworkData WithProviderPhysicalNetwork(this NetworkData network, string physicalNetwork)
        {
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(network.ExtensionData);
            extensionData["provider:physical_network"] = JValue.CreateString(physicalNetwork);
            return new NetworkData(network.Name, network.ProjectId, network.Shared, network.AdminStateUp, extensionData);
        }

        public static NetworkData WithProviderNetworkType(this NetworkData network, NetworkType networkType)
        {
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(network.ExtensionData);
            extensionData["provider:network_type"] = JToken.FromObject(networkType);
            return new NetworkData(network.Name, network.ProjectId, network.Shared, network.AdminStateUp, extensionData);
        }

        public static NetworkData WithProviderSegmentationId(this NetworkData network, SegmentationId segmentationId)
        {
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(network.ExtensionData);
            extensionData["provider:segmentation_id"] = JToken.FromObject(segmentationId);
            return new NetworkData(network.Name, network.ProjectId, network.Shared, network.AdminStateUp, extensionData);
        }
    }
}
