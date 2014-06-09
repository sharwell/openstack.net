﻿namespace OpenStack.Services.Networking.V2.MultiProvider
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Rackspace.Threading;
    using ExtensionAlias = OpenStack.Services.Compute.V2.ExtensionAlias;

    public static class MultiProviderExtensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("multi-provider");

        public static Task<bool> SupportsNetworkMultipleProvidersAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static ReadOnlyCollection<Segment> GetProviders(this NetworkData network)
        {
            if (network == null)
                throw new ArgumentNullException("network");

            JToken segmentsToken;
            if (!network.ExtensionData.TryGetValue("segments", out segmentsToken))
                return null;

            return segmentsToken.ToObject<ReadOnlyCollection<Segment>>();
        }

        public static NetworkData WithProviders(this NetworkData network, IEnumerable<Segment> segments)
        {
            if (network == null)
                throw new ArgumentNullException("network");
            if (segments == null)
                throw new ArgumentNullException("segments");

            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(network.ExtensionData);
            extensionData["segments"] = JToken.FromObject(segments);
            return new NetworkData(network.Name, network.ProjectId, network.Shared, network.AdminStateUp, extensionData);
        }
    }
}
