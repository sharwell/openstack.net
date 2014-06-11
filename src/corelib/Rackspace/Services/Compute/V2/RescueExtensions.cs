﻿namespace Rackspace.Services.Compute.V2
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using OpenStack.Services.Compute.V2;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class defines extensions for preparing and sending Rackspace-specific
    /// API requests to place a server resource into, and take it back out of,
    /// rescue mode.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class RescueExtensions
    {
        public static Task<RescueServerApiCall> PrepareRescueServerAsync(this IComputeService service, ServerId serverId, RescueServerRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new RescueServerApiCall(factory.CreateJsonApiCall<RescueServerResponse>(task.Result)));
        }

        public static Task<UnrescueServerApiCall> PrepareUnrescueServerAsync(this IComputeService service, ServerId serverId, UnrescueServerRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("servers/{server_id}/action");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };

            IHttpApiCallFactory factory = GetHttpApiCallFactory(service);
            return service.GetBaseUriAsync(cancellationToken)
                .Then(service.PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new UnrescueServerApiCall(factory.CreateBasicApiCall(task.Result, HttpCompletionOption.ResponseContentRead)));
        }

        public static Task<string> RescueServerAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareRescueServerAsync(serverId, new RescueServerRequest(), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.AdministratorPassword);
        }

        public static Task UnrescueServerAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareUnrescueServerAsync(serverId, new UnrescueServerRequest(), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        private static IHttpApiCallFactory GetHttpApiCallFactory(IComputeService service)
        {
            IHttpApiCallFactory factory = service as IHttpApiCallFactory;
            return factory ?? new HttpApiCallFactory();
        }
    }
}
