namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using net.openstack.Core;
    using net.openstack.Core.Providers;
    using net.openstack.Core.Schema;
    using net.openstack.Providers.Rackspace;
    using net.openstack.Providers.Rackspace.Objects.Images;
    using Newtonsoft.Json;
    using CloudIdentity = net.openstack.Core.Domain.CloudIdentity;
    using IIdentityProvider = net.openstack.Core.Providers.IIdentityProvider;
    using ObjectStore = net.openstack.Core.Domain.ObjectStore;
    using Path = System.IO.Path;
    using ProjectId = net.openstack.Core.Domain.ProjectId;

    [TestClass]
    public class UserImagesTests
    {
        /// <summary>
        /// This prefix is used for images created by unit tests, to avoid
        /// overwriting images created by other applications.
        /// </summary>
        internal const string TestImagePrefix = "UnitTestImage-";

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImages()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                Console.WriteLine("Images");
                foreach (Image image in images.OrderBy(i => i.Size))
                {
                    Console.WriteLine("    {0} ({1})", image.Name, image.Id);
                    Console.WriteLine("        Size: {0}", image.Size);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithFilter()
        {
            Assert.Inconclusive("Not yet implemented");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetImage()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                Console.WriteLine("Images");
                foreach (Image image in images)
                {
                    Image currentImage = await provider.GetImageAsync(image.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(currentImage);
                    Assert.AreEqual(image.Id, currentImage.Id);
                    Assert.AreEqual(image.Checksum, currentImage.Checksum);
                    Assert.AreEqual(image.Created, currentImage.Created);
                    Assert.AreEqual(image.File, currentImage.File);
                    Assert.AreEqual(image.LastModified, currentImage.LastModified);
                    Assert.AreEqual(image.Name, currentImage.Name);
                    Assert.AreEqual(image.Schema, currentImage.Schema);
                    Assert.AreEqual(image.Self, currentImage.Self);
                    Assert.AreEqual(image.Size, currentImage.Size);
                    Assert.AreEqual(image.Status, currentImage.Status);
                    Assert.AreEqual(image.Visibility, currentImage.Visibility);
                    Console.WriteLine("    {0} ({1})", image.Name, image.Id);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestUpdateImage()
        {
            Assert.Inconclusive("Not yet implemented");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestExportImportImage()
        {
            IImageService provider = CreateProvider();
            IObjectStorageProvider objectStorageProvider = Bootstrapper.CreateObjectStorageProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(600))))
            {
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                string containerName = UserObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName();
                containerName = containerName.Replace('.', '_');
                ObjectStore objectStore = objectStorageProvider.CreateContainer(containerName);
                Assert.AreEqual(ObjectStore.ContainerCreated, objectStore);

                Image imageToExport = images.FirstOrDefault(i => string.Equals(i.Name, "UnitTestSourceImage", StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(imageToExport, "Could not find source image to export.");

                ExportImageTask exportTask = await provider.ExportImageAsync(new ExportTaskDescriptor(imageToExport.Id, containerName), AsyncCompletionOption.RequestCompleted, cancellationTokenSource.Token, null);
                Assert.IsNotNull(exportTask);
                Assert.AreEqual(ImageTaskStatus.Success, exportTask.Status);
                string exportLocation = exportTask.Result.ExportLocation;
                Assert.IsNotNull(exportLocation);

                string imageName = TestImagePrefix + Path.GetRandomFileName().Replace('.', '_');
                string importFrom = exportLocation;
                ImportImageTask importTask = await provider.ImportImageAsync(new ImportTaskDescriptor(importFrom, imageName), AsyncCompletionOption.RequestCompleted, cancellationTokenSource.Token, null);
                Assert.IsNotNull(importTask);
                Assert.AreEqual(ImageTaskStatus.Success, importTask.Status);
                ImageId importedImageId = importTask.Result.ImageId;
                Assert.IsNotNull(importedImageId);

                await provider.RemoveImageAsync(importedImageId, cancellationTokenSource.Token);

                objectStorageProvider.DeleteContainer(containerName, deleteObjects: true);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImageMembers()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                Console.WriteLine("Images");
                foreach (Image image in images)
                {
                    ReadOnlyCollection<ImageMember> members = await provider.ListImageMembersAsync(image.Id, cancellationTokenSource.Token);
                    Assert.IsNotNull(members);
                    Console.WriteLine("    {0} ({1})", image.Name, image.Id);
                    foreach (ImageMember member in members)
                    {
                        Assert.AreEqual(image.Id, member.ImageId);
                        Console.WriteLine("        {0} ({1})", member.ImageId, member.Status);
                    }
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestCreateUpdateRemoveImageMember()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(600))))
            {
                ImageFilter filter = new ImageFilter(name: "UnitTestSourceImage");
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                Image image = images.FirstOrDefault(i => string.Equals(i.Name, "UnitTestSourceImage", StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(image, "Could not find source image to export.");

                IIdentityProvider identityProvider = Bootstrapper.CreateIdentityProvider();
                var userAccess = identityProvider.GetUserAccess(Bootstrapper.Settings.TestIdentity);
                ProjectId memberId = new ProjectId(userAccess.Token.Tenant.Id);

                ImageMember imageMember = await provider.CreateImageMemberAsync(image.Id, memberId, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMember);
                Assert.AreEqual(MemberStatus.Pending, imageMember.Status);
                Assert.AreEqual(image.Id, imageMember.ImageId);
                Assert.AreEqual(memberId, imageMember.MemberId);

                imageMember = await provider.UpdateImageMemberAsync(image.Id, memberId, MemberStatus.Accepted, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMember);
                Assert.AreEqual(MemberStatus.Accepted, imageMember.Status);
                Assert.AreEqual(image.Id, imageMember.ImageId);
                Assert.AreEqual(memberId, imageMember.MemberId);

                imageMember = await provider.UpdateImageMemberAsync(image.Id, memberId, MemberStatus.Rejected, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMember);
                Assert.AreEqual(MemberStatus.Rejected, imageMember.Status);
                Assert.AreEqual(image.Id, imageMember.ImageId);
                Assert.AreEqual(memberId, imageMember.MemberId);

                await provider.RemoveImageMemberAsync(image.Id, memberId, cancellationTokenSource.Token);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestImageTagging()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images");

                Image imageToTag = images.FirstOrDefault(i => string.Equals(i.Name, "UnitTestSourceImage", StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(imageToTag, "Could not find source image to add a tag to.");

                ImageTag tag = new ImageTag(Path.GetRandomFileName().Replace('.', '_'));

                await provider.AddImageTagAsync(imageToTag.Id, tag, cancellationTokenSource.Token);
                Image taggedImage = await provider.GetImageAsync(imageToTag.Id, cancellationTokenSource.Token);
                Assert.IsNotNull(taggedImage);
                Assert.IsNotNull(taggedImage.Tags);
                Assert.IsTrue(taggedImage.Tags.Contains(tag));

                await provider.RemoveImageTagAsync(imageToTag.Id, tag, cancellationTokenSource.Token);
                Image untaggedImage = await provider.GetImageAsync(imageToTag.Id, cancellationTokenSource.Token);
                Assert.IsNotNull(untaggedImage);
                Assert.IsNotNull(untaggedImage.Tags);
                Assert.IsFalse(untaggedImage.Tags.Contains(tag));
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListTasks()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ReadOnlyCollection<GenericImageTask> images = await ListAllTasksAsync(provider, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any tasks");

                Console.WriteLine("Tasks");
                foreach (ImageTask image in images)
                {
                    Console.WriteLine("    {0} ({1})", image.Id, image.Type);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetTask()
        {
            Assert.Inconclusive("Not yet implemented");
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetImagesSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetImagesSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetImageSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetImageSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetImageMembersSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetImageMembersSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetImageMemberSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetImageMemberSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetTasksSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetTasksSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestGetTaskSchema()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(300))))
            {
                JsonSchema schema = await provider.GetTaskSchemaAsync(cancellationTokenSource.Token);
                Assert.IsNotNull(schema);
            }
        }

        protected static async Task<ReadOnlyCollection<Image>> ListAllImagesAsync(IImageService service, int? blockSize, CancellationToken cancellationToken, net.openstack.Core.IProgress<ReadOnlyCollection<Image>> progress = null)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return await (await service.ListImagesAsync(null, blockSize, cancellationToken)).GetAllPagesAsync(cancellationToken, progress);
        }

        protected static async Task<ReadOnlyCollection<Image>> ListAllImagesAsync(IImageService service, ImageFilter filter, int? blockSize, CancellationToken cancellationToken, net.openstack.Core.IProgress<ReadOnlyCollection<Image>> progress = null)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return await (await service.ListImagesAsync(filter, null, blockSize, cancellationToken)).GetAllPagesAsync(cancellationToken, progress);
        }

        protected static async Task<ReadOnlyCollection<GenericImageTask>> ListAllTasksAsync(IImageService service, CancellationToken cancellationToken, net.openstack.Core.IProgress<ReadOnlyCollection<GenericImageTask>> progress = null)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return await (await service.ListTasksAsync(cancellationToken)).GetAllPagesAsync(cancellationToken, progress);
        }

        private TimeSpan TestTimeout(TimeSpan timeout)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Using extended timeout due to attached debugger.");
                return TimeSpan.FromDays(1);
            }

            return timeout;
        }

        /// <summary>
        /// Creates an instance of <see cref="IImageService"/> for testing using
        /// the <see cref="OpenstackNetSetings.TestIdentity"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IImageService"/> for integration testing.</returns>
        internal static IImageService CreateProvider()
        {
            var provider = new TestCloudImagesProvider(Bootstrapper.Settings.TestIdentity, Bootstrapper.Settings.DefaultRegion, null);
            provider.BeforeAsyncWebRequest +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Request) {1} {2}", DateTime.Now, e.Request.Method, e.Request.RequestUri);
                };
            provider.AfterAsyncWebResponse +=
                (sender, e) =>
                {
                    Console.Error.WriteLine("{0} (Result {1}) {2}", DateTime.Now, e.Response.StatusCode, e.Response.ResponseUri);
                };

            provider.ConnectionLimit = 20;
            return provider;
        }

        internal class TestCloudImagesProvider : CloudImagesProvider
        {
            public TestCloudImagesProvider(CloudIdentity defaultIdentity, string defaultRegion, IIdentityProvider identityProvider)
                : base(defaultIdentity, defaultRegion, identityProvider)
            {
            }

            protected override byte[] EncodeRequestBodyImpl<TBody>(HttpWebRequest request, TBody body)
            {
                byte[] encoded = base.EncodeRequestBodyImpl<TBody>(request, body);
                Console.Error.WriteLine("<== " + Encoding.UTF8.GetString(encoded));
                return encoded;
            }

            protected override Tuple<HttpWebResponse, string> ReadResultImpl(Task<WebResponse> task, CancellationToken cancellationToken)
            {
                try
                {
                    Tuple<HttpWebResponse, string> result = base.ReadResultImpl(task, cancellationToken);
                    LogResult(result.Item1, result.Item2, true);
                    return result;
                }
                catch (WebException ex)
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    if (response != null && response.ContentLength != 0)
                        LogResult(response, ex.Message, true);

                    throw;
                }
            }

            private void LogResult(HttpWebResponse response, string rawBody, bool reformat)
            {
                foreach (string header in response.Headers)
                {
                    Console.Error.WriteLine(string.Format("{0}: {1}", header, response.Headers[header]));
                }

                if (!string.IsNullOrEmpty(rawBody))
                {
                    if (reformat)
                    {
                        try
                        {
                            object parsed = JsonConvert.DeserializeObject(rawBody);
                            Console.Error.WriteLine("==> " + JsonConvert.SerializeObject(parsed, Formatting.Indented));
                        }
                        catch (JsonException)
                        {
                            Console.Error.WriteLine("==> " + rawBody);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("==> " + rawBody);
                    }
                }
            }
        }
    }
}
