﻿namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
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
        public async Task TestListImagesWithAscendingCreatedSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Created, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Created).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending created sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].Created >= images[i - 1].Created);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingCreatedSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Created, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Created).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending created sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].Created <= images[i - 1].Created);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingIdSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Id, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Id).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending ID sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Id.Value, images[i - 1].Id.Value) >= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingIdSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Id, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Id).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending ID sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Id.Value, images[i - 1].Id.Value) <= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingLastModifiedSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.LastModified, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.LastModified).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending last modified sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].LastModified >= images[i - 1].LastModified);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingLastModifiedSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.LastModified, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.LastModified).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending last modified sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].LastModified <= images[i - 1].LastModified);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingNameSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.ImageName, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Name).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending name sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Name, images[i - 1].Name) >= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingNameSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.ImageName, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Name).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending name sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Name, images[i - 1].Name) <= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingSizeSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Size, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Size).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending size sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].Size >= images[i - 1].Size);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingSizeSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Size, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Size).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending size sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(images[i].Size <= images[i - 1].Size);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingStatusSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Status, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Status).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending status sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Status.Name, images[i - 1].Status.Name) >= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingStatusSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Status, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Status).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending status sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Status.Name, images[i - 1].Status.Name) <= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingVisibilitySortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Visibility, sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Visibility).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending visibility sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Visibility.Name, images[i - 1].Visibility.Name) >= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingVisibilitySortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.Visibility, sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.Visibility).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the descending visibility sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].Visibility.Name, images[i - 1].Visibility.Name) <= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithAscendingCustomAttributeSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.FromName(PredefinedImageProperties.MinDisk), sortDirection: ImageSortDirection.Ascending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.ExtensionData[PredefinedImageProperties.MinDisk].ToString()).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending custom attribute sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].ExtensionData[PredefinedImageProperties.MinDisk].ToString(), images[i - 1].ExtensionData[PredefinedImageProperties.MinDisk].ToString()) >= 0);
                }
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.User)]
        [TestCategory(TestCategories.Images)]
        public async Task TestListImagesWithDescendingCustomAttributeSortFilter()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(10))))
            {
                ImageFilter filter = new ImageFilter(sortKey: ImageSortKey.FromName(PredefinedImageProperties.MinDisk), sortDirection: ImageSortDirection.Descending);
                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Select(i => i.ExtensionData[PredefinedImageProperties.MinDisk].ToString()).Distinct().Count() < 2)
                    Assert.Inconclusive("The service did not report enough images to test the ascending custom attribute sort");

                for (int i = 1; i < images.Count; i++)
                {
                    Assert.IsTrue(StringComparer.OrdinalIgnoreCase.Compare(images[i].ExtensionData[PredefinedImageProperties.MinDisk].ToString(), images[i - 1].ExtensionData[PredefinedImageProperties.MinDisk].ToString()) <= 0);
                }
            }
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
                string containerName = UserObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName();
                containerName = containerName.Replace('.', '_');
                ObjectStore objectStore = objectStorageProvider.CreateContainer(containerName);
                Assert.AreEqual(ObjectStore.ContainerCreated, objectStore);

                Image imageToExport = await GetUnitTestImageAsync(provider, cancellationTokenSource.Token);

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
                IIdentityProvider identityProvider = Bootstrapper.CreateIdentityProvider();
                var userAccess = identityProvider.GetUserAccess(Bootstrapper.Settings.TestIdentity);
                ProjectId memberId = new ProjectId(userAccess.Token.Tenant.Id);
                ImageFilter filter = new ImageFilter(owner: memberId);

                ReadOnlyCollection<Image> images = await ListAllImagesAsync(provider, filter, null, cancellationTokenSource.Token);
                if (images.Count == 0)
                    Assert.Inconclusive("The service did not report any images owned by the current user");

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
        public async Task TestCreateListUpdateRemoveImageMember()
        {
            IImageService provider = CreateProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(600))))
            {
                Image image = await GetUnitTestImageAsync(provider, cancellationTokenSource.Token);

                IIdentityProvider identityProvider = Bootstrapper.CreateIdentityProvider();
                var userAccess = identityProvider.GetUserAccess(Bootstrapper.Settings.TestIdentity);
                ProjectId memberId = new ProjectId(userAccess.Token.Tenant.Id);

                ImageMember imageMember = await provider.CreateImageMemberAsync(image.Id, memberId, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMember);
                Assert.AreEqual(MemberStatus.Pending, imageMember.Status);
                Assert.AreEqual(image.Id, imageMember.ImageId);
                Assert.AreEqual(memberId, imageMember.MemberId);

                ReadOnlyCollection<ImageMember> imageMembers = await ListAllImageMembersAsync(provider, image.Id, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMembers);
                Assert.IsTrue(imageMembers.Count > 0);
                Assert.IsTrue(imageMembers.Any(i => i.ImageId == image.Id && i.MemberId == memberId));

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

                imageMembers = await ListAllImageMembersAsync(provider, image.Id, cancellationTokenSource.Token);
                Assert.IsNotNull(imageMembers);
                Assert.IsTrue(!imageMembers.Any(i => i.ImageId == image.Id && i.MemberId == memberId));
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
                Image imageToTag = await GetUnitTestImageAsync(provider, cancellationTokenSource.Token);

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
        public async Task TestListGetTasks()
        {
            IImageService provider = CreateProvider();
            IObjectStorageProvider objectStorageProvider = Bootstrapper.CreateObjectStorageProvider();
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TestTimeout(TimeSpan.FromSeconds(600))))
            {
                string containerName = UserObjectStorageTests.TestContainerPrefix + Path.GetRandomFileName();
                containerName = containerName.Replace('.', '_');
                ObjectStore objectStore = objectStorageProvider.CreateContainer(containerName);
                Assert.AreEqual(ObjectStore.ContainerCreated, objectStore);

                Image imageToExport = await GetUnitTestImageAsync(provider, cancellationTokenSource.Token);

                Task<ExportImageTask> exportTask1 = provider.ExportImageAsync(new ExportTaskDescriptor(imageToExport.Id, containerName), AsyncCompletionOption.RequestCompleted, cancellationTokenSource.Token, null);
                Task<ExportImageTask> exportTask2 = provider.ExportImageAsync(new ExportTaskDescriptor(imageToExport.Id, containerName), AsyncCompletionOption.RequestCompleted, cancellationTokenSource.Token, null);

                ReadOnlyCollection<GenericImageTask> tasks = await ListAllTasksAsync(provider, cancellationTokenSource.Token, null);
                Assert.IsNotNull(tasks);
                Assert.IsTrue(tasks.Count >= 2);
                Assert.IsTrue(tasks.Count(i => i.Type == ImageTaskType.Export) >= 2);

                foreach (GenericImageTask task in tasks)
                {
                    if (task.Type == ImageTaskType.Export)
                    {
                        ExportImageTask singleTask = await provider.GetTaskAsync<ExportImageTask>(task.Id, cancellationTokenSource.Token);
                        Assert.IsNotNull(singleTask);
                    }
                    else if (task.Type == ImageTaskType.Import)
                    {
                        ImportImageTask singleTask = await provider.GetTaskAsync<ImportImageTask>(task.Id, cancellationTokenSource.Token);
                        Assert.IsNotNull(singleTask);
                    }
                    else
                    {
                        GenericImageTask singleTask = await provider.GetTaskAsync<GenericImageTask>(task.Id, cancellationTokenSource.Token);
                        Assert.IsNotNull(singleTask);
                    }
                }

                await Task.WhenAll(exportTask1, exportTask2);
                objectStorageProvider.DeleteContainer(containerName, deleteObjects: true);
            }
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

        protected static async Task<Image> GetUnitTestImageAsync(IImageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            ImageFilter filter = new ImageFilter(name: "UnitTestSourceImage");
            ReadOnlyCollection<Image> images = await ListAllImagesAsync(service, filter, null, cancellationToken);
            Assert.IsTrue(images.Count > 0, string.Format("Could not find an image with the name '{0}' in the test region.", filter.Name));
            return images.FirstOrDefault();
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

        protected static async Task<ReadOnlyCollection<ImageMember>> ListAllImageMembersAsync(IImageService service, ImageId imageId, CancellationToken cancellationToken, net.openstack.Core.IProgress<ReadOnlyCollection<ImageMember>> progress = null)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return await (await service.ListImageMembersAsync(imageId, cancellationToken)).GetAllPagesAsync(cancellationToken, progress);
        }

        protected static async Task<ReadOnlyCollection<ImageMember>> ListAllImageMembersAsync(IImageService service, ImageId imageId, ImageMemberFilter filter, CancellationToken cancellationToken, net.openstack.Core.IProgress<ReadOnlyCollection<ImageMember>> progress = null)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return await (await service.ListImageMembersAsync(imageId, filter, cancellationToken)).GetAllPagesAsync(cancellationToken, progress);
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
