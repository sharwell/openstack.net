namespace OpenStackNetTests.Live
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.ObjectModel;
    using OpenStack.Security.Authentication;
    using OpenStack.Services.ObjectStorage.V1;
    using OpenStack.Services.Orchestration.V1;
    using OpenStack.Threading;
    using Xunit;
    using ContainerName = OpenStack.Services.ObjectStorage.V1.ContainerName;
    using IObjectStorageService = OpenStack.Services.ObjectStorage.V1.IObjectStorageService;
    using Path = System.IO.Path;

    partial class OrchestrationTests
    {
        /// <summary>
        /// This prefix is used for stacks created by unit tests, to easily identify the stacks for safe cleanup.
        /// </summary>
        private const string TestStackPrefix = "UnitTestStack-";

        /// <summary>
        /// This unit removes all stacks which were created by these unit tests.
        /// </summary>
        [Fact]
        [Trait(TestCategories.Cleanup, "")]
        public async Task CleanupTestStacks()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                foreach (Stack stack in stacks)
                {
                    if (!stack.Name.Value.StartsWith(TestStackPrefix, StringComparison.Ordinal))
                        continue;

                    Console.WriteLine("Removing stack: {0}", stack.Name);
                    await service.RemoveStackAsync(stack.Name, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public void TestJsonSerialization()
        {
            TestResourceIdentifierBehavior<TemplateParameterName>();
            //var dictionary = new Dictionary<TemplateParameterName, TemplateParameterName> { { new TemplateParameterName("foo"), new TemplateParameterName("foo") } };
            //string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            //var deserialized = JsonConvert.DeserializeObject<Dictionary<TemplateParameterName, TemplateParameterName>>(json);
            //CollectionAssert.AreEquivalent(dictionary, deserialized);
        }

        public void TestResourceIdentifierBehavior<T>()
            where T : ResourceIdentifier<T>
        {
            // the basics
            T resource1a = JsonConvert.DeserializeObject<T>("\"Resource1\"");
            Assert.NotNull(resource1a);
            Assert.Equal("Resource1", resource1a.Value);

            T resource1b = JsonConvert.DeserializeObject<T>("\"Resource1\"");
            Assert.Equal(resource1a, resource1b);

            string string1a = JsonConvert.SerializeObject(resource1a);
            Assert.Equal("\"Resource1\"", string1a);

            string string1b = JsonConvert.SerializeObject(resource1b);
            Assert.Equal(string1a, string1b);

            // lists


            // other scenarios
            //var dictionary = new Dictionary<T, T> { { new T("foo"), new T("bar") } };
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListStacks()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    Console.WriteLine("  {0}", stack.Name);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetStackByNameAndId()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackApiCall singleStackCall = await service.PrepareGetStackAsync(stack.Name, stack.Id, cancellationToken);
                    var result = await singleStackCall.SendAsync(cancellationToken);
                    Stack singleStack = result.Item2.Stack;
                    Assert.NotNull(singleStack);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetStackByNameOnly()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackApiCall singleStackCall = await service.PrepareGetStackAsync(stack.Name, null, cancellationToken);
                    var result = await singleStackCall.SendAsync(cancellationToken);
                    Stack singleStack = result.Item2.Stack;
                    Assert.NotNull(singleStack);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetStackTemplateByNameAndId()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackTemplateApiCall stackTemplateCall = await service.PrepareGetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);
                    var result = await stackTemplateCall.SendAsync(cancellationToken);
                    StackTemplate stackTemplate = result.Item2;
                    Assert.NotNull(stackTemplate);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetStackTemplateByNameOnly()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackTemplateApiCall stackTemplateCall = await service.PrepareGetStackTemplateAsync(stack.Name, null, cancellationToken);
                    var result = await stackTemplateCall.SendAsync(cancellationToken);
                    StackTemplate stackTemplate = result.Item2;
                    Assert.NotNull(stackTemplate);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestSuspendResumeStack()
        {
            if (string.Equals("rackspace", Credentials.Vendor, StringComparison.OrdinalIgnoreCase))
                Assert.False(true, "Rackspace Cloud Orchestration does not support stack actions.");

            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                Stack stack = stacks.First();
                SuspendStackApiCall suspendCall = await service.PrepareSuspendStackAsync(stack.Name, stack.Id, new SuspendStackRequest(), cancellationToken);
                await suspendCall.SendAsync(cancellationToken);

                ResumeStackApiCall resumeCall = await service.PrepareResumeStackAsync(stack.Name, stack.Id, new ResumeStackRequest(), cancellationToken);
                await resumeCall.SendAsync(cancellationToken);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListResources()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                bool foundResources = false;
                foreach (var stack in stacks)
                {
                    var listResourcesCall = await service.PrepareListResourcesAsync(stack.Name, stack.Id, cancellationToken);
                    var resourcesResponse = await listResourcesCall.SendAsync(cancellationToken);
                    Assert.NotNull(resourcesResponse);
                    Assert.NotNull(resourcesResponse.Item2);

                    var resources = await resourcesResponse.Item2.GetAllPagesAsync(cancellationToken, null);
                    if (resources.Count == 0)
                        continue;

                    foundResources = true;
                    foreach (var resource in resources)
                    {
                    }
                }

                if (!foundResources)
                    Assert.False(true, "No resources were found for any stacks in the region.");
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListEvents()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                bool foundEvents = false;
                foreach (var stack in stacks)
                {
                    var listEventsCall = await service.PrepareListEventsAsync(stack.Name, stack.Id, cancellationToken);
                    var response = await listEventsCall.SendAsync(cancellationToken);
                    var events = await response.Item2.GetAllPagesAsync(cancellationToken, null);
                    Assert.NotNull(events);
                    if (events.Count == 0)
                        continue;

                    foundEvents = true;
                }

                if (!foundEvents)
                    Assert.False(true, "No events were found for any stacks in the region.");
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListResourceEvents()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.False(true, "The account does not have any stacks in the region.");

                bool foundEvents = false;
                foreach (var stack in stacks)
                {
                    var listResourcesCall = await service.PrepareListResourcesAsync(stack.Name, stack.Id, cancellationToken);
                    var resourcesResponse = await listResourcesCall.SendAsync(cancellationToken);
                    Assert.NotNull(resourcesResponse);
                    Assert.NotNull(resourcesResponse.Item2);

                    var resources = await resourcesResponse.Item2.GetAllPagesAsync(cancellationToken, null);
                    if (resources.Count == 0)
                        continue;

                    foreach (var resource in resources)
                    {
                        var listResourceEventsCall = await service.PrepareListResourceEventsAsync(stack.Name, stack.Id, resource.Name, cancellationToken);
                        var response = await listResourceEventsCall.SendAsync(cancellationToken);
                        var events = await response.Item2.GetAllPagesAsync(cancellationToken, null);
                        Assert.NotNull(events);
                        if (events.Count == 0)
                            continue;

                        foundEvents = true;
                    }
                }

                if (!foundEvents)
                    Assert.False(true, "No events were found for any stack resources in the region.");
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestValidateTemplateJson()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();

                ValidateTemplateRequest validateRequest = new ValidateTemplateRequest(JsonConvert.DeserializeObject<StackTemplate>(TestResources.ValidateStackTemplateJson));
                ValidateTemplateApiCall validateCall = await service.PrepareValidateTemplateAsync(validateRequest, cancellationToken);
                var validateResult = await validateCall.SendAsync(cancellationToken);
                Assert.NotNull(validateResult);
                TemplateValidationInformation validated = validateResult.Item2;
                Assert.NotNull(validated);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestValidateTemplateYaml()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();

                ValidateTemplateRequest validateRequest = new ValidateTemplateRequest(null, null, new JProperty("template", JValue.CreateString(TestResources.ValidateStackTemplateYaml)));
                ValidateTemplateApiCall validateCall = await service.PrepareValidateTemplateAsync(validateRequest, cancellationToken);
                var validateResult = await validateCall.SendAsync(cancellationToken);
                Assert.NotNull(validateResult);
                TemplateValidationInformation validated = validateResult.Item2;
                Assert.NotNull(validated);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListResourceTypes()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(100)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.False(true, "The account does not have any resource types in the region.");

                Console.WriteLine("Resource Types");
                foreach (ResourceTypeName resourceType in resourceTypes)
                    Console.WriteLine("  {0}", resourceType.Value);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetResourceTypeSchema()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.False(true, "The account does not have any resource types in the region.");

                foreach (ResourceTypeName resourceType in resourceTypes)
                {
                    GetResourceTypeSchemaApiCall apiCall = await service.PrepareGetResourceTypeSchemaAsync(resourceType, cancellationToken);
                    var response = await apiCall.SendAsync(cancellationToken);
                    ResourceTypeSchema schema = response.Item2;
                    Assert.NotNull(schema);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestGetResourceTypeTemplate()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.False(true, "The account does not have any resource types in the region.");

                foreach (ResourceTypeName resourceType in resourceTypes)
                {
                    GetResourceTypeTemplateApiCall apiCall = await service.PrepareGetResourceTypeTemplateAsync(resourceType, cancellationToken);
                    var response = await apiCall.SendAsync(cancellationToken);
                    ResourceTypeTemplate template = response.Item2;
                    Assert.NotNull(template);
                }
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestShowBuildInformation()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();

                GetBuildInformationApiCall apiCall = await service.PrepareGetBuildInformationAsync(cancellationToken);
                var response = await apiCall.SendAsync(cancellationToken);
                BuildInformation buildInformation = response.Item2;
                Assert.NotNull(buildInformation);
                Assert.NotNull(buildInformation.Api);
                Assert.NotNull(buildInformation.Api.Revision);
                Assert.NotNull(buildInformation.Engine);
                Assert.NotNull(buildInformation.Engine.Revision);

                Console.WriteLine("API Revision: {0}", buildInformation.Api.Revision);
                Console.WriteLine("Engine Revision: {0}", buildInformation.Engine.Revision);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestCreateEmptyStack()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = new StackTemplate(TemplateVersion.V2013_05_23, "Empty stack template", parameters: null, resources: null, outputs: null);
                TemplateEnvironment environment = null;
                JObject files = null;
                IDictionary<string, string> parameters = null;
                TimeSpan? timeout = null;
                bool? disableRollback = null;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                CreateStackApiCall apiCall = await service.PrepareCreateStackAsync(requestData, cancellationToken);
                var response = await apiCall.SendAsync(cancellationToken);
                Assert.NotNull(response);
                Assert.NotNull(response.Item2);
                Stack stack = response.Item2.Stack;

                RemoveStackApiCall removeApiCall = await service.PrepareRemoveStackAsync(stackName, stack.Id, cancellationToken);
                await removeApiCall.SendAsync(cancellationToken);
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestCreateContainerStack()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateContainerStackTemplate);
                IDictionary<string, JToken> environmentParameters = new Dictionary<string, JToken>
                    {
                        { "name", JToken.FromObject(containerName) }
                    };
                ResourceRegistry resourceRegistry = null;
                TemplateEnvironment environment = new TemplateEnvironment(environmentParameters, resourceRegistry);
                JObject files = null;
                IDictionary<string, string> parameters = null;
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                await service.GetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                await service.RemoveStackAsync(stackName, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestCreateContainerStackListResources()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateContainerStackTemplate);
                IDictionary<string, JToken> environmentParameters = new Dictionary<string, JToken>
                    {
                        { "name", JToken.FromObject(containerName) }
                    };
                ResourceRegistry resourceRegistry = null;
                TemplateEnvironment environment = new TemplateEnvironment(environmentParameters, resourceRegistry);
                JObject files = null;
                IDictionary<string, string> parameters = null;
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                ReadOnlyCollection<Resource> resources = await (await service.ListResourcesAsync(stack.Name, stack.Id, cancellationToken)).GetAllPagesAsync(cancellationToken, null);
                Assert.Equal(1, resources.Count);
                Resource containerResource = resources[0];
                Assert.Equal(new ResourceTypeName("OS::Swift::Container"), containerResource.Type);
                await service.GetResourceMetadataAsync(stack.Name, stack.Id, containerResource.Name, cancellationToken);

                await service.GetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                await service.RemoveStackAsync(stackName, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestCreateCustomResourceTemplate()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ResourceTypeTemplate containerResourceTemplate = await service.GetResourceTypeTemplateAsync(new ResourceTypeName("OS::Swift::Container"), cancellationToken);
                string resourceFileName = "file:///swift-container.template";

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateCustomContainerStackTemplate);
                IDictionary<string, JToken> environmentParameters = new Dictionary<string, JToken>
                    {
                        { "name", JToken.FromObject(containerName) }
                    };
                ResourceRegistry resourceRegistry = JsonConvert.DeserializeObject<ResourceRegistry>("{\"My::Container\":\"" + resourceFileName + "\"}");
                TemplateEnvironment environment = new TemplateEnvironment(environmentParameters, resourceRegistry);
                JObject files = new JObject(
                    new JProperty(resourceFileName, JValue.CreateString(JsonConvert.SerializeObject(containerResourceTemplate))));
                IDictionary<string, string> parameters = null;
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                await service.GetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                await service.RemoveStackAsync(stackName, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestCreateCustomResourceTemplateNewVersion()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                string resourceFileName = "file:///swift-container.template";

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateCustomContainerStackTemplate);
                IDictionary<string, JToken> environmentParameters = new Dictionary<string, JToken>
                    {
                        { "name", JToken.FromObject(containerName) }
                    };
                ResourceRegistry resourceRegistry = JsonConvert.DeserializeObject<ResourceRegistry>("{\"My::Container\":\"" + resourceFileName + "\"}");
                TemplateEnvironment environment = new TemplateEnvironment(environmentParameters, resourceRegistry);
                JObject files = new JObject(
                    new JProperty(resourceFileName, JValue.CreateString(TestResources.CreateContainerNewCustomResourceTemplate)));
                IDictionary<string, string> parameters = null;
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                await service.GetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                await service.RemoveStackAsync(stackName, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestAbandonAdoptStack()
        {
            /* 1. Create a stack with a single Object Storage container as a resource
             * 2. Abandon the stack
             * 3. Adopt the stack from existing resources, using the result of abandon stack
             * 4. Remove the stack
             */
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());
                ContainerName anotherContainerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateContainerStackTemplate);
                TemplateEnvironment environment = null;
                JObject files = null;
                IDictionary<string, string> parameters = new Dictionary<string, string>
                    {
                        { "name", containerName.Value }
                    };
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                Stack abandonedStack = await service.AbandonStackAsync(stackName, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                StackName adoptedStackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                IDictionary<string, string> adoptedParameters = new Dictionary<string, string>
                {
                    { "name", anotherContainerName.Value }
                };
                AdoptStackData adoptRequestData = new AdoptStackData(adoptedStackName, JsonConvert.SerializeObject(abandonedStack), templateUri, template, environment, files, adoptedParameters, timeout, disableRollback);
                Stack adoptedStack = await service.AdoptStackAsync(adoptRequestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                await service.RemoveStackAsync(adoptedStackName, adoptedStack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestAbandonAdoptStackAutomatic()
        {
            /* 1. Create a stack with a single Object Storage container as a resource
             * 2. Abandon the stack
             * 3. Adopt the stack from existing resources, using the result of abandon stack
             * 4. Remove the stack
             */
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());
                ContainerName anotherContainerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateContainerStackTemplate);
                TemplateEnvironment environment = null;
                JObject files = null;
                IDictionary<string, string> parameters = new Dictionary<string, string>
                    {
                        { "name", containerName.Value }
                    };
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                Stack abandonedStack = await service.AbandonStackAsync(stackName, stack.Id, cancellationToken);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                StackName adoptedStackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                IDictionary<string, string> adoptedParameters = new Dictionary<string, string>
                {
                    { "name", anotherContainerName.Value }
                };
                AdoptStackData adoptRequestData = new AdoptStackData(adoptedStackName, abandonedStack);
                Stack adoptedStack = await service.AdoptStackAsync(adoptRequestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));

                await service.RemoveStackAsync(adoptedStackName, adoptedStack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
                Assert.False(await ContainerExistsAsync(objectStorageService, anotherContainerName, cancellationToken));
            }
        }

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestUpdateContainerStack()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(60)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
                IOrchestrationService service = CreateService(authenticationService, Credentials);
                IObjectStorageService objectStorageService = ObjectStorageTests.CreateService(authenticationService, Credentials);

                ContainerName containerName = new ContainerName(ObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName());
                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                StackName stackName = new StackName(TestStackPrefix + Path.GetRandomFileName());
                Uri templateUri = null;
                StackTemplate template = JsonConvert.DeserializeObject<StackTemplate>(TestResources.CreateContainerStackTemplate);
                TemplateEnvironment environment = null;
                JObject files = null;
                IDictionary<string, string> parameters = new Dictionary<string, string>
                    {
                        { "name", containerName.Value }
                    };
                TimeSpan? timeout = null;
                bool? disableRollback = false;
                StackData requestData = new StackData(stackName, templateUri, template, environment, files, parameters, timeout, disableRollback);

                Stack stack = await service.CreateStackAsync(requestData, AsyncCompletionOption.RequestCompleted, cancellationToken, null);
                Assert.NotNull(stack);

                Assert.True(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));

                await service.RemoveStackAsync(stackName, stack.Id, AsyncCompletionOption.RequestCompleted, cancellationToken, null);

                Assert.False(await ContainerExistsAsync(objectStorageService, containerName, cancellationToken));
            }
        }

#if false
        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListSoftwareConfigurations()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<SoftwareConfiguration> softwareConfigurations = await ListAllSoftwareConfigurationsAsync(service, cancellationToken);
                if (!softwareConfigurations.Any())
                    Assert.False(true, "The account does not have any software configurations in the region.");

                Console.WriteLine("Software Configurations");
                foreach (SoftwareConfiguration softwareConfiguration in softwareConfigurations)
                {
                    Console.WriteLine("  {0}", softwareConfiguration.Name);
                }
            }
        }
