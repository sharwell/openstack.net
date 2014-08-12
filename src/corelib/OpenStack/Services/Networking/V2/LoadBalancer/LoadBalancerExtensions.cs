namespace OpenStack.Services.Networking.V2.LoadBalancer
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

    public static class LoadBalancerExtensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("lbaas");

        public static Task<bool> SupportsLoadBalancerAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            return service.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static Task<AddVirtualAddressApiCall> PrepareAddVirtualAddressAsync(this INetworkingService service, VirtualAddressRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/vips");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddVirtualAddressApiCall(factory.CreateJsonApiCall<VirtualAddressResponse>(task.Result)));
        }

        public static Task<ListVirtualAddressesApiCall> PrepareListVirtualAddressesAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/vips");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<VirtualAddress>>> deserializeResult =
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

                                JToken virtualAddressesToken = jsonObject["vips"];
                                if (virtualAddressesToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                VirtualAddress[] virtualAddresses = virtualAddressesToken.ToObject<VirtualAddress[]>();
                                ReadOnlyCollectionPage<VirtualAddress> result = new BasicReadOnlyCollectionPage<VirtualAddress>(virtualAddresses, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListVirtualAddressesApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetVirtualAddressApiCall> PrepareGetVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/vips/{vip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "vip_id", virtualAddressId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetVirtualAddressApiCall(factory.CreateJsonApiCall<VirtualAddressResponse>(task.Result)));
        }

        public static Task<UpdateVirtualAddressApiCall> PrepareUpdateVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, VirtualAddressRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/vips/{vip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "vip_id", virtualAddressId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateVirtualAddressApiCall(factory.CreateJsonApiCall<VirtualAddressResponse>(task.Result)));
        }

        public static Task<RemoveVirtualAddressApiCall> PrepareRemoveVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/vips/{vip_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "vip_id", virtualAddressId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveVirtualAddressApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddHealthMonitorApiCall> PrepareAddHealthMonitorAsync(this INetworkingService service, HealthMonitorRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/healthmonitors");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddHealthMonitorApiCall(factory.CreateJsonApiCall<HealthMonitorResponse>(task.Result)));
        }

        public static Task<ListHealthMonitorsApiCall> PrepareListHealthMonitorsAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/healthmonitors");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<HealthMonitor>>> deserializeResult =
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

                                JToken healthMonitorsToken = jsonObject["health_monitors"];
                                if (healthMonitorsToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                HealthMonitor[] healthMonitors = healthMonitorsToken.ToObject<HealthMonitor[]>();
                                ReadOnlyCollectionPage<HealthMonitor> result = new BasicReadOnlyCollectionPage<HealthMonitor>(healthMonitors, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListHealthMonitorsApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetHealthMonitorApiCall> PrepareGetHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/healthmonitors/{health_monitor_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "health_monitor_id", healthMonitorId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetHealthMonitorApiCall(factory.CreateJsonApiCall<HealthMonitorResponse>(task.Result)));
        }

        public static Task<UpdateHealthMonitorApiCall> PrepareUpdateHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, HealthMonitorRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/healthmonitors/{health_monitor_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "health_monitor_id", healthMonitorId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateHealthMonitorApiCall(factory.CreateJsonApiCall<HealthMonitorResponse>(task.Result)));
        }

        public static Task<RemoveHealthMonitorApiCall> PrepareRemoveHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/healthmonitors/{health_monitor_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "health_monitor_id", healthMonitorId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveHealthMonitorApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddPoolApiCall> PrepareAddPoolAsync(this INetworkingService service, PoolRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddPoolApiCall(factory.CreateJsonApiCall<PoolResponse>(task.Result)));
        }

        public static Task<ListPoolsApiCall> PrepareListPoolsAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Pool>>> deserializeResult =
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

                                JToken poolsToken = jsonObject["pools"];
                                if (poolsToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                Pool[] pools = poolsToken.ToObject<Pool[]>();
                                ReadOnlyCollectionPage<Pool> result = new BasicReadOnlyCollectionPage<Pool>(pools, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListPoolsApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetPoolApiCall> PrepareGetPoolAsync(this INetworkingService service, PoolId poolId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools/{pool_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "pool_id", poolId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetPoolApiCall(factory.CreateJsonApiCall<PoolResponse>(task.Result)));
        }

        public static Task<UpdatePoolApiCall> PrepareUpdatePoolAsync(this INetworkingService service, PoolId poolId, PoolRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools/{pool_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "pool_id", poolId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdatePoolApiCall(factory.CreateJsonApiCall<PoolResponse>(task.Result)));
        }

        public static Task<RemovePoolApiCall> PrepareRemovePoolAsync(this INetworkingService service, PoolId poolId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools/{pool_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "pool_id", poolId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemovePoolApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddPoolHealthMonitorApiCall> PrepareAddPoolHealthMonitorAsync(this INetworkingService service, PoolId poolId, PoolHealthMonitorRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools/{pool_id}/health_monitors");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "pool_id", poolId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddPoolHealthMonitorApiCall(factory.CreateJsonApiCall<HealthMonitorResponse>(task.Result)));
        }

        public static Task<RemovePoolHealthMonitorApiCall> PrepareRemovePoolHealthMonitorAsync(this INetworkingService service, PoolId poolId, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/pools/{pool_id}/health_monitors/{health_monitor_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "pool_id", poolId.Value }, { "health_monitor_id", healthMonitorId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemovePoolHealthMonitorApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddMemberApiCall> PrepareAddMemberAsync(this INetworkingService service, MemberRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/members");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddMemberApiCall(factory.CreateJsonApiCall<MemberResponse>(task.Result)));
        }

        public static Task<ListMembersApiCall> PrepareListMembersAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/members");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Member>>> deserializeResult =
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

                                JToken membersToken = jsonObject["members"];
                                if (membersToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                Member[] members = membersToken.ToObject<Member[]>();
                                ReadOnlyCollectionPage<Member> result = new BasicReadOnlyCollectionPage<Member>(members, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListMembersApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetMemberApiCall> PrepareGetMemberAsync(this INetworkingService service, MemberId memberId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/members/{member_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "member_id", memberId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetMemberApiCall(factory.CreateJsonApiCall<MemberResponse>(task.Result)));
        }

        public static Task<UpdateMemberApiCall> PrepareUpdateMemberAsync(this INetworkingService service, MemberId memberId, MemberRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/members/{member_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "member_id", memberId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateMemberApiCall(factory.CreateJsonApiCall<MemberResponse>(task.Result)));
        }

        public static Task<RemoveMemberApiCall> PrepareRemoveMemberAsync(this INetworkingService service, MemberId memberId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("lb/members/{member_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "member_id", memberId.Value } };

            IHttpApiCallFactory factory = service.GetHttpApiCallFactory();
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveMemberApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<VirtualAddress> AddVirtualAddressAsync(this INetworkingService service, VirtualAddressData virtualAddressData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareAddVirtualAddressAsync(new VirtualAddressRequest(virtualAddressData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.VirtualAddress);
        }

        public static Task<ReadOnlyCollectionPage<VirtualAddress>> ListVirtualAddressesAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListVirtualAddressesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<VirtualAddress> GetVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetVirtualAddressAsync(virtualAddressId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.VirtualAddress);
        }

        public static Task<VirtualAddress> UpdateVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, VirtualAddressData virtualAddressData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateVirtualAddressAsync(virtualAddressId, new VirtualAddressRequest(virtualAddressData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.VirtualAddress);
        }

        public static Task RemoveVirtualAddressAsync(this INetworkingService service, VirtualAddressId virtualAddressId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveVirtualAddressAsync(virtualAddressId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<HealthMonitor> AddHealthMonitorAsync(this INetworkingService service, HealthMonitorData healthMonitorData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareAddHealthMonitorAsync(new HealthMonitorRequest(healthMonitorData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.HealthMonitor);
        }

        public static Task<ReadOnlyCollectionPage<HealthMonitor>> ListHealthMonitorsAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListHealthMonitorsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<HealthMonitor> GetHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetHealthMonitorAsync(healthMonitorId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.HealthMonitor);
        }

        public static Task<HealthMonitor> UpdateHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, HealthMonitorData healthMonitorData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateHealthMonitorAsync(healthMonitorId, new HealthMonitorRequest(healthMonitorData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.HealthMonitor);
        }

        public static Task RemoveHealthMonitorAsync(this INetworkingService service, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveHealthMonitorAsync(healthMonitorId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Pool> AddPoolAsync(this INetworkingService service, PoolData poolData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareAddPoolAsync(new PoolRequest(poolData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Pool);
        }

        public static Task<ReadOnlyCollectionPage<Pool>> ListPoolsAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListPoolsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Pool> GetPoolAsync(this INetworkingService service, PoolId poolId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetPoolAsync(poolId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Pool);
        }

        public static Task<Pool> UpdatePoolAsync(this INetworkingService service, PoolId poolId, PoolData poolData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdatePoolAsync(poolId, new PoolRequest(poolData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Pool);
        }

        public static Task RemovePoolAsync(this INetworkingService service, PoolId poolId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemovePoolAsync(poolId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<HealthMonitor> AddPoolHealthMonitorAsync(this INetworkingService service, PoolId poolId, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareAddPoolHealthMonitorAsync(poolId, new PoolHealthMonitorRequest(new PoolHealthMonitorRequest.HealthMonitorData(healthMonitorId)), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.HealthMonitor);
        }

        public static Task RemovePoolHealthMonitorAsync(this INetworkingService service, PoolId poolId, HealthMonitorId healthMonitorId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemovePoolHealthMonitorAsync(poolId, healthMonitorId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Member> AddMemberAsync(this INetworkingService service, MemberData memberData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareAddMemberAsync(new MemberRequest(memberData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Member);
        }

        public static Task<ReadOnlyCollectionPage<Member>> ListMembersAsync(this INetworkingService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListMembersAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Member> GetMemberAsync(this INetworkingService service, MemberId memberId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetMemberAsync(memberId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Member);
        }

        public static Task<Member> UpdateMemberAsync(this INetworkingService service, MemberId memberId, MemberData memberData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateMemberAsync(memberId, new MemberRequest(memberData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Member);
        }

        public static Task RemoveMemberAsync(this INetworkingService service, MemberId memberId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveMemberAsync(memberId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }
    }
}
