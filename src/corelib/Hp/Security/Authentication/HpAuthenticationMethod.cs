namespace Hp.Security.Authentication
{
    using OpenStack.Services.Identity.V3;

    public static class HpAuthenticationMethod
    {
        public static readonly AuthenticationMethod AccessKey = AuthenticationMethod.FromName("accessKey");
    }
}