#endif

        [Fact]
        [Trait(TestCategories.User, "")]
        [Trait(TestCategories.Orchestration, "")]
        public async Task TestListDeployments()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Deployment> deployments = await ListAllDeploymentsAsync(service, cancellationToken);
                if (!deployments.Any())
                    Assert.False(true, "The account does not have any deployments in the region.");

                Console.WriteLine("Deployments");
                foreach (Deployment deployment in deployments)
                {
                    //Console.WriteLine("  {0}", deployment.Name);
                }
            }
        }

        protected static async Task<bool> ContainerExistsAsync(IObjectStorageService service, ContainerName containerName, CancellationToken cancellationToken)
        {
            try
            {
                GetContainerMetadataApiCall apiCall = await service.PrepareGetContainerMetadataAsync(containerName, cancellationToken).WithNewest();
                await apiCall.SendAsync(cancellationToken);
                return true;
            }
            catch (HttpWebException ex)
            {
                if (ex.ResponseMessage == null || ex.ResponseMessage.StatusCode != HttpStatusCode.NotFound)
                    throw;

                return false;
            }
        }

        protected static async Task<ReadOnlyCollection<Stack>> ListAllStacksAsync(IOrchestrationService service, CancellationToken cancellationToken)
        {
            ListStacksApiCall apiCall = await service.PrepareListStacksAsync(cancellationToken);
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Stack>> response = await apiCall.SendAsync(cancellationToken);
            return await response.Item2.GetAllPagesAsync(cancellationToken, null);
        }

        protected static async Task<ReadOnlyCollection<ResourceTypeName>> ListAllResourceTypesAsync(IOrchestrationService service, CancellationToken cancellationToken)
        {
            ListResourceTypesApiCall apiCall = await service.PrepareListResourceTypesAsync(cancellationToken);
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<ResourceTypeName>> response = await apiCall.SendAsync(cancellationToken);
            return await response.Item2.GetAllPagesAsync(cancellationToken, null);
        }

