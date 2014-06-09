namespace OpenStack.Services.Networking.V2.Quotas
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
    using OpenStack.Services.Identity;
    using Rackspace.Net;
    using Rackspace.Threading;
    using ExtensionAlias = OpenStack.Services.Compute.V2.ExtensionAlias;

    public static class QuotasExtensions
    {
        public static readonly ExtensionAlias ExtensionAlias = new ExtensionAlias("quotas");

        public static Task<bool> SupportsQuotasAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return client.ListExtensionsAsync(cancellationToken)
                .Select(task => task.Result.Any(i => ExtensionAlias.Equals(i.Alias)));
        }

        public static Task<ListQuotasApiCall> PrepareListQuotasAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("quotas");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Quota>>> deserializeResult =
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

                                JToken quotasToken = jsonObject["quotas"];
                                if (quotasToken == null)
                                    return null;

                                // the pagination behavior, if any, is not described in the documentation
                                Quota[] quotas = quotasToken.ToObject<Quota[]>();
                                ReadOnlyCollectionPage<Quota> result = new BasicReadOnlyCollectionPage<Quota>(quotas, null);
                                return result;
                            });
                };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListQuotasApiCall(factory.CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        public static Task<GetQuotasApiCall> PrepareGetQuotasAsync(this INetworkingService client, ProjectId projectId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("quotas/{project_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "project_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetQuotasApiCall(factory.CreateJsonApiCall<QuotaResponse>(task.Result)));
        }

        public static Task<UpdateQuotasApiCall> PrepareUpdateQuotasAsync(this INetworkingService client, ProjectId projectId, QuotaRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("quotas/{project_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "project_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateQuotasApiCall(factory.CreateJsonApiCall<QuotaResponse>(task.Result)));
        }

        public static Task<ResetQuotasApiCall> PrepareResetQuotasAsync(this INetworkingService client, ProjectId projectId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("quotas/{project_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "project_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(client);
            return client.GetBaseUriAsync(cancellationToken)
                .Then(client.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new ResetQuotasApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<ReadOnlyCollectionPage<Quota>> ListQuotasAsync(this INetworkingService client, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareListQuotasAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Quota> GetQuotasAsync(this INetworkingService client, ProjectId projectId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareGetQuotasAsync(projectId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Quota);
        }

        public static Task<Quota> UpdateQuotasAsync(this INetworkingService client, ProjectId projectId, QuotaData quotaData, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => client.PrepareUpdateQuotasAsync(projectId, new QuotaRequest(quotaData), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Quota);
        }

        public static Task ResetQuotasAsync(this INetworkingService client, ProjectId projectId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => client.PrepareResetQuotasAsync(projectId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(INetworkingService client)
        {
            IHttpApiCallFactory factory = client as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
