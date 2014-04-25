namespace Rackspace.Security.Authentication
{
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Identity.V2;
    using Rackspace.Services.Identity.V2;

    public class RackspaceAuthenticationClient : IdentityV2AuthenticationClient
    {
        public RackspaceAuthenticationClient(IIdentityService identityService, AuthenticationRequest authenticationRequest)
            : base(identityService, authenticationRequest)
        {
        }

        protected override string GetEffectiveRegion(UserAccess userAccess, string region)
        {
            string effectiveRegion = base.GetEffectiveRegion(userAccess, region);
            if (effectiveRegion == null)
                effectiveRegion = userAccess.User.GetDefaultRegion();

            return effectiveRegion;
        }
    }
}
