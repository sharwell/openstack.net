namespace OpenStack.Services.Orchestration.V1
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
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Compute;
    using OpenStack.Services.Identity;
    using Rackspace.Net;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides a default implementation of <see cref="IOrchestrationService"/> suitable for connecting to
    /// OpenStack-compatible installations of the Orchestration Service V1.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class OrchestrationClient : ServiceClient, IOrchestrationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrchestrationClient"/> class with the specified authentication
        /// service and default region.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use for authenticating requests made to
        /// this service.</param>
        /// <param name="defaultRegion">The preferred region for the service. Unless otherwise specified for a specific
        /// client, derived service clients will not use a default region if this value is <see langword="null"/> (i.e.
        /// only region-less or "global" service endpoints will be considered acceptable).</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="authenticationService"/> is <see langword="null"/>.
        /// </exception>
        public OrchestrationClient(IAuthenticationService authenticationService, string defaultRegion)
            : base(authenticationService, defaultRegion)
        {
        }

        #region IOrchestrationService

        #region API versions

        #endregion

        #region Stacks

        /// <inheritdoc/>
        public virtual Task<CreateStackApiCall> PrepareCreateStackAsync(StackData requestData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("stacks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, requestData, cancellationToken))
                .Select(task => new CreateStackApiCall(CreateJsonApiCall<StackResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<AdoptStackApiCall> PrepareAdoptStackAsync(AdoptStackData requestData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("stacks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, requestData, cancellationToken))
                .Select(task => new AdoptStackApiCall(CreateJsonApiCall<StackResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ListStacksApiCall> PrepareListStacksAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("stacks");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Stack>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["stacks"] as JArray;
                                if (listing == null)
                                    return null;

                                Stack[] stacks = listing.ToObject<Stack[]>();
                                // http://docs.rackspace.com/orchestration/api/v1/orchestration-devguide/content/pagination.html
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Stack>>> getNextPageAsync = null;
                                JArray linksArray = response["links"] as JArray;
                                if (linksArray != null)
                                {
                                    IList<Link> extensionsLinks = linksArray.ToObject<Link[]>();
                                    Link nextLink = extensionsLinks.FirstOrDefault(i => string.Equals("next", i.Relation, StringComparison.OrdinalIgnoreCase));
                                    if (nextLink != null)
                                    {
                                        getNextPageAsync =
                                            nextCancellationToken =>
                                            {
                                                return PrepareListStacksAsync(nextCancellationToken)
                                                    .WithUri(nextLink.Target)
                                                    .Then(nextApiCall => nextApiCall.Result.SendAsync(nextCancellationToken))
                                                    .Select(nextApiCallResult => nextApiCallResult.Result.Item2);
                                            };
                                    }
                                }

                                ReadOnlyCollectionPage<Stack> result = new BasicReadOnlyCollectionPage<Stack>(stacks, getNextPageAsync);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListStacksApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetStackApiCall> PrepareGetStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetStackApiCall(CreateJsonApiCall<StackResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<UpdateStackApiCall> PrepareUpdateStackAsync(StackName stackName, StackId stackId, StackData requestData, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, requestData, cancellationToken))
                .Select(task => new UpdateStackApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveStackApiCall> PrepareRemoveStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveStackApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<PreviewStackApiCall> PreparePreviewStackAsync(StackData requestData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("stacks/preview");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, requestData, cancellationToken))
                .Select(task => new PreviewStackApiCall(CreateJsonApiCall<StackResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<AbandonStackApiCall> PrepareAbandonStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/abandon");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new AbandonStackApiCall(CreateJsonApiCall<Stack>(task.Result)));
        }

        #endregion

        #region Stack actions

        /// <inheritdoc/>
        public virtual Task<StackActionApiCall<T>> PrepareStackActionAsync<T>(StackName stackName, StackId stackId, StackActionRequest request, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/actions");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new StackActionApiCall<T>(CreateJsonApiCall<T>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<SuspendStackApiCall> PrepareSuspendStackAsync(StackName stackName, StackId stackId, SuspendStackRequest request, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/actions");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new SuspendStackApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ResumeStackApiCall> PrepareResumeStackAsync(StackName stackName, StackId stackId, ResumeStackRequest request, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/actions");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ResumeStackApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        #region Stack resources

        /// <inheritdoc/>
        public virtual Task<ListResourcesApiCall> PrepareListResourcesAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/resources");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Resource>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["resources"] as JArray;
                                if (listing == null)
                                    return null;

                                Resource[] resources = listing.ToObject<Resource[]>();
                                // This call does not appear to be paginated
                                ReadOnlyCollectionPage<Resource> result = new BasicReadOnlyCollectionPage<Resource>(resources, null);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListResourcesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetResourceApiCall> PrepareGetResourceAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");
            if (resourceName == null)
                throw new ArgumentNullException("resourceName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/resources/{resource_name}");
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "stack_name", stackName.Value },
                    { "resource_name", resourceName.Value },
                };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetResourceApiCall(CreateJsonApiCall<ResourceResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetResourceMetadataApiCall> PrepareGetResourceMetadataAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");
            if (resourceName == null)
                throw new ArgumentNullException("resourceName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/resources/{resource_name}/metadata");
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "stack_name", stackName.Value },
                    { "resource_name", resourceName.Value },
                };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetResourceMetadataApiCall(CreateJsonApiCall<ResourceMetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ListResourceTypesApiCall> PrepareListResourceTypesAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("resource_types");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<ResourceTypeName>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["resource_types"] as JArray;
                                if (listing == null)
                                    return null;

                                ResourceTypeName[] resourceTypes = listing.ToObject<ResourceTypeName[]>();
                                // this response does not appear to be paginated
                                ReadOnlyCollectionPage<ResourceTypeName> result = new BasicReadOnlyCollectionPage<ResourceTypeName>(resourceTypes, null);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListResourceTypesApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetResourceTypeSchemaApiCall> PrepareGetResourceTypeSchemaAsync(ResourceTypeName resourceTypeName, CancellationToken cancellationToken)
        {
            if (resourceTypeName == null)
                throw new ArgumentNullException("resourceTypeName");

            UriTemplate template = new UriTemplate("resource_types/{type_name}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "type_name", resourceTypeName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetResourceTypeSchemaApiCall(CreateJsonApiCall<ResourceTypeSchema>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetResourceTypeTemplateApiCall> PrepareGetResourceTypeTemplateAsync(ResourceTypeName resourceTypeName, CancellationToken cancellationToken)
        {
            if (resourceTypeName == null)
                throw new ArgumentNullException("resourceTypeName");

            UriTemplate template = new UriTemplate("resource_types/{type_name}/template");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "type_name", resourceTypeName.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetResourceTypeTemplateApiCall(CreateJsonApiCall<ResourceTypeTemplate>(task.Result)));
        }

        #endregion

        #region Stack events

        /// <inheritdoc/>
        public virtual Task<ListEventsApiCall> PrepareListEventsAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/events");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Event>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["events"] as JArray;
                                if (listing == null)
                                    return null;

                                Event[] events = listing.ToObject<Event[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Event>>> getNextPageAsync;
                                if (events.Length > 0)
                                {
                                    Uri lastUri = responseMessage.RequestMessage.RequestUri;
                                    EventId lastEventId = events.Last().Id;
                                    getNextPageAsync =
                                        innerCancellationToken2 => TaskBlocks.Using(
                                            () => PrepareListEventsAsync(stackName, stackId, innerCancellationToken2)
                                                .WithUri(lastUri)
                                                .WithMarker(lastEventId),
                                            task => task.Result.SendAsync(innerCancellationToken2).Select(innerTask2 => innerTask2.Result.Item2));
                                }
                                else
                                {
                                    getNextPageAsync = null;
                                }

                                ReadOnlyCollectionPage<Event> result = new BasicReadOnlyCollectionPage<Event>(events, getNextPageAsync);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListEventsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<ListResourceEventsApiCall> PrepareListResourceEventsAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");
            if (resourceName == null)
                throw new ArgumentNullException("resourceName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/resources/{resource_name}/events?limit=1");
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "stack_name", stackName.Value },
                    { "resource_name", resourceName.Value },
                };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Event>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["events"] as JArray;
                                if (listing == null)
                                    return null;

                                Event[] events = listing.ToObject<Event[]>();
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Event>>> getNextPageAsync;
                                if (events.Length > 0)
                                {
                                    Uri lastUri = responseMessage.RequestMessage.RequestUri;
                                    EventId lastEventId = events.Last().Id;
                                    getNextPageAsync =
                                        innerCancellationToken2 => TaskBlocks.Using(
                                            () => PrepareListResourceEventsAsync(stackName, stackId, resourceName, innerCancellationToken2)
                                                .WithUri(lastUri)
                                                .WithMarker(lastEventId),
                                            task => task.Result.SendAsync(innerCancellationToken2).Select(innerTask2 => innerTask2.Result.Item2));
                                }
                                else
                                {
                                    getNextPageAsync = null;
                                }

                                ReadOnlyCollectionPage<Event> result = new BasicReadOnlyCollectionPage<Event>(events, getNextPageAsync);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListResourceEventsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<GetResourceEventApiCall> PrepareGetResourceEventAsync(StackName stackName, StackId stackId, ResourceName resourceName, EventId eventId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");
            if (resourceName == null)
                throw new ArgumentNullException("resourceName");
            if (eventId == null)
                throw new ArgumentNullException("eventId");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/resources/{resource_name}/events/{event_id}");
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "stack_name", stackName.Value },
                    { "resource_name", resourceName.Value },
                    { "event_id", eventId.Value },
                };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetResourceEventApiCall(CreateJsonApiCall<EventResponse>(task.Result)));
        }

        #endregion

        #region Templates

        /// <inheritdoc/>
        public virtual Task<GetStackTemplateApiCall> PrepareGetStackTemplateAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            UriTemplate template = new UriTemplate("stacks/{stack_name}{/stack_id}/template");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "stack_name", stackName.Value } };
            if (stackId != null)
                parameters.Add("stack_id", stackId.Value);

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetStackTemplateApiCall(CreateJsonApiCall<StackTemplate>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ValidateTemplateApiCall> PrepareValidateTemplateAsync(ValidateTemplateRequest request, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("validate");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, request, cancellationToken))
                .Select(task => new ValidateTemplateApiCall(CreateJsonApiCall<TemplateValidationInformation>(task.Result)));
        }

        #endregion

        #region Build info

        /// <inheritdoc/>
        public virtual Task<GetBuildInformationApiCall> PrepareGetBuildInformationAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("build_info");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetBuildInformationApiCall(CreateJsonApiCall<BuildInformation>(task.Result)));
        }

        #endregion

        #region Software configuration

        /// <inheritdoc/>
        public virtual Task<CreateSoftwareConfigurationApiCall> PrepareCreateSoftwareConfigurationAsync(SoftwareConfigurationData requestData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("software_configs");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, requestData, cancellationToken))
                .Select(task => new CreateSoftwareConfigurationApiCall(CreateJsonApiCall<SoftwareConfigurationResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetSoftwareConfigurationApiCall> PrepareGetSoftwareConfigurationAsync(SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken)
        {
            if (softwareConfigurationId == null)
                throw new ArgumentNullException("softwareConfigurationId");

            UriTemplate template = new UriTemplate("software_configs/{config_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "config_id", softwareConfigurationId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetSoftwareConfigurationApiCall(CreateJsonApiCall<SoftwareConfigurationResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveSoftwareConfigurationApiCall> PrepareRemoveSoftwareConfigurationAsync(SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken)
        {
            if (softwareConfigurationId == null)
                throw new ArgumentNullException("softwareConfigurationId");

            UriTemplate template = new UriTemplate("software_configs/{config_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "config_id", softwareConfigurationId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveSoftwareConfigurationApiCall(CreateBasicApiCall(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<ListDeploymentsApiCall> PrepareListDeploymentsAsync(CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("software_deployments");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Func<HttpResponseMessage, CancellationToken, Task<ReadOnlyCollectionPage<Deployment>>> deserializeResult =
                (responseMessage, innerCancellationToken) =>
                {
                    if (!HttpApiCall.IsAcceptable(responseMessage))
                        throw new HttpWebException(responseMessage);

                    return responseMessage.Content.ReadAsStringAsync()
                        .Select(
                            innerTask =>
                            {
                                JObject response = JsonConvert.DeserializeObject<JObject>(innerTask.Result);
                                if (response == null)
                                    return null;

                                JArray listing = response["software_deployments"] as JArray;
                                if (listing == null)
                                    return null;

                                Deployment[] deployments = listing.ToObject<Deployment[]>();
                                // http://docs.rackspace.com/orchestration/api/v1/orchestration-devguide/content/pagination.html
                                Func<CancellationToken, Task<ReadOnlyCollectionPage<Deployment>>> getNextPageAsync = null;
                                JArray linksArray = response["links"] as JArray;
                                if (linksArray != null)
                                {
                                    IList<Link> extensionsLinks = linksArray.ToObject<Link[]>();
                                    Link nextLink = extensionsLinks.FirstOrDefault(i => string.Equals("next", i.Relation, StringComparison.OrdinalIgnoreCase));
                                    if (nextLink != null)
                                    {
                                        getNextPageAsync =
                                            nextCancellationToken =>
                                            {
                                                return PrepareListDeploymentsAsync(nextCancellationToken)
                                                    .WithUri(nextLink.Target)
                                                    .Then(nextApiCall => nextApiCall.Result.SendAsync(nextCancellationToken))
                                                    .Select(nextApiCallResult => nextApiCallResult.Result.Item2);
                                            };
                                    }
                                }

                                ReadOnlyCollectionPage<Deployment> result = new BasicReadOnlyCollectionPage<Deployment>(deployments, getNextPageAsync);
                                return result;
                            });
                };

            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new ListDeploymentsApiCall(CreateCustomApiCall(task.Result, HttpCompletionOption.ResponseContentRead, deserializeResult)));
        }

        /// <inheritdoc/>
        public virtual Task<CreateDeploymentApiCall> PrepareCreateDeploymentAsync(DeploymentData requestData, CancellationToken cancellationToken)
        {
            UriTemplate template = new UriTemplate("software_deployments");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Post, template, parameters, requestData, cancellationToken))
                .Select(task => new CreateDeploymentApiCall(CreateJsonApiCall<DeploymentResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetDeploymentMetadataApiCall> PrepareGetDeploymentMetadataAsync(ServerId serverId, CancellationToken cancellationToken)
        {
            if (serverId == null)
                throw new ArgumentNullException("serverId");

            UriTemplate template = new UriTemplate("software_deployments/metadata/{server_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "server_id", serverId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetDeploymentMetadataApiCall(CreateJsonApiCall<DeploymentMetadataResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<GetDeploymentApiCall> PrepareGetDeploymentAsync(DeploymentId deploymentId, CancellationToken cancellationToken)
        {
            if (deploymentId == null)
                throw new ArgumentNullException("deploymentId");

            UriTemplate template = new UriTemplate("software_deployments/{deployment_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "deployment_id", deploymentId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Get, template, parameters, cancellationToken))
                .Select(task => new GetDeploymentApiCall(CreateJsonApiCall<DeploymentResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<UpdateDeploymentApiCall> PrepareUpdateDeploymentAsync(DeploymentId deploymentId, DeploymentData requestData, CancellationToken cancellationToken)
        {
            if (deploymentId == null)
                throw new ArgumentNullException("deploymentId");

            UriTemplate template = new UriTemplate("software_deployments/{deployment_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "deployment_id", deploymentId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Put, template, parameters, requestData, cancellationToken))
                .Select(task => new UpdateDeploymentApiCall(CreateJsonApiCall<DeploymentResponse>(task.Result)));
        }

        /// <inheritdoc/>
        public virtual Task<RemoveDeploymentApiCall> PrepareRemoveDeploymentAsync(DeploymentId deploymentId, CancellationToken cancellationToken)
        {
            if (deploymentId == null)
                throw new ArgumentNullException("deploymentId");

            UriTemplate template = new UriTemplate("software_deployments/{deployment_id}");
            Dictionary<string, string> parameters = new Dictionary<string, string> { { "deployment_id", deploymentId.Value } };
            return GetBaseUriAsync(cancellationToken)
                .Then(PrepareRequestAsyncFunc(HttpMethod.Delete, template, parameters, cancellationToken))
                .Select(task => new RemoveDeploymentApiCall(CreateBasicApiCall(task.Result)));
        }

        #endregion

        /// <inheritdoc/>
        public virtual Task<Tuple<HttpResponseMessage, StackResponse>> WaitForStackOperationAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken, IProgress<Tuple<HttpResponseMessage, StackResponse>> progress)
        {
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            Func<Task<IEnumerator<TimeSpan>>> resource = () => CompletedTask.FromResult(BackoffPolicy.GetBackoffIntervals().GetEnumerator());
            Func<Task<IEnumerator<TimeSpan>>, Task<Tuple<HttpResponseMessage, StackResponse>>> body =
                resourceTask =>
                {
                    Tuple<HttpResponseMessage, StackResponse> stackDetails = null;
                    bool complete = false;
                    Func<bool> condition = () => !complete && resourceTask.Result.MoveNext();
                    Func<Task> whileBody =
                        () => DelayedTask.Delay(resourceTask.Result.Current)
                            .Then(_ => TaskBlocks.Using(
                                () => PrepareGetStackAsync(stackName, stackId, cancellationToken),
                                task => task.Result.SendAsync(cancellationToken)))
                            .Select(
                                stackTask =>
                                {
                                    stackDetails = stackTask.Result;
                                    complete = !IsOperationInProgress(stackDetails.Item1, stackDetails.Item2);

                                    if (progress != null)
                                        progress.Report(stackDetails);
                                });
                    Func<Task, Task<Tuple<HttpResponseMessage, StackResponse>>> selector =
                        _ => stackDetails != null ? CompletedTask.FromResult(stackDetails) : CompletedTask.Canceled<Tuple<HttpResponseMessage, StackResponse>>();

                    return TaskBlocks.While(condition, whileBody).Then(selector);
                };

            return TaskBlocks.Using(resource, body);
        }

        /// <inheritdoc/>
        public virtual TExtension GetServiceExtension<TExtension>(ServiceExtensionDefinition<IOrchestrationService, TExtension> definition)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            return definition.CreateDefaultInstance(this, this);
        }

        #endregion

        /// <summary>
        /// Determines if an operation is in progress for a particular <see cref="Stack"/>, based on the information
        /// returned by the <see cref="GetStackApiCall"/> HTTP API call.
        /// </summary>
        /// <remarks>
        /// <para>This method is used to implement the inner polling operation in
        /// <see cref="WaitForStackOperationAsync"/>. The default implementation determines that an operation is in
        /// progress if the <see cref="Stack.Status"/> property contains the substring <c>IN_PROGRESS</c>
        /// (case-insensitive).</para>
        /// </remarks>
        /// <param name="getStackResponseMessage">The <see cref="HttpResponseMessage"/> representing the raw response to
        /// the HTTP API call.</param>
        /// <param name="stackResponse">The <see cref="StackResponse"/> representing the deserialized response to the
        /// HTTP API call.</param>
        /// <returns><see langword="true"/> if an asynchronous stack operation is in progress; otherwise,
        /// <see langword="false"/>.</returns>
        protected virtual bool IsOperationInProgress(HttpResponseMessage getStackResponseMessage, StackResponse stackResponse)
        {
            if (stackResponse == null || stackResponse.Stack == null)
                return false;

            StackStatus status = stackResponse.Stack.Status;
            if (status == null)
                return false;

            return status.Name.IndexOf("IN_PROGRESS", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method calls <see cref="IAuthenticationService.GetBaseAddressAsync"/> to obtain a URI for the type
        /// <c>orchestration</c>. The preferred name is not specified.
        /// </remarks>
        protected override Task<Uri> GetBaseUriAsyncImpl(CancellationToken cancellationToken)
        {
            const string serviceType = "orchestration";
            const string serviceName = null;
            return AuthenticationService.GetBaseAddressAsync(serviceType, serviceName, DefaultRegion, false, cancellationToken);
        }
    }
}
