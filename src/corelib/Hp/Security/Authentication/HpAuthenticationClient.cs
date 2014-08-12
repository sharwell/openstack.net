namespace Hp.Security.Authentication
{
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Identity.V3;


    /// <summary>
    /// This class extends the OpenStack <see cref="IdentityV3AuthenticationClient"/> with specific
    /// support for HP Cloud Identity.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class HpAuthenticationClient : IdentityV3AuthenticationClient
    {
        public HpAuthenticationClient(IIdentityService identityService, AuthenticateRequest authenticateRequest)
            : base(identityService, authenticateRequest)
        {
        }
    }
}
