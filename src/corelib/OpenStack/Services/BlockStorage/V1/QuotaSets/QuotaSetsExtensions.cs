namespace OpenStack.Services.BlockStorage.V1.QuotaSets
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using OpenStack.Services.Identity;
    using Rackspace.Net;
    using Rackspace.Threading;

    public static class QuotaSetsExtensions
    {
        public static Task<GetQuotasApiCall> PrepareGetQuotasAsync(this IBlockStorageService service, ProjectId projectId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetQuotasApiCall(factory.CreateJsonApiCall<QuotaSetResponse>(task.Result)));
        }

        public static Task<UpdateQuotasApiCall> PrepareUpdateQuotasAsync(this IBlockStorageService service, ProjectId projectId, QuotaSetRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, request, cancellationToken))
                .Select(task => new UpdateQuotasApiCall(factory.CreateJsonApiCall<QuotaSetResponse>(task.Result)));
        }

        public static Task<RemoveQuotasApiCall> PrepareRemoveQuotasAsync(this IBlockStorageService service, ProjectId projectId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveQuotasApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<GetDefaultQuotasApiCall> PrepareGetDefaultQuotasAsync(this IBlockStorageService service, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/defaults");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetDefaultQuotasApiCall(factory.CreateJsonApiCall<QuotaSetResponse>(task.Result)));
        }

        public static Task<GetUserQuotasApiCall> PrepareGetUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}/{user_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value }, { "user_id", userId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetUserQuotasApiCall(factory.CreateJsonApiCall<QuotaSetResponse>(task.Result)));
        }

        public static Task<UpdateUserQuotasApiCall> PrepareUpdateUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, QuotaSetRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}/{user_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value }, { "user_id", userId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new UpdateUserQuotasApiCall(factory.CreateJsonApiCall<QuotaSetResponse>(task.Result)));
        }

        public static Task<RemoveUserQuotasApiCall> PrepareRemoveUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}/{user_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value }, { "user_id", userId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveUserQuotasApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<GetUserQuotaDetailsApiCall> PrepareGetUserQuotaDetailsAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("os-quota-sets/{tenant_id}/detail/{user_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "tenant_id", projectId.Value }, { "user_id", userId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetUserQuotaDetailsApiCall(factory.CreateJsonApiCall<QuotaSetDetailsResponse>(task.Result)));
        }

        public static Task<QuotaSet> GetQuotasAsync(this IBlockStorageService service, ProjectId projectId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetQuotasAsync(projectId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSet);
        }

        public static Task<QuotaSet> UpdateQuotasAsync(this IBlockStorageService service, ProjectId projectId, QuotaSet quotaSet, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateQuotasAsync(projectId, new QuotaSetRequest(quotaSet), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSet);
        }

        public static Task RemoveQuotasAsync(this IBlockStorageService service, ProjectId projectId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveQuotasAsync(projectId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<QuotaSet> GetDefaultQuotasAsync(this IBlockStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetDefaultQuotasAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSet);
        }

        public static Task<QuotaSet> GetUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetUserQuotasAsync(projectId, userId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSet);
        }

        public static Task<QuotaSet> UpdateUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, QuotaSet quotaSet, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateUserQuotasAsync(projectId, userId, new QuotaSetRequest(quotaSet), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSet);
        }

        public static Task RemoveUserQuotasAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveUserQuotasAsync(projectId, userId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<QuotaSetDetails> GetUserQuotaDetailsAsync(this IBlockStorageService service, ProjectId projectId, UserId userId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetUserQuotaDetailsAsync(projectId, userId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.QuotaSetDetails);
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(IBlockStorageService service)
        {
            IHttpApiCallFactory factory = service as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
