namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Services.Compute;
    using OpenStack.Threading;

    /// <summary>
    /// This is the base interface for the OpenStack Orchestration Service V1.
    /// </summary>
    /// <seealso href="http://developer.openstack.org/api-ref-orchestration-v1.html">Orchestration API V1 - OpenStack Complete API Reference</seealso>
    /// <preliminary/>
    public interface IOrchestrationService : IHttpService, IExtensibleService<IOrchestrationService>
    {
        #region API versions

        #endregion

        #region Stacks

#warning "Create Stack" and "Adopt Stack" use the same URI

        Task<CreateStackApiCall> PrepareCreateStackAsync(StackData requestData, CancellationToken cancellationToken);

        Task<AdoptStackApiCall> PrepareAdoptStackAsync(AdoptStackData requestData, CancellationToken cancellationToken);

        Task<ListStacksApiCall> PrepareListStacksAsync(CancellationToken cancellationToken);

        Task<GetStackApiCall> PrepareGetStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task<UpdateStackApiCall> PrepareUpdateStackAsync(StackName stackName, StackId stackId, StackData requestData, CancellationToken cancellationToken);

        Task<RemoveStackApiCall> PrepareRemoveStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task<PreviewStackApiCall> PreparePreviewStackAsync(StackData requestData, CancellationToken cancellationToken);

        Task<AbandonStackApiCall> PrepareAbandonStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        #endregion

#warning The entire Stack Actions section is omitted from the Rackspace documentation

        #region Stack actions

        Task<StackActionApiCall<T>> PrepareStackActionAsync<T>(StackName stackName, StackId stackId, StackActionRequest request, CancellationToken cancellationToken);

#warning The following two are just specific actions.

        Task<SuspendStackApiCall> PrepareSuspendStackAsync(StackName stackName, StackId stackId, SuspendStackRequest request, CancellationToken cancellationToken);

        Task<ResumeStackApiCall> PrepareResumeStackAsync(StackName stackName, StackId stackId, ResumeStackRequest request, CancellationToken cancellationToken);

        #endregion

        #region Stack resources

        Task<ListResourcesApiCall> PrepareListResourcesAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task<GetResourceApiCall> PrepareGetResourceAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken);

        Task<GetResourceMetadataApiCall> PrepareGetResourceMetadataAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken);

        Task<ListResourceTypesApiCall> PrepareListResourceTypesAsync(CancellationToken cancellationToken);

        Task<GetResourceTypeSchemaApiCall> PrepareGetResourceTypeSchemaAsync(ResourceTypeName resourceTypeName, CancellationToken cancellationToken);

        Task<GetResourceTypeTemplateApiCall> PrepareGetResourceTypeTemplateAsync(ResourceTypeName resourceTypeName, CancellationToken cancellationToken);

        #endregion

        #region Stack events

#warning Events are always associated with a resource. "List Stack Events" just lists events for all resources.

        Task<ListEventsApiCall> PrepareListEventsAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task<ListResourceEventsApiCall> PrepareListResourceEventsAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken);

        Task<GetResourceEventApiCall> PrepareGetResourceEventAsync(StackName stackName, StackId stackId, ResourceName resourceName, EventId eventId, CancellationToken cancellationToken);

        #endregion

        #region Templates

        Task<GetStackTemplateApiCall> PrepareGetStackTemplateAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task<ValidateTemplateApiCall> PrepareValidateTemplateAsync(ValidateTemplateRequest request, CancellationToken cancellationToken);

        #endregion

        #region Build info

        Task<GetBuildInformationApiCall> PrepareGetBuildInformationAsync(CancellationToken cancellationToken);

        #endregion

#warning The entire Software Configuration section is omitted from the Rackspace documentation

        #region Software configuration

#warning Can you list software configurations?

        Task<CreateSoftwareConfigurationApiCall> PrepareCreateSoftwareConfigurationAsync(SoftwareConfigurationData requestData, CancellationToken cancellationToken);

        Task<GetSoftwareConfigurationApiCall> PrepareGetSoftwareConfigurationAsync(SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken);

        Task<RemoveSoftwareConfigurationApiCall> PrepareRemoveSoftwareConfigurationAsync(SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken);

        Task<ListDeploymentsApiCall> PrepareListDeploymentsAsync(CancellationToken cancellationToken);

        Task<CreateDeploymentApiCall> PrepareCreateDeploymentAsync(DeploymentData requestData, CancellationToken cancellationToken);

        Task<GetDeploymentMetadataApiCall> PrepareGetDeploymentMetadataAsync(ServerId serverId, CancellationToken cancellationToken);

        Task<GetDeploymentApiCall> PrepareGetDeploymentAsync(DeploymentId deploymentId, CancellationToken cancellationToken);

        Task<UpdateDeploymentApiCall> PrepareUpdateDeploymentAsync(DeploymentId deploymentId, DeploymentData requestData, CancellationToken cancellationToken);

        Task<RemoveDeploymentApiCall> PrepareRemoveDeploymentAsync(DeploymentId deploymentId, CancellationToken cancellationToken);

        #endregion

        Task<Tuple<HttpResponseMessage, StackResponse>> WaitForStackOperationAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken, IProgress<Tuple<HttpResponseMessage, StackResponse>> progress);
    }
}
