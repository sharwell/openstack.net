namespace Rackspace.Services.AutoScale.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Services;

    public interface IAutoScaleService : IHttpService
    {
        #region Scaling groups

        Task<ListScalingGroupsApiCall> PrepareListScalingGroupsAsync(CancellationToken cancellationToken);

        Task<CreateScalingGroupApiCall> PrepareCreateScalingGroupAsync(ScalingGroupData request, CancellationToken cancellationToken);

        Task<GetScalingGroupApiCall> PrepareGetScalingGroupAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        Task<RemoveScalingGroupApiCall> PrepareRemoveScalingGroupAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        Task<GetScalingGroupStateApiCall> PrepareGetScalingGroupStateAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        #endregion

        #region Configurations

        Task<GetScalingGroupConfigurationApiCall> PrepareGetScalingGroupConfigurationAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        Task<UpdateScalingGroupConfigurationApiCall> PrepareUpdateScalingGroupConfigurationAsync(ScalingGroupId scalingGroupId, GroupConfiguration configuration, CancellationToken cancellationToken);

        Task<GetScalingGroupLaunchConfigurationApiCall> PrepareGetScalingGroupLaunchConfigurationAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        Task<UpdateScalingGroupLaunchConfigurationApiCall> PrepareUpdateScalingGroupLaunchConfigurationAsync(ScalingGroupId scalingGroupId, LaunchConfiguration configuration, CancellationToken cancellationToken);

        #endregion

        #region Policies

        Task<ListPoliciesApiCall> PrepareListPoliciesAsync(ScalingGroupId scalingGroupId, CancellationToken cancellationToken);

        Task<CreatePoliciesApiCall> PrepareCreatePoliciesAsync(ScalingGroupId scalingGroupId, IEnumerable<PolicyData> policies, CancellationToken cancellationToken);

        Task<GetPolicyApiCall> PrepareGetPolicyAsync(ScalingGroupId scalingGroupId, PolicyId policyId, CancellationToken cancellationToken);

        Task<UpdatePolicyApiCall> PrepareUpdatePolicyAsync(ScalingGroupId scalingGroupId, PolicyId policyId, PolicyData policyData, CancellationToken cancellationToken);

        Task<RemovePolicyApiCall> PrepareRemovePolicyAsync(ScalingGroupId scalingGroupId, PolicyId policyId, CancellationToken cancellationToken);

        Task<ExecutePolicyApiCall> PrepareExecutePolicyAsync(ScalingGroupId scalingGroupId, PolicyId policyId, CancellationToken cancellationToken);

        #endregion

        #region Executions

        #endregion

        #region Webhooks

        Task<ListWebhooksApiCall> PrepareListWebhooksAsync(ScalingGroupId scalingGroupId, PolicyId policyId, CancellationToken cancellationToken);

        Task<CreateWebhooksApiCall> PrepareCreateWebhooksAsync(ScalingGroupId scalingGroupId, PolicyId policyId, IEnumerable<WebhookData> webhooks, CancellationToken cancellationToken);

        Task<GetWebhookApiCall> PrepareGetWebhookAsync(ScalingGroupId scalingGroupId, PolicyId policyId, WebhookId webhookId, CancellationToken cancellationToken);

        Task<UpdateWebhookApiCall> PrepareUpdateWebhookAsync(ScalingGroupId scalingGroupId, PolicyId policyId, WebhookId webhookId, WebhookData webhookData, CancellationToken cancellationToken);

        Task<RemoveWebhookApiCall> PrepareRemoveWebhookAsync(ScalingGroupId scalingGroupId, PolicyId policyId, WebhookId webhookId, CancellationToken cancellationToken);

        #endregion
    }
}
