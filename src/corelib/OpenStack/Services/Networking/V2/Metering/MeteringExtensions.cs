namespace OpenStack.Services.Networking.V2.Metering
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

    public static class MeteringExtensions
    {
#warning this alias is not correct.
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("");

        public static Task<bool> SupportsMeteringAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static Task<AddMeteringLabelApiCall> PrepareAddMeteringLabelAsync(this INetworkingService client, MeteringLabelRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-labels");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddMeteringLabelApiCall(factory.CreateJsonApiCall<MeteringLabelResponse>(task.Result)));
        }

        public static Task<ListMeteringLabelsApiCall> PrepareListMeteringLabelsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-labels");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<MeteringLabel>>> deserializeResult =
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

                                JToken meteringLabelsToken = jsonObject["metering_labels"];
                                if (meteringLabelsToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                MeteringLabel[] meteringLabels = meteringLabelsToken.ToObject<MeteringLabel[]>();
                                ReadOnlyCollectionPage<MeteringLabel> result = new BasicReadOnlyCollectionPage<MeteringLabel>(meteringLabels, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListMeteringLabelsApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetMeteringLabelApiCall> PrepareGetMeteringLabelAsync(this INetworkingService client, MeteringLabelId meteringLabelId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-labels/{metering_label_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "metering_label_id", meteringLabelId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetMeteringLabelApiCall(factory.CreateJsonApiCall<MeteringLabelResponse>(task.Result)));
        }

        public static Task<RemoveMeteringLabelApiCall> PrepareRemoveMeteringLabelAsync(this INetworkingService client, MeteringLabelId meteringLabelId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-labels/{metering_label_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "metering_label_id", meteringLabelId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveMeteringLabelApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<AddMeteringLabelRuleApiCall> PrepareAddMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-label-rules");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new AddMeteringLabelRuleApiCall(factory.CreateJsonApiCall<MeteringLabelRuleResponse>(task.Result)));
        }

        public static Task<ListMeteringLabelRulesApiCall> PrepareListMeteringLabelRulesAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-label-rules");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<MeteringLabelRule>>> deserializeResult =
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

                                JToken meteringLabelRulesToken = jsonObject["metering_label_rules"];
                                if (meteringLabelRulesToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                MeteringLabelRule[] meteringLabelRules = meteringLabelRulesToken.ToObject<MeteringLabelRule[]>();
                                ReadOnlyCollectionPage<MeteringLabelRule> result = new BasicReadOnlyCollectionPage<MeteringLabelRule>(meteringLabelRules, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListMeteringLabelRulesApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetMeteringLabelRuleApiCall> PrepareGetMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleId meteringLabelRuleId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-label-rules/{metering_label_rule_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "metering_label_rule_id", meteringLabelRuleId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetMeteringLabelRuleApiCall(factory.CreateJsonApiCall<MeteringLabelRuleResponse>(task.Result)));
        }

        public static Task<RemoveMeteringLabelRuleApiCall> PrepareRemoveMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleId meteringLabelRuleId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("metering-label-rules/{metering_label_rule_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "metering_label_rule_id", meteringLabelRuleId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveMeteringLabelRuleApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<MeteringLabel> AddMeteringLabelAsync(this INetworkingService client, MeteringLabelData meteringLabelData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddMeteringLabelAsync(new MeteringLabelRequest(meteringLabelData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.MeteringLabel);
        }

        public static Task<ReadOnlyCollectionPage<MeteringLabel>> ListMeteringLabelsAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListMeteringLabelsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<MeteringLabel> GetMeteringLabelAsync(this INetworkingService client, MeteringLabelId meteringLabelId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetMeteringLabelAsync(meteringLabelId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.MeteringLabel);
        }

        public static Task RemoveMeteringLabelAsync(this INetworkingService client, MeteringLabelId meteringLabelId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveMeteringLabelAsync(meteringLabelId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<MeteringLabelRule> AddMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleData meteringLabelRuleData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareAddMeteringLabelRuleAsync(new MeteringLabelRuleRequest(meteringLabelRuleData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.MeteringLabelRule);
        }

        public static Task<ReadOnlyCollectionPage<MeteringLabelRule>> ListMeteringLabelRulesAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListMeteringLabelRulesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<MeteringLabelRule> GetMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleId meteringLabelRuleId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetMeteringLabelRuleAsync(meteringLabelRuleId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.MeteringLabelRule);
        }

        public static Task RemoveMeteringLabelRuleAsync(this INetworkingService client, MeteringLabelRuleId meteringLabelRuleId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareRemoveMeteringLabelRuleAsync(meteringLabelRuleId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(INetworkingService client)
        {
            IHttpApiCallFactory factory = client as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
