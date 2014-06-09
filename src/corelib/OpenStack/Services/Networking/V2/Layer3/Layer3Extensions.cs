namespace OpenStack.Services.Networking.V2.Layer3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;
    using ExtensionAlias = OpenStack.Services.Compute.V2.ExtensionAlias;
    using QuotaData = OpenStack.Services.Networking.V2.Quotas.QuotaData;

    public static class Layer3Extensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("router");

        public static Task<bool> SupportsLayer3Async(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static Task<AddRouterApiCall> PrepareAddRouterAsync(this INetworkingService client, RouterRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddRouterApiCall(factory.CreateJsonApiCall<RouterResponse>(task.Result)));
        }

        public static Task<ListRoutersApiCall> PrepareListRoutersAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Router>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            content =>
                            {
                                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(content.Result);
                                if (jsonObject == null)
                                    return null;

                                JToken routersToken = jsonObject["routers"];
                                if (routersToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                Router[] routers = routersToken.ToObject<Router[]>();
                                ReadOnlyCollectionPage<Router> result = new BasicReadOnlyCollectionPage<Router>(routers, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListRoutersApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetRouterApiCall> PrepareGetRouterAsync(this INetworkingService client, RouterId routerId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers/{router_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "router_id", routerId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetRouterApiCall(factory.CreateJsonApiCall<RouterResponse>(task.Result)));
        }

        public static Task<UpdateRouterApiCall> PrepareUpdateRouterAsync(this INetworkingService client, RouterId routerId, RouterRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers/{router_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "router_id", routerId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateRouterApiCall(factory.CreateJsonApiCall<RouterResponse>(task.Result)));
        }

        public static Task<RemoveRouterApiCall> PrepareRemoveRouterAsync(this INetworkingService client, RouterId routerId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers/{router_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "router_id", routerId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveRouterApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddRouterInterfaceApiCall> PrepareAddRouterInterfaceAsync(this INetworkingService client, RouterId routerId, AddRouterInterfaceRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers/{router_id}/add_router_interface");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "router_id", routerId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new AddRouterInterfaceApiCall(factory.CreateJsonApiCall<AddRouterInterfaceResponse>(task.Result)));
        }

        public static Task<RemoveRouterInterfaceApiCall> PrepareRemoveRouterInterfaceAsync(this INetworkingService client, RouterId routerId, RemoveRouterInterfaceRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("routers/{router_id}/remove_router_interface");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "router_id", routerId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new RemoveRouterInterfaceApiCall(factory.CreateJsonApiCall<RemoveRouterInterfaceResponse>(task.Result)));
        }

        public static Task<AddFloatingIpApiCall> PrepareAddFloatingIpAsync(this INetworkingService client, FloatingIpRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("floatingips");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddFloatingIpApiCall(factory.CreateJsonApiCall<FloatingIpResponse>(task.Result)));
        }

        public static Task<ListFloatingIpsApiCall> PrepareListFloatingIpsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("floatingips");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<FloatingIp>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            content =>
                            {
                                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(content.Result);
                                if (jsonObject == null)
                                    return null;

                                JToken floatingIpsToken = jsonObject["floatingips"];
                                if (floatingIpsToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                FloatingIp[] floatingIps = floatingIpsToken.ToObject<FloatingIp[]>();
                                ReadOnlyCollectionPage<FloatingIp> result = new BasicReadOnlyCollectionPage<FloatingIp>(floatingIps, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListFloatingIpsApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetFloatingIpApiCall> PrepareGetFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("floatingips/{floatingip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "floatingip_id", floatingIpId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetFloatingIpApiCall(factory.CreateJsonApiCall<FloatingIpResponse>(task.Result)));
        }

        public static Task<UpdateFloatingIpApiCall> PrepareUpdateFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, FloatingIpRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("floatingips/{floatingip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "floatingip_id", floatingIpId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateFloatingIpApiCall(factory.CreateJsonApiCall<FloatingIpResponse>(task.Result)));
        }

        public static Task<RemoveFloatingIpApiCall> PrepareRemoveFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("floatingips/{floatingip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "floatingip_id", floatingIpId.Value }
            };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveFloatingIpApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<Router> AddRouterAsync(this INetworkingService client, RouterData routerData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddRouterAsync(new RouterRequest(routerData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Router);
        }

        public static Task<ReadOnlyCollectionPage<Router>> ListRoutersAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListRoutersAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Router> GetRouterAsync(this INetworkingService client, RouterId routerId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetRouterAsync(routerId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Router);
        }

        public static Task<Router> UpdateRouterAsync(this INetworkingService client, RouterId routerId, RouterData routerData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareUpdateRouterAsync(routerId, new RouterRequest(routerData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Router);
        }

        public static Task RemoveRouterAsync(this INetworkingService client, RouterId routerId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveRouterAsync(routerId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<AddRouterInterfaceResponse> AddRouterInterfaceAsync(this INetworkingService client, RouterId routerId, SubnetId subnetId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddRouterInterfaceAsync(routerId, new AddRouterInterfaceRequest(subnetId), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<RemoveRouterInterfaceResponse> RemoveRouterInterfaceAsync(this INetworkingService client, RouterId routerId, SubnetId subnetId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareRemoveRouterInterfaceAsync(routerId, new RemoveRouterInterfaceRequest(subnetId), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<FloatingIp> AddFloatingIpAsync(this INetworkingService client, FloatingIpData floatingIpData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddFloatingIpAsync(new FloatingIpRequest(floatingIpData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.FloatingIp);
        }

        public static Task<ReadOnlyCollectionPage<FloatingIp>> ListFloatingIpsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListFloatingIpsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<FloatingIp> GetFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetFloatingIpAsync(floatingIpId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.FloatingIp);
        }

        public static Task<FloatingIp> UpdateFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, FloatingIpData floatingIpData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareUpdateFloatingIpAsync(floatingIpId, new FloatingIpRequest(floatingIpData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.FloatingIp);
        }

        public static Task RemoveFloatingIpAsync(this INetworkingService client, FloatingIpId floatingIpId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveFloatingIpAsync(floatingIpId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static int? GetRouterQuota(this QuotaData quotaData)
        {
            JToken value;
            if (!quotaData.ExtensionData.TryGetValue("router", out value) || value == null)
                return null;

            return value.ToObject<int?>();
        }

        public static QuotaData WithRouterQuota(this QuotaData quotaData, int routerQuota)
        {
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(quotaData.ExtensionData);
            extensionData["router"] = JToken.FromObject(routerQuota);
            return new QuotaData(quotaData.Network, quotaData.Subnet, quotaData.Port, extensionData);
        }

        public static int? GetFloatingIpQuota(this QuotaData quotaData)
        {
            JToken value;
            if (!quotaData.ExtensionData.TryGetValue("floatingip", out value) || value == null)
                return null;

            return value.ToObject<int?>();
        }

        public static QuotaData WithFloatingIpQuota(this QuotaData quotaData, int routerQuota)
        {
            Dictionary<string, JToken> extensionData = new Dictionary<string, JToken>(quotaData.ExtensionData);
            extensionData["floatingip"] = JToken.FromObject(routerQuota);
            return new QuotaData(quotaData.Network, quotaData.Subnet, quotaData.Port, extensionData);
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(INetworkingService client)
        {
            IHttpApiCallFactory factory = client as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
