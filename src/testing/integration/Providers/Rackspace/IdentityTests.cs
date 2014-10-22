using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.openstack.Core.Domain;
using net.openstack.Core.Providers;
using net.openstack.Providers.Rackspace;
using net.openstack.Providers.Rackspace.Objects;

namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    [TestClass]
    public class IdentityTests
    {
        private TestContext testContextInstance;
        private static ExtendedRackspaceCloudIdentity _testIdentity;
        private static ExtendedRackspaceCloudIdentity _testAdminIdentity;
        private static User _userDetails;
        private static User _adminUserDetails;
        private static User _testUser;
        private static string _newTestUserPassword;
        private const string NewUserPassword = "My_n3wuser2_p@$$ssw0rd";

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _testIdentity = new ExtendedRackspaceCloudIdentity(Bootstrapper.Settings.TestIdentity);
            _testAdminIdentity = new ExtendedRackspaceCloudIdentity(Bootstrapper.Settings.TestAdminIdentity);
           
        }

        [Fact]
        public void Should_Authenticate_Test_Identity()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);
            var userAccess = serviceProvider.Authenticate();

            Assert.NotNull(userAccess);
        }

        [Fact]
        public void Should_Authenticate_Test_Admin_Identity()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);
            var userAccess = serviceProvider.Authenticate();

            Assert.NotNull(userAccess);
        }

        [Fact]
        public void Should_Throw_Error_When_Authenticating_With_Invalid_Password()
        {
            var identity = new RackspaceCloudIdentity()
                               {
                                   Username = _testIdentity.Username,
                                   Password = "bad password",
                                   Domain = _testIdentity.Domain,
                               };
            IIdentityProvider serviceProvider = new CloudIdentityProvider(identity);

            try
            {
                var userAccess = serviceProvider.Authenticate();

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch (net.openstack.Core.Exceptions.Response.ResponseException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_Throw_Error_When_Authenticating_With_Invalid_API_Key()
        {
            var identity = new RackspaceCloudIdentity()
                               {
                                   Username = _testIdentity.Username,
                                   APIKey = "bad api key",
                                   Domain = _testIdentity.Domain,
                               };
            IIdentityProvider serviceProvider = new CloudIdentityProvider(identity);

            try
            {
                var userAccess = serviceProvider.Authenticate();

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch(net.openstack.Core.Exceptions.Response.ResponseException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_Throw_Error_When_Authenticating_With_Invalid_Username()
        {
            var identity = new RackspaceCloudIdentity()
            {
                Username = "I'm a bad bad user",
                APIKey = "bad api key"
            };
            IIdentityProvider serviceProvider = new CloudIdentityProvider(identity);

            try
            {
                var userAccess = serviceProvider.Authenticate();

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch (net.openstack.Core.Exceptions.Response.ResponseException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_List_Only_User_In_Account_When_Retrieving_List_Of_Users_With_User_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);

            var users = serviceProvider.ListUsers();

            Assert.True(users.Any());
            Assert.Equal(_testIdentity.Username, users.First().Username);
        }

        [Fact]
        public void Should_List_Multiple_Users_When_Retrieving_List_Of_Users_With_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);

            var users = serviceProvider.ListUsers();

            Assert.True(users.Any());
        }

        [Fact]
        public void Should_List_Details_Of_Self_When_Retrieving_User_By_Name_With_Non_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);

            _userDetails = serviceProvider.GetUserByName(_testIdentity.Username);

            Assert.NotNull(_userDetails);
            Assert.Equal(_testIdentity.Username, _userDetails.Username);
        }

        [Fact]
        public void Should_List_Details_Of_Self_When_Retrieving_User_By_Name_With_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);

            _adminUserDetails = serviceProvider.GetUserByName(_testAdminIdentity.Username);

            Assert.NotNull(_adminUserDetails);
            Assert.Equal(_testAdminIdentity.Username, _adminUserDetails.Username);
        }

        [Fact]
        public void Should_List_Details_Of_Other_User_When_Retrieving_User_By_Name_With_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);

            var details = serviceProvider.GetUserByName(_testIdentity.Username);

            Assert.NotNull(details);
            Assert.Equal(_testIdentity.Username, details.Username);
        }

        [Fact]
        public void Should_Throw_Exception_When_Trying_To_Get_Details_Of_A_Different_User_When_Retrieving_User_By_Name_With_Non_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);

            try
            {
                var details = serviceProvider.GetUserByName(_testAdminIdentity.Username);

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch (net.openstack.Core.Exceptions.Response.ResponseException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_List_Details_Of_Self_When_Retrieving_User_By_Id_With_Non_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);

            _userDetails = serviceProvider.GetUser(_userDetails.Id);

            Assert.NotNull(_userDetails);
            Assert.Equal(_testIdentity.Username, _userDetails.Username);
        }

        [Fact]
        public void Should_List_Details_Of_Self_When_Retrieving_User_By_Id_With_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);

            _adminUserDetails = serviceProvider.GetUser(_adminUserDetails.Id);

            Assert.NotNull(_adminUserDetails);
            Assert.Equal(_testAdminIdentity.Username, _adminUserDetails.Username);
        }

        [Fact]
        public void Should_List_Details_Of_Other_User_When_Retrieving_User_By_Id_With_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testAdminIdentity);

            var details = serviceProvider.GetUser(_userDetails.Id);

            Assert.NotNull(details);
            Assert.Equal(_testIdentity.Username, details.Username);
        }

        [Fact]
        public void Should_Throw_Exception_When_Trying_To_Get_Details_Of_A_Different_User_When_Retrieving_User_By_Id_With_Non_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(_testIdentity);

            try
            {
                var details = serviceProvider.GetUser(_adminUserDetails.Id);

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch (net.openstack.Core.Exceptions.Response.ResponseException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_Add_New_User_1_Without_Specifying_A_Password_Or_Default_Region_To_Account_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var newTestUser = provider.AddUser(new NewUser("openstacknettestuser1", "newuser@me.com", enabled : true));

            _newTestUserPassword = newTestUser.Password;
            Assert.NotNull(newTestUser);
            Assert.Equal("openstacknettestuser1", newTestUser.Username);
            Assert.Equal("newuser@me.com", newTestUser.Email);
            Assert.Equal(true, newTestUser.Enabled);
            Assert.False(string.IsNullOrWhiteSpace(newTestUser.Password));
        }

        [Fact]
        public void Should_Retrieve_New_User_1_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testAdminIdentity);

            _testUser = provider.GetUserByName("openstacknettestuser1");

            Assert.NotNull(_testUser);
            Assert.Equal("openstacknettestuser1", _testUser.Username);
            Assert.Equal("newuser@me.com", _testUser.Email);
            Assert.Equal(true, _testUser.Enabled);
        }

        [Fact]
        public void Should_Authenticate_NewUser()
        {
            Assert.NotNull(_testUser);

            IIdentityProvider provider = new CloudIdentityProvider();

            var userAccess =
                provider.Authenticate(new RackspaceCloudIdentity
                                          {Username = _testUser.Username, Password = _newTestUserPassword});

            Assert.NotNull(userAccess);
        }

        [Fact]
        public void Should_Update_NewUser_Username_And_Email_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            User user = provider.GetUser(_testUser.Id);
            user.Username = "openstacknettestuser12";
            user.Email = "newuser2@me.com";
            user.Enabled = true;
            var updatedUser = provider.UpdateUser(user);

            Assert.NotNull(updatedUser);
            Assert.Equal("openstacknettestuser12", updatedUser.Username);
            Assert.Equal("newuser2@me.com", updatedUser.Email);
            Assert.Equal(true, updatedUser.Enabled);
            Assert.True(string.IsNullOrWhiteSpace(updatedUser.DefaultRegion));
        }

        [Fact]
        public void Should_Delete_NewUser_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var response = provider.DeleteUser(_testUser.Id);

            Assert.True(response);
        }

        [Fact]
        public void Should_Throw_Exception_When_Requesting_The_NewUser_After_It_Has_Been_Deleted_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            try
            {
                provider.GetUser(_testUser.Id);

                throw new Exception("This code path is invalid, exception was expected.");
            }
            catch(Exception )
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Should_Add_New_User_2_With_Specifying_A_Password_But_Not_Default_Region_To_Account_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var newUser = provider.AddUser(new NewUser("openstacknettestuser2", "newuser2@me.com", enabled : true, password : NewUserPassword));
            _newTestUserPassword = newUser.Password;

            Assert.NotNull(newUser);
            Assert.Equal("openstacknettestuser2", newUser.Username);
            Assert.Equal("newuser2@me.com", newUser.Email);
            Assert.Equal(true, newUser.Enabled);
            Assert.Equal(NewUserPassword, newUser.Password);
            Assert.False(string.IsNullOrWhiteSpace(newUser.Password));
        }

        [Fact]
        public void Should_Retrieve_New_User_2_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testAdminIdentity);

            _testUser = provider.GetUserByName("openstacknettestuser2");

            Assert.NotNull(_testUser);
            Assert.Equal("openstacknettestuser2", _testUser.Username);
            Assert.Equal("newuser2@me.com", _testUser.Email);
            Assert.Equal(true, _testUser.Enabled);
        }

        [Fact]
        public void Should_Update_NewUser_Username_And_Email_And_Default_Region_When_Requesting_As_User_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            User user = provider.GetUser(_testUser.Id);
            user.Username = "openstacknettestuser32";
            user.Email = "newuser32@me.com";
            user.Enabled = true;
            user.DefaultRegion = "DFW";
            var updatedUser = provider.UpdateUser(user);

            Assert.NotNull(updatedUser);
            Assert.Equal("openstacknettestuser32", updatedUser.Username);
            Assert.Equal("newuser32@me.com", updatedUser.Email);
            Assert.Equal(true, updatedUser.Enabled);
            Assert.Equal("DFW", updatedUser.DefaultRegion);
        }

        [Fact]
        public void Should_Get_NewUser_When_Requesting_As_Self()
        {
            IIdentityProvider provider = new CloudIdentityProvider(new RackspaceCloudIdentity { Username = _testUser.Username, Password = _newTestUserPassword });

            var user = provider.GetUser(_testUser.Id);

            Assert.NotNull(user);
        }

        [Fact]
        public void Should_Update_NewUser_Username_And_Email_When_Requesting_As_Self()
        {
            IIdentityProvider provider = new CloudIdentityProvider(new RackspaceCloudIdentity { Username = _testUser.Username, Password = _newTestUserPassword });

            User user = provider.GetUser(_testUser.Id);
            user.Username = "openstacknettestuser42";
            user.Email = "newuser42@me.com";
            user.Enabled = true;
            var updatedUser = provider.UpdateUser(user);

            Assert.NotNull(updatedUser);
            Assert.Equal("openstacknettestuser42", updatedUser.Username);
            Assert.Equal("newuser42@me.com", updatedUser.Email);
            Assert.Equal(true, updatedUser.Enabled);
        }

        [Fact]
        public void Should_List_Only_Self_When_Retrieving_List_Of_Users_With_Non_Admin_Account()
        {
            IIdentityProvider serviceProvider = new CloudIdentityProvider(new RackspaceCloudIdentity { Username = _testUser.Username, Password = _newTestUserPassword });

            var users = serviceProvider.ListUsers();

            Assert.True(users.Count() == 1);
            Assert.Equal(_testUser.Username, users.First().Username);
        }

        [Fact]
        public void Should_Return_The_Users_Tenant_When_Requesting_As_Non_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var tenants = provider.ListTenants();

            Assert.True(tenants.Any());

            if(!string.IsNullOrWhiteSpace(_testIdentity.TenantId))
            {
                Assert.True(tenants.Any(t => t.Id == _testIdentity.TenantId));
            }
        }

        [Fact]
        public void Should_Return_List_Of_Users_Credentials_When_Requesting_As_Non_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var creds = provider.ListUserCredentials(_userDetails.Id);

            Assert.NotNull(creds);
            Assert.True(creds.Any());

            foreach (var cred in creds)
            {
                Assert.False(string.IsNullOrWhiteSpace(cred.Name));
                Assert.False(string.IsNullOrWhiteSpace(cred.APIKey));
                Assert.False(string.IsNullOrWhiteSpace(cred.Username));
            }
        }

        [Fact]
        public void Should_Return_User_API_Credential_When_Requesting_As_Non_Admin()
        {
            IIdentityProvider provider = new CloudIdentityProvider(_testIdentity);

            var cred = provider.GetUserCredential(_userDetails.Id, "RAX-KSKEY:apiKeyCredentials");

            Assert.NotNull(cred);
            Assert.Equal("RAX-KSKEY:apiKeyCredentials", cred.Name);
            Assert.False(string.IsNullOrWhiteSpace(cred.APIKey));
            Assert.False(string.IsNullOrWhiteSpace(cred.Username));
        }
    }
}
