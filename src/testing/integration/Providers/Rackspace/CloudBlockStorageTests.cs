using System.Linq;
using System.Threading;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using net.openstack.Providers.Rackspace.Objects;
using Xunit;

namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    [TestClass]
    public class CloudBlockStorageTests
    {
        private static RackspaceCloudIdentity _testIdentity;
        private TestContext testContextInstance;

        private const string volumeDisplayName = "Integration Test Volume";
        private const string volumeDisplayDescription = "Integration Test Volume Description";
        private const string snapshotDisplayName = "Integration Test Snapshot";
        private const string snapshotDisplayDescription = "Integration Test Snapshot Description";

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
            _testIdentity = new RackspaceCloudIdentity(Bootstrapper.Settings.TestIdentity);

        }

        #region Volume Tests
        [Fact]
        public void Should_Return_Volume_Type_List()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeTypeListResponse = provider.ListVolumeTypes(identity: _testIdentity);
            Assert.NotNull(volumeTypeListResponse);
            Assert.True(volumeTypeListResponse.Any());
        }

        [Fact]
        public void Should_Return_Single_Volume_Type()
        {
            var provider = new CloudBlockStorageProvider();

            var volumeTypeListResponse = provider.ListVolumeTypes(identity: _testIdentity);
            if (volumeTypeListResponse != null && volumeTypeListResponse.Any())
            {
                var firstVolumeTypeInList = volumeTypeListResponse.First();
                var singleVolumeTypeResponse = provider.DescribeVolumeType(firstVolumeTypeInList.Id, identity: _testIdentity);
                Assert.NotNull(singleVolumeTypeResponse);
                Assert.True(singleVolumeTypeResponse.Id == firstVolumeTypeInList.Id);
            }
            else
            {
                Assert.True(false, "No volumes types present to query.");
            }
        }

        [Fact]
        public void Should_Create_Volume_Only_Required_Parameters()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeCreatedResponse = provider.CreateVolume(100, identity: _testIdentity);
            Assert.NotNull(volumeCreatedResponse);
        }

        [Fact]
        public void Should_Create_Volume_Full_Parameters()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeCreatedResponse = provider.CreateVolume(100, volumeDisplayDescription, volumeDisplayName, null, "SATA", null, _testIdentity);
            Assert.NotNull(volumeCreatedResponse);


        }

        [Fact(Timeout = 1800000)]
        public void Should_Wait_Until_Test_Volume_Becomes_Available_Or_Exceeds_Timeout_For_Becoming_Available()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            if (volumeListResponse != null && volumeListResponse.Any())
            {
                var testVolume = volumeListResponse.FirstOrDefault(x => x.DisplayName == volumeDisplayName);
                if (testVolume == null)
                {
                    Assert.True(false, "Could not find test volume to query for Available status.");
                }

                var volumeDetails = provider.WaitForVolumeAvailable(testVolume.Id, identity: _testIdentity);
                Assert.NotNull(volumeDetails);
                Assert.Equal(VolumeState.Available, volumeDetails.Status);

            }
            else
            {
                Assert.True(false, "No volumes present to obtain Available status.");
            }
        }

        [Fact]
        public void Should_Return_Volume_List()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            Assert.NotNull(volumeListResponse);
            Assert.True(volumeListResponse.Any());
        }

        [Fact]
        public void Should_Return_Test_Volume()
        {
            var provider = new CloudBlockStorageProvider();

            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            if (volumeListResponse != null && volumeListResponse.Any())
            {
                var testVolume = volumeListResponse.FirstOrDefault(x => x.DisplayName == volumeDisplayName);
                if (testVolume == null)
                {
                    Assert.True(false, "Could not retrieve test volume.");
                }
                var singleVolumeResponse = provider.ShowVolume(testVolume.Id, identity: _testIdentity);
                Assert.NotNull(singleVolumeResponse);
                Assert.True(singleVolumeResponse.Id == testVolume.Id);
            }
            else
            {
                Assert.True(false, "No volumes present to query.");
            }
        }

        [Fact(Timeout = 1800000)]
        public void Should_Delete_Test_Volume()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            if (volumeListResponse != null && volumeListResponse.Any())
            {
                var testVolume = volumeListResponse.FirstOrDefault(x => x.DisplayName == volumeDisplayName);
                if (testVolume == null)
                {
                    Assert.True(false, "Could not find test volume to delete.");
                }
                var deleteVolumeResult = provider.DeleteVolume(testVolume.Id, identity: _testIdentity);
                if (deleteVolumeResult)
                {
                    var volumeWaitForDeletedResult = provider.WaitForVolumeDeleted(testVolume.Id, identity: _testIdentity);
                    Assert.True(volumeWaitForDeletedResult);
                }
                else
                {
                    Assert.True(false, "Test volume was not deleted.");
                }
            }
            else
            {
                Assert.True(false, "No volumes present to test delete.");
            }
        }

        #endregion

        #region Snapshot Tests
        [Fact]
        public void Should_Create_Snapshot_Only_Required_Parameters()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            if (volumeListResponse != null && volumeListResponse.Any())
            {
                var testVolume = volumeListResponse.FirstOrDefault(x => x.DisplayName == volumeDisplayName);
                if (testVolume == null)
                {
                    Assert.True(false, "Could not find test volume to create snapshot.");
                }

                var snapshotCreatedResponse = provider.CreateSnapshot(testVolume.Id, identity: _testIdentity);
                Assert.NotNull(snapshotCreatedResponse);
            }
            else
            {
                Assert.True(false, "No volumes present to create a snapshot.");
            }
        }

        [Fact]
        public void Should_Create_Snapshot_Full_Parameters()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);
            if (volumeListResponse != null && volumeListResponse.Any())
            {
                var testVolume = volumeListResponse.FirstOrDefault(x => x.DisplayName == volumeDisplayName);
                if (testVolume == null)
                {
                    Assert.True(false, "Could not find test volume to create snapshot.");
                }

                var snapshotCreatedResponse = provider.CreateSnapshot(testVolume.Id, true, snapshotDisplayName,
                                                                      snapshotDisplayDescription,
                                                                      identity: _testIdentity);
                Assert.NotNull(snapshotCreatedResponse);
            }
            else
            {
                Assert.True(false, "No volumes present to create a snapshot.");
            }
        }

        [Fact(Timeout = 1800000)]
        public void Should_Wait_Until_Test_Snapshot_Becomes_Available_Or_Exceeds_Timeout_For_Becoming_Available()
        {
            var provider = new CloudBlockStorageProvider();

            var snapshotListResponse = provider.ListSnapshots(identity: _testIdentity);
            if (snapshotListResponse != null && snapshotListResponse.Any())
            {
                var testSnapshot = snapshotListResponse.FirstOrDefault(x => x.DisplayName == snapshotDisplayName);
                if (testSnapshot == null)
                {
                    Assert.True(false, "Could not find test snapshot to query for Available status.");
                }

                var snapshotDetails = provider.WaitForSnapshotAvailable(testSnapshot.Id, identity: _testIdentity);
                Assert.NotNull(snapshotDetails);
                Assert.Equal(SnapshotState.Available, snapshotDetails.Status);

            }
            else
            {
                Assert.True(false, "No snapshots present to obtain Available status.");
            }
        }

        [Fact]
        public void Should_Return_Snapshot_List()
        {
            var provider = new CloudBlockStorageProvider();
            var snapshotListResponse = provider.ListSnapshots(identity: _testIdentity);
            Assert.NotNull(snapshotListResponse);
            Assert.True(snapshotListResponse.Any());
        }

        [Fact]
        public void Should_Return_Test_Snapshot()
        {
            var provider = new CloudBlockStorageProvider();

            var snapshotListResponse = provider.ListSnapshots(identity: _testIdentity);
            if (snapshotListResponse != null && snapshotListResponse.Any())
            {
                var testSnapshot = snapshotListResponse.FirstOrDefault(x => x.DisplayName == snapshotDisplayName);
                if (testSnapshot == null)
                {
                    Assert.True(false, "Could not retrieve test snapshot");
                }
                var singleSnapshotResponse = provider.ShowSnapshot(testSnapshot.Id, identity: _testIdentity);
                Assert.NotNull(singleSnapshotResponse);
                Assert.True(singleSnapshotResponse.Id == testSnapshot.Id);
            }
            else
            {
                Assert.True(false, "No snapshots present to query.");
            }
        }

        [Fact(Timeout = 1800000)]
        public void Should_Delete_Test_Snapshot()
        {
            var provider = new CloudBlockStorageProvider();
            var snapshotListResponse = provider.ListSnapshots(identity: _testIdentity);
            if (snapshotListResponse != null && snapshotListResponse.Any())
            {
                var testSnapshot = snapshotListResponse.FirstOrDefault(x => x.DisplayName == snapshotDisplayName);
                if (testSnapshot == null)
                {
                    Assert.True(false, "Could not find test snapshot to delete.");
                }
                var deleteSnapshotResult = provider.DeleteSnapshot(testSnapshot.Id, identity: _testIdentity);

                if (deleteSnapshotResult)
                {
                    var snapshotDeleteDetails = provider.WaitForSnapshotDeleted(testSnapshot.Id, identity: _testIdentity);
                    Assert.True(snapshotDeleteDetails);
                }
                else
                {
                    Assert.True(false, "Test snapshot was not deleted.");
                }
            }
            else
            {
                Assert.True(false, "No snapshots present to test delete.");
            }
        }

        #endregion

        #region Cleanup Routines
        [Fact]
        public void Should_Cleanup_CloudBlockStorage_Environment()
        {
            var provider = new CloudBlockStorageProvider();
            var volumeListResponse = provider.ListVolumes(identity: _testIdentity);

            if (volumeListResponse == null || !volumeListResponse.Any()) return;

            var testVolumeList = volumeListResponse.Where(x => x.DisplayName == volumeDisplayName);
            if (!testVolumeList.Any()) return;

            var snapshotList = provider.ListSnapshots(identity: _testIdentity);
            if (snapshotList != null && snapshotList.Any())
            {
                foreach (var testVolume in testVolumeList)
                {
                    var testVolumeSnapshots = snapshotList.Where(x => x.VolumeId == testVolume.Id);

                    if (testVolumeSnapshots.Any())
                    {
                        foreach (var testSnapshot in testVolumeSnapshots)
                        {
                            var deleteSnapshotResult = provider.DeleteSnapshot(testSnapshot.Id,
                                                                               identity: _testIdentity);

                            if (deleteSnapshotResult)
                            {
                                var snapshotDeleteDetails = provider.WaitForSnapshotDeleted(testSnapshot.Id,
                                                                                            identity: _testIdentity);
                                Assert.True(snapshotDeleteDetails);
                            }
                            else
                            {
                                Assert.True(false, string.Format("Snapshot (Volume ID: {0} -- Snapshot ID:{1}) could not be deleted.", testVolume.Id, testSnapshot.Id));
                            }

                        }
                    }
                }
            }

            foreach (var testVolume in testVolumeList)
            {
                var deleteVolumeResults = provider.DeleteVolume(testVolume.Id, identity: _testIdentity);
                Assert.True(deleteVolumeResults);
                var volumeWaitForDeletedResult = provider.WaitForVolumeDeleted(testVolume.Id, identity: _testIdentity);
                Assert.True(volumeWaitForDeletedResult);
            }
        }
        #endregion
    }
}
