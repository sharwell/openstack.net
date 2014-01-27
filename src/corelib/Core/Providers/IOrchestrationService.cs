namespace net.openstack.Core.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core.Collections;
    using net.openstack.Core.Domain.Orchestration;

    public interface IOrchestrationService
    {
        #region Stacks

        Task CreateStackAsync(NewStackConfiguration configuration, CancellationToken cancellationToken);

        Task<ReadOnlyCollectionPage<Stack>> ListStacksAsync(StackFilters filters, CancellationToken cancellationToken);

        Task<CancellationToken> GetStackUriAsync(StackName stackName, CancellationToken cancellationToken);

        Task GetStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task UpdateStackAsync(StackName stackName, StackId stackId, UpdateStackConfiguration configuration, CancellationToken cancellationToken);

        Task RemoveStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        #endregion Stacks

        #region Stack Actions

        Task SuspendStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task ResumeStackAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        #endregion Stack Actions

        #region Stack Resources

        Task ListResourcesAsync();

        Task GetResourceAsync();

        Task GetResourceMetadataAsync();

        Task ListResourceTypesAsync();

        Task GetResourceSchemaAsync();

        Task GetResourceTemplateAsync();

        #endregion Stack Resources

        #region Stack Events

        Task ListEventsAsync(StackName stackName, StackId stackId, CancellationToken cancellationToken);

        Task ListEventsAsync(StackName stackName, StackId stackId, ResourceName resourceName, CancellationToken cancellationToken);

        Task GetEventDataAsync(StackName stackName, StackId stackId, ResourceName resourceName, EventId eventId, CancellationToken cancellationToken);

        #endregion Stack Events

        #region Templates

        Task GetTemplateAsync();

        Task ValidateTemplateAsync();

        #endregion Templates

        #region Build Info

        Task<BuildInfo> GetBuildInfoAsync(CancellationToken cancellationToken);

        #endregion Build Info
    }
}
