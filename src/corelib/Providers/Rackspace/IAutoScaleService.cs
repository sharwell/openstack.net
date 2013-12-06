namespace net.openstack.Providers.Rackspace
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using net.openstack.Providers.Rackspace.Objects.AutoScale;

    public interface IAutoScaleService
    {
        #region Groups

        Task<ReadOnlyCollection<ScalingGroup>> ListScalingGroupsAsync(ScalingGroupId marker, int? limit, CancellationToken cancellationToken);

        Task<ScalingGroup> CreateGroupAsync(ScalingGroupConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task<ScalingGroup> GetGroupAsync(ScalingGroupId groupId, CancellationToken cancellationToken);

        Task DeleteGroupAsync(ScalingGroupId groupId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task<GroupState> GetGroupStateAsync(ScalingGroupId groupId, CancellationToken cancellationToken);

        Task PauseGroupAsync(ScalingGroupId groupId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task ResumeGroupAsync(ScalingGroupId groupId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        #endregion Groups

        #region Configurations

        Task<GroupConfiguration> GetGroupConfigurationAsync(ScalingGroupId groupId, CancellationToken cancellationToken);

        Task SetGroupConfigurationAsync(ScalingGroupId groupId, GroupConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task<LaunchConfiguration> GetLaunchConfigurationAsync(ScalingGroupId groupId, CancellationToken cancellationToken);

        Task SetLaunchConfigurationAsync(ScalingGroupId groupId, LaunchConfiguration configuration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        #endregion Configurations

        #region Policies

        Task<ReadOnlyCollection<Policy>> ListPoliciesAsync(ScalingGroupId groupId, PolicyId marker, int? limit, CancellationToken cancellationToken);

        Task<Policy> CreatePolicyAsync(ScalingGroupId groupId, PolicyConfiguration policyConfiguration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task<Policy> GetPolicyAsync(ScalingGroupId groupId, PolicyId policyId, CancellationToken cancellationToken);

        Task SetPolicyAsync(ScalingGroupId groupId, PolicyId policyId, PolicyConfiguration policyConfiguration, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task DeletePolicyAsync(ScalingGroupId groupId, PolicyId policyId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        Task ExecutePolicyAsync(ScalingGroupId groupId, PolicyId policyId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<ScalingGroup> progress);

        #endregion Policies

        #region Webhooks

        Task<ReadOnlyCollection<Webhook>> ListWebhooksAsync(ScalingGroupId groupId, PolicyId policyId, WebhookId marker, int? limit, CancellationToken cancellationToken);

        Task<Webhook> CreateWebhookAsync(ScalingGroupId groupId, PolicyId policyId, NewWebhookConfiguration configuration, CancellationToken cancellationToken);

        Task<Webhook[]> CreateWebhookRangeAsync(ScalingGroupId groupId, PolicyId policyId, IEnumerable<NewWebhookConfiguration> configurations, CancellationToken cancellationToken);

        Task<Webhook> GetWebhookAsync(ScalingGroupId groupId, PolicyId policyId, WebhookId webhookId, CancellationToken cancellationToken);

        Task UpdateWebhookAsync(ScalingGroupId groupId, PolicyId policyId, WebhookId webhookId, UpdateWebhookConfiguration configuration, CancellationToken cancellationToken);

        Task DeleteWebhookAsync(ScalingGroupId groupId, PolicyId policyId, WebhookId webhookId, CancellationToken cancellationToken);

        #endregion Webhooks
    }
}