#if false
        protected static async Task<ReadOnlyCollection<SoftwareConfiguration>> ListAllSoftwareConfigurationsAsync(IOrchestrationService service, CancellationToken cancellationToken)
        {
            ListSoftwareConfigurationsApiCall apiCall = await service.PrepareListSoftwareConfigurationsAsync(cancellationToken);
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<SoftwareConfiguration>> response = await apiCall.SendAsync(cancellationToken);
            return await response.Item2.GetAllPagesAsync(cancellationToken, null);
        }
#endif

        protected static async Task<ReadOnlyCollection<Deployment>> ListAllDeploymentsAsync(IOrchestrationService service, CancellationToken cancellationToken)
        {
            ListDeploymentsApiCall apiCall = await service.PrepareListDeploymentsAsync(cancellationToken);
            Tuple<HttpResponseMessage, ReadOnlyCollectionPage<Deployment>> response = await apiCall.SendAsync(cancellationToken);
            return await response.Item2.GetAllPagesAsync(cancellationToken, null);
        }

        protected TimeSpan TestTimeout(TimeSpan timeSpan)
        {
            if (Debugger.IsAttached)
                return TimeSpan.FromDays(6);

            return timeSpan;
        }

        private IOrchestrationService CreateService()
        {
            IAuthenticationService authenticationService = IdentityV2Tests.CreateAuthenticationService(Credentials);
            return CreateService(authenticationService, Credentials);
        }

        internal static IOrchestrationService CreateService(IAuthenticationService authenticationService, TestCredentials credentials)
        {
            OrchestrationClient client;
            switch (credentials.Vendor)
            {
            case "HP":
                // currently HP does not have a vendor-specific IOrchestrationService
                goto default;

            case "Rackspace":
                // currently Rackspace does not have a vendor-specific IOrchestrationService
                goto default;

            case "OpenStack":
            default:
                client = new OrchestrationClient(authenticationService, credentials.DefaultRegion);
                break;
            }

            TestProxy.ConfigureService(client, credentials.Proxy);
            client.BeforeAsyncWebRequest += TestHelpers.HandleBeforeAsyncWebRequest;
            client.AfterAsyncWebResponse += TestHelpers.HandleAfterAsyncWebResponse;

            return client;
        }
    }
}
