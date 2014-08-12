namespace Hp.Security.Authentication
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Services.Identity;
    using OpenStack.Services.Identity.V3;

    public static class HpAuthentication
    {
        public static AuthenticateRequest Password(UserId userId, string password)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("id", userId),
                            new JProperty("password", password))))));
            AuthenticationScope scope = null;

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest Password(UserId userId, string password, string domainName)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("id", userId),
                            new JProperty("password", password),
                            new JProperty("domain", new JObject(
                                new JProperty("name", domainName))))))));
            AuthenticationScope scope = null;

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest Password(string username, string password, DomainId domainId)
        {
            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("username cannot be empty");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("name", username),
                            new JProperty("password", password),
                            new JProperty("domain", new JObject(
                                new JProperty("id", domainId.Value))))))));
            AuthenticationScope scope = null;

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest ScopedPassword(string username, string password, ProjectId projectId)
        {
            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("username cannot be empty");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("name", username),
                            new JProperty("password", password))))));
            AuthenticationScope scope =
                new AuthenticationScope(
                    new AuthenticationScope.ProjectData(projectId));

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest ScopedPassword(UserId userId, string password, string projectName, DomainId domainId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("id", userId),
                            new JProperty("password", password))))));
            AuthenticationScope scope =
                new AuthenticationScope(
                    new AuthenticationScope.ProjectData(
                        projectName,
                        new AuthenticationScope.DomainData(domainId)));

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest ScopedPassword(UserId userId, string password, string domainName)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("id", userId),
                            new JProperty("password", password))))));
            AuthenticationScope scope =
                new AuthenticationScope(
                    new AuthenticationScope.DomainData(
                        null,
                        new JProperty("name", domainName)));

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest ScopedPassword(UserId userId, string password, DomainId domainId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == null)
                throw new ArgumentNullException("password");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { AuthenticationMethod.Password },
                    new JProperty("password", new JObject(
                        new JProperty("user", new JObject(
                            new JProperty("id", userId),
                            new JProperty("password", password))))));
            AuthenticationScope scope =
                new AuthenticationScope(
                    new AuthenticationScope.DomainData(domainId));

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }

        public static AuthenticateRequest ScopedAccessKey(string accessKey, string secretKey, ProjectId projectId)
        {
            if (accessKey == null)
                throw new ArgumentNullException("accessKey");
            if (secretKey == null)
                throw new ArgumentNullException("secretKey");
            if (string.IsNullOrEmpty(accessKey))
                throw new ArgumentException("accessKey cannot be empty");
            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentException("secretKey cannot be empty");

            IdentityData identityData =
                new IdentityData(
                    new AuthenticationMethod[] { HpAuthenticationMethod.AccessKey },
                    new JProperty("accessKey", new JObject(
                        new JProperty("accessKey", accessKey),
                        new JProperty("secretKey", secretKey))));
            AuthenticationScope scope =
                new AuthenticationScope(
                    new AuthenticationScope.ProjectData(projectId));

            AuthenticateRequest authenticateRequest =
                new AuthenticateRequest(
                    new AuthenticateData(identityData, scope));

            return authenticateRequest;
        }
    }
}
