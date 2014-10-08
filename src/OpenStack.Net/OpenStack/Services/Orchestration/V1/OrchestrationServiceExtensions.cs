namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Services.Compute;
    using OpenStack.Threading;
    using Rackspace.Threading;

    /// <summary>
    /// This class provides extension methods that simplify the process of preparing and sending Orchestration Service
    /// HTTP API calls for the most common use cases.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class OrchestrationServiceExtensions
    {
        #region API versions

        #endregion

        #region Stacks

        public static Task<Stack> CreateStackAsync(this IOrchestrationService service, StackData requestData, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                if (requestData == null || requestData.Name == null)
                    throw new NotSupportedException();
            }

            Task<Stack> result = TaskBlocks.Using(
                () => service.PrepareCreateStackAsync(requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Stack));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(
                    antecedent =>
                    {
                        StackName stackName = requestData.Name;
                        StackId stackId = antecedent.Result.Id;
                        return service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress);
                    });
            }

            return result;
        }

        public static Task<Stack> AdoptStackAsync(this IOrchestrationService service, AdoptStackData requestData, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                if (requestData == null || requestData.Name == null)
                    throw new NotSupportedException();
            }

            Task<Stack> result = TaskBlocks.Using(
                () => service.PrepareAdoptStackAsync(requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Stack));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(
                    antecedent =>
                    {
                        StackName stackName = requestData.Name;
                        StackId stackId = antecedent.Result.Id;
                        return service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress);
                    });
            }

            return result;
        }

        public static Task<ReadOnlyCollectionPage<Stack>> ListStacksAsync(this IOrchestrationService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListStacksAsync(cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<Stack> GetStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetStackAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Stack));
        }

        public static Task UpdateStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, StackData requestData, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            Task result = TaskBlocks.Using(
                () => service.PrepareUpdateStackAsync(stackName, stackId, requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(antecedent => service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress));
            }

            return result;
        }

        public static Task RemoveStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            Task result = TaskBlocks.Using(
                () => service.PrepareRemoveStackAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(antecedent => service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress));
            }

            return result;
        }

        public static Task<Stack> PreviewStackAsync(this IOrchestrationService service, StackData requestData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PreparePreviewStackAsync(requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Stack));
        }

        public static Task<Stack> AbandonStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareAbandonStackAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        #endregion

        #region Stack actions

        public static Task<T> StackActionAsync<T>(this IOrchestrationService service, StackName stackName, StackId stackId, StackActionRequest request, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareStackActionAsync<T>(stackName, stackId, request, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task SuspendStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            Task result = TaskBlocks.Using(
                () => service.PrepareSuspendStackAsync(stackName, stackId, new SuspendStackRequest(), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(antecedent => service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress));
            }

            return result;
        }

        public static Task ResumeStackAsync(this IOrchestrationService service, StackName stackName, StackId stackId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            Task result = TaskBlocks.Using(
                () => service.PrepareResumeStackAsync(stackName, stackId, new ResumeStackRequest(), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestCompleted)
            {
                result = result.Then(antecedent => service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, progress));
            }

            return result;
        }

        #endregion

        #region Stack resources

        public static Task<ReadOnlyCollectionPage<Resource>> ListResourcesAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListResourcesAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<Resource> GetResourceAsync(this IOrchestrationService service, StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetResourceAsync(stackName, stackId, resourceName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Resource));
        }

        public static Task<ReadOnlyDictionary<string, JToken>> GetResourceMetadataAsync(this IOrchestrationService service, StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetResourceMetadataAsync(stackName, stackId, resourceName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Metadata));
        }

        public static Task<ReadOnlyCollectionPage<ResourceTypeName>> ListResourceTypesAsync(this IOrchestrationService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListResourceTypesAsync(cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<ResourceTypeSchema> GetResourceTypeSchemaAsync(this IOrchestrationService service, ResourceTypeName resourceTypeName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetResourceTypeSchemaAsync(resourceTypeName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<ResourceTypeTemplate> GetResourceTypeTemplateAsync(this IOrchestrationService service, ResourceTypeName resourceTypeName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetResourceTypeTemplateAsync(resourceTypeName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        #endregion

        #region Stack events

        public static Task<ReadOnlyCollectionPage<Event>> ListEventsAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListEventsAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<ReadOnlyCollectionPage<Event>> ListResourceEventsAsync(this IOrchestrationService service, StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListResourceEventsAsync(stackName, stackId, resourceName, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<Event> GetResourceEventAsync(this IOrchestrationService service, StackName stackName, StackId stackId, ResourceName resourceName, EventId eventId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetResourceEventAsync(stackName, stackId, resourceName, eventId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Event));
        }

        #endregion

        #region Templates

        public static Task<StackTemplate> GetStackTemplateAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetStackTemplateAsync(stackName, stackId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<TemplateValidationInformation> ValidateTemplateAsync(this IOrchestrationService service, ValidateTemplateRequest request, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareValidateTemplateAsync(request, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        #endregion

        #region Build info

        public static Task<BuildInformation> GetBuildInformationAsync(this IOrchestrationService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetBuildInformationAsync(cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        #endregion

        #region Software configuration

        public static Task<SoftwareConfiguration> CreateSoftwareConfigurationAsync(this IOrchestrationService service, SoftwareConfigurationData requestData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareCreateSoftwareConfigurationAsync(requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.SoftwareConfiguration));
        }

        public static Task<SoftwareConfiguration> GetSoftwareConfigurationAsync(this IOrchestrationService service, SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetSoftwareConfigurationAsync(softwareConfigurationId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.SoftwareConfiguration));
        }

        public static Task RemoveSoftwareConfigurationAsync(this IOrchestrationService service, SoftwareConfigurationId softwareConfigurationId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareRemoveSoftwareConfigurationAsync(softwareConfigurationId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ReadOnlyCollectionPage<Deployment>> ListDeploymentsAsync(this IOrchestrationService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareListDeploymentsAsync(cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        public static Task<Deployment> CreateDeploymentAsync(this IOrchestrationService service, DeploymentData requestData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareCreateDeploymentAsync(requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Deployment));
        }

        public static Task<ReadOnlyCollection<DeploymentMetadata>> GetDeploymentMetadataAsync(this IOrchestrationService service, ServerId serverId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetDeploymentMetadataAsync(serverId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Metadata));
        }

        public static Task<Deployment> GetDeploymentAsync(this IOrchestrationService service, DeploymentId deploymentId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareGetDeploymentAsync(deploymentId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Deployment));
        }

        public static Task<Deployment> UpdateDeploymentAsync(this IOrchestrationService service, DeploymentId deploymentId, DeploymentData requestData, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareUpdateDeploymentAsync(deploymentId, requestData, cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2.Deployment));
        }

        public static Task RemoveDeploymentAsync(this IOrchestrationService service, DeploymentId deploymentId, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return TaskBlocks.Using(
                () => service.PrepareRemoveDeploymentAsync(deploymentId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        public static Task<Stack> WaitForStackOperationAsync(this IOrchestrationService service, StackName stackName, StackId stackId, CancellationToken cancellationToken, IProgress<Stack> progress)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            if (stackName == null)
                throw new ArgumentNullException("stackName");

            return service.WaitForStackOperationAsync(stackName, stackId, cancellationToken, StackProgressWrapper.FromProgress(progress))
                .Select(task => task.Result.Item2.Stack);
        }

        private class StackProgressWrapper : IProgress<Tuple<HttpResponseMessage, StackResponse>>
        {
            private readonly IProgress<Stack> _progress;

            public StackProgressWrapper(IProgress<Stack> progress)
            {
                _progress = progress;
            }

            public static StackProgressWrapper FromProgress(IProgress<Stack> progress)
            {
                if (progress == null)
                    return null;

                return new StackProgressWrapper(progress);
            }

            public void Report(Tuple<HttpResponseMessage, StackResponse> value)
            {
                _progress.Report(value.Item2.Stack);
            }
        }
    }
}
