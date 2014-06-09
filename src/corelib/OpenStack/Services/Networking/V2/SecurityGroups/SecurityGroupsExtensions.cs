namespace OpenStack.Services.Networking.V2.SecurityGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using Rackspace.Net;
    using Rackspace.Threading;
    using ExtensionAlias = OpenStack.Services.Compute.V2.ExtensionAlias;

    public static class SecurityGroupsExtensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("security-groups");

        public static Task<bool> SupportsSecurityGroupsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static Task<AddSecurityGroupApiCall> PrepareAddSecurityGroupAsync(this INetworkingService client, SecurityGroupRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-groups");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddSecurityGroupApiCall(factory.CreateJsonApiCall<SecurityGroupResponse>(task.Result)));
        }

        public static Task<ListSecurityGroupsApiCall> PrepareListSecurityGroupsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-groups");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<SecurityGroup>>> deserializeResult =
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

                                JToken securityGroupsToken = jsonObject["security_groups"];
                                if (securityGroupsToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                SecurityGroup[] securityGroups = securityGroupsToken.ToObject<SecurityGroup[]>();
                                ReadOnlyCollectionPage<SecurityGroup> result = new BasicReadOnlyCollectionPage<SecurityGroup>(securityGroups, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListSecurityGroupsApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetSecurityGroupApiCall> PrepareGetSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-groups/{security_group_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_id", securityGroupId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetSecurityGroupApiCall(factory.CreateJsonApiCall<SecurityGroupResponse>(task.Result)));
        }

        //public static Task<UpdateSecurityGroupApiCall> PrepareUpdateSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, SecurityGroupRequest request, CancellationToken cancellationToken)
        //{
        //    UriTemplate template = new UriTemplate("security-groups/{security_group_id}");
        //    Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_id", securityGroupId.Value } };

        //    IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
        //    return client.GetBaseUriAsync(cancellationToken)
        //        .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
        //        .Select(task => new UpdateSecurityGroupApiCall(factory.CreateJsonApiCall<SecurityGroupResponse>(task.Result)));
        //}

        public static Task<RemoveSecurityGroupApiCall> PrepareRemoveSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-groups/{security_group_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_id", securityGroupId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveSecurityGroupApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddSecurityGroupRuleApiCall> PrepareAddSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-group-rules");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddSecurityGroupRuleApiCall(factory.CreateJsonApiCall<SecurityGroupRuleResponse>(task.Result)));
        }

        public static Task<ListSecurityGroupRulesApiCall> PrepareListSecurityGroupRulesAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-group-rules");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<SecurityGroupRule>>> deserializeResult =
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

                                JToken securityGroupRulesToken = jsonObject["security_group_rules"];
                                if (securityGroupRulesToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                SecurityGroupRule[] securityGroupRules = securityGroupRulesToken.ToObject<SecurityGroupRule[]>();
                                ReadOnlyCollectionPage<SecurityGroupRule> result = new BasicReadOnlyCollectionPage<SecurityGroupRule>(securityGroupRules, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListSecurityGroupRulesApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetSecurityGroupRuleApiCall> PrepareGetSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-group-rules/{security_group_rule_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_rule_id", securityGroupRuleId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetSecurityGroupRuleApiCall(factory.CreateJsonApiCall<SecurityGroupRuleResponse>(task.Result)));
        }

        //public static Task<UpdateSecurityGroupRuleApiCall> PrepareUpdateSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, SecurityGroupRuleRequest request, CancellationToken cancellationToken)
        //{
        //    UriTemplate template = new UriTemplate("security-group-rules/{security_group_rule_id}");
        //    Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_rule_id", securityGroupRuleId.Value } };

        //    IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
        //    return client.GetBaseUriAsync(cancellationToken)
        //        .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
        //        .Select(task => new UpdateSecurityGroupRuleApiCall(factory.CreateJsonApiCall<SecurityGroupRuleResponse>(task.Result)));
        //}

        public static Task<RemoveSecurityGroupRuleApiCall> PrepareRemoveSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("security-group-rules/{security_group_rule_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "security_group_rule_id", securityGroupRuleId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveSecurityGroupRuleApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<SecurityGroup> AddSecurityGroupAsync(this INetworkingService client, SecurityGroupData securityGroupData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddSecurityGroupAsync(new SecurityGroupRequest(securityGroupData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.SecurityGroup);
        }

        public static Task<ReadOnlyCollectionPage<SecurityGroup>> ListSecurityGroupsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListSecurityGroupsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<SecurityGroup> GetSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetSecurityGroupAsync(securityGroupId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.SecurityGroup);
        }

        //public static Task<SecurityGroup> UpdateSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, SecurityGroupData securityGroupData, CancellationToken cancellationToken)
        //{
        //    return
        //        CoreTaskExtensions.Using(
        //            () => client.PrepareUpdateSecurityGroupAsync(securityGroupId, new SecurityGroupRequest(securityGroupData), cancellationToken),
        //            task => task.Result.SendAsync(cancellationToken))
        //        .Select(task => task.Result.Item2.SecurityGroup);
        //}

        public static Task RemoveSecurityGroupAsync(this INetworkingService client, SecurityGroupId securityGroupId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareRemoveSecurityGroupAsync(securityGroupId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<SecurityGroupRule> AddSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleData securityGroupRuleData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddSecurityGroupRuleAsync(new SecurityGroupRuleRequest(securityGroupRuleData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.SecurityGroupRule);
        }


        public static Task<ReadOnlyCollectionPage<SecurityGroupRule>> ListSecurityGroupRuleAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListSecurityGroupRulesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<SecurityGroupRule> GetSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetSecurityGroupRuleAsync(securityGroupRuleId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.SecurityGroupRule);
        }


        //public static Task<SecurityGroupRule> UpdateSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, SecurityGroupRuleData securityGroupRuleData, CancellationToken cancellationToken)
        //{
        //    return
        //        CoreTaskExtensions.Using(
        //            () => client.PrepareUpdateSecurityGroupRuleAsync(securityGroupRuleId, new SecurityGroupRuleRequest(securityGroupRuleData), cancellationToken),
        //            task => task.Result.SendAsync(cancellationToken))
        //        .Select(task => task.Result.Item2.SecurityGroupRule);
        //}

        public static Task RemoveSecurityGroupRuleAsync(this INetworkingService client, SecurityGroupRuleId securityGroupRuleId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareRemoveSecurityGroupRuleAsync(securityGroupRuleId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken));
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(INetworkingService client)
        {
            IHttpApiCallFactory factory = client as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
