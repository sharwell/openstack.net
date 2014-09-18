namespace OpenStackNetTests.Live
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.Collections;
    using OpenStack.Security.Authentication;
    using OpenStack.Services.Orchestration.V1;
    using Path = System.IO.Path;

    partial class OrchestrationTests
    {
        /// <summary>
        /// This prefix is used for stacks created by unit tests, to easily identify the stacks for safe cleanup.
        /// </summary>
        private const string TestStackPrefix = "UnitTestStack-";

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestListStacks()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    Console.WriteLine("  {0}", stack.Name);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetStackByNameAndId()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackApiCall singleStackCall = await service.PrepareGetStackAsync(stack.Name, stack.Id, cancellationToken);
                    var result = await singleStackCall.SendAsync(cancellationToken);
                    Stack singleStack = result.Item2.Stack;
                    Assert.IsNotNull(singleStack);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetStackByNameOnly()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackApiCall singleStackCall = await service.PrepareGetStackAsync(stack.Name, null, cancellationToken);
                    var result = await singleStackCall.SendAsync(cancellationToken);
                    Stack singleStack = result.Item2.Stack;
                    Assert.IsNotNull(singleStack);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetStackTemplateByNameAndId()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackTemplateApiCall stackTemplateCall = await service.PrepareGetStackTemplateAsync(stack.Name, stack.Id, cancellationToken);
                    var result = await stackTemplateCall.SendAsync(cancellationToken);
                    StackTemplate stackTemplate = result.Item2;
                    Assert.IsNotNull(stackTemplate);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetStackTemplateByNameOnly()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Console.WriteLine("Stacks");
                foreach (Stack stack in stacks)
                {
                    GetStackTemplateApiCall stackTemplateCall = await service.PrepareGetStackTemplateAsync(stack.Name, null, cancellationToken);
                    var result = await stackTemplateCall.SendAsync(cancellationToken);
                    StackTemplate stackTemplate = result.Item2;
                    Assert.IsNotNull(stackTemplate);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestSuspendResumeStack()
        {
            if (string.Equals("rackspace", Credentials.Vendor, StringComparison.OrdinalIgnoreCase))
                Assert.Inconclusive("Rackspace Cloud Orchestration does not support stack actions.");

            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                Stack stack = stacks.First();
                SuspendStackApiCall suspendCall = await service.PrepareSuspendStackAsync(stack.Name, stack.Id, new SuspendStackRequest(), cancellationToken);
                await suspendCall.SendAsync(cancellationToken);

                ResumeStackApiCall resumeCall = await service.PrepareResumeStackAsync(stack.Name, stack.Id, new ResumeStackRequest(), cancellationToken);
                await resumeCall.SendAsync(cancellationToken);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestListResources()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                bool foundResources = false;
                foreach (var stack in stacks)
                {
                    var listResourcesCall = await service.PrepareListResourcesAsync(stack.Name, stack.Id, cancellationToken);
                    var resourcesResponse = await listResourcesCall.SendAsync(cancellationToken);
                    Assert.IsNotNull(resourcesResponse);
                    Assert.IsNotNull(resourcesResponse.Item2);

                    var resources = await resourcesResponse.Item2.GetAllPagesAsync(cancellationToken, null);
                    if (resources.Count == 0)
                        continue;

                    foundResources = true;
                    foreach (var resource in resources)
                    {
                    }
                }

                if (!foundResources)
                    Assert.Inconclusive("No resources were found for any stacks in the region.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestListEvents()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                bool foundEvents = false;
                foreach (var stack in stacks)
                {
                    var listEventsCall = await service.PrepareListEventsAsync(stack.Name, stack.Id, cancellationToken);
                    var response = await listEventsCall.SendAsync(cancellationToken);
                    var events = await response.Item2.GetAllPagesAsync(cancellationToken, null);
                    Assert.IsNotNull(events);
                    if (events.Count == 0)
                        continue;

                    foundEvents = true;
                }

                if (!foundEvents)
                    Assert.Inconclusive("No events were found for any stacks in the region.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestListResourceEvents()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(10)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<Stack> stacks = await ListAllStacksAsync(service, cancellationToken);
                if (!stacks.Any())
                    Assert.Inconclusive("The account does not have any stacks in the region.");

                bool foundEvents = false;
                foreach (var stack in stacks)
                {
                    var listResourcesCall = await service.PrepareListResourcesAsync(stack.Name, stack.Id, cancellationToken);
                    var resourcesResponse = await listResourcesCall.SendAsync(cancellationToken);
                    Assert.IsNotNull(resourcesResponse);
                    Assert.IsNotNull(resourcesResponse.Item2);

                    var resources = await resourcesResponse.Item2.GetAllPagesAsync(cancellationToken, null);
                    if (resources.Count == 0)
                        continue;

                    foreach (var resource in resources)
                    {
                        var listResourceEventsCall = await service.PrepareListResourceEventsAsync(stack.Name, stack.Id, resource.Name, cancellationToken);
                        var response = await listResourceEventsCall.SendAsync(cancellationToken);
                        var events = await response.Item2.GetAllPagesAsync(cancellationToken, null);
                        Assert.IsNotNull(events);
                        if (events.Count == 0)
                            continue;

                        foundEvents = true;
                    }
                }

                if (!foundEvents)
                    Assert.Inconclusive("No events were found for any stack resources in the region.");
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
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
                Assert.IsNotNull(validateResult);
                TemplateValidationInformation validated = validateResult.Item2;
                Assert.IsNotNull(validated);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
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
                Assert.IsNotNull(validateResult);
                TemplateValidationInformation validated = validateResult.Item2;
                Assert.IsNotNull(validated);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestListResourceTypes()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(100)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.Inconclusive("The account does not have any resource types in the region.");

                Console.WriteLine("Resource Types");
                foreach (ResourceTypeName resourceType in resourceTypes)
                    Console.WriteLine("  {0}", resourceType.Value);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetResourceTypeSchema()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.Inconclusive("The account does not have any resource types in the region.");

                foreach (ResourceTypeName resourceType in resourceTypes)
                {
                    GetResourceTypeSchemaApiCall apiCall = await service.PrepareGetResourceTypeSchemaAsync(resourceType, cancellationToken);
                    var response = await apiCall.SendAsync(cancellationToken);
                    ResourceTypeSchema schema = response.Item2;
                    Assert.IsNotNull(schema);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
        public async Task TestGetResourceTypeTemplate()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(TestTimeout(TimeSpan.FromSeconds(30)));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                IOrchestrationService service = CreateService();
                ReadOnlyCollection<ResourceTypeName> resourceTypes = await ListAllResourceTypesAsync(service, cancellationToken);
                if (!resourceTypes.Any())
                    Assert.Inconclusive("The account does not have any resource types in the region.");

                foreach (ResourceTypeName resourceType in resourceTypes)
                {
                    GetResourceTypeTemplateApiCall apiCall = await service.PrepareGetResourceTypeTemplateAsync(resourceType, cancellationToken);
                    var response = await apiCall.SendAsync(cancellationToken);
                    ResourceTypeTemplate template = response.Item2;
                    Assert.IsNotNull(template);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
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
                Assert.IsNotNull(buildInformation);
                Assert.IsNotNull(buildInformation.Api);
                Assert.IsNotNull(buildInformation.Api.Revision);
                Assert.IsNotNull(buildInformation.Engine);
                Assert.IsNotNull(buildInformation.Engine.Revision);

                Console.WriteLine("API Revision: {0}", buildInformation.Api.Revision);
                Console.WriteLine("Engine Revision: {0}", buildInformation.Engine.Revision);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Orchestration)]
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
                Assert.IsNotNull(response);
                Assert.IsNotNull(response.Item2);
                Stack stack = response.Item2.Stack;

                RemoveStackApiCall removeApiCall = await service.PrepareRemoveStackAsync(stackName, stack.Id, cancellationToken);
                await removeApiCall.SendAsync(cancellationToken);
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
