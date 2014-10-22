using System;
using net.openstack.Providers.Rackspace.Exceptions;
using net.openstack.Providers.Rackspace.Validators;
using Xunit;

namespace OpenStackNet.Testing.Unit.Providers.Rackspace
{
    public class CloudBlockStorageTests
    {
        [Fact]
        public void Should_Not_Throw_Exception_When_Size_Is_In_Range()
        {
            const int size = 900;

            try
            {
                var cloudBlockStorageValidator = CloudBlockStorageValidator.Default;
                cloudBlockStorageValidator.ValidateVolumeSize(size);
            }
            catch (Exception)
            {

                Assert.True(false, "Exception should not be thrown.");
            }
        }

        [Fact]
        public void Should_Throw_Exception_When_Size_Is_Less_Than_100()
        {
            const int size = 50;

            try
            {
                var cloudBlockStorageValidator = CloudBlockStorageValidator.Default;
                cloudBlockStorageValidator.ValidateVolumeSize(size);
                Assert.True(false, "Expected exception was not thrown.");
            }
            catch (Exception exc)
            {
                Assert.IsAssignableFrom<InvalidVolumeSizeException>(exc);
            }
        }

        [Fact]
        public void Should_Throw_Exception_When_Size_Is_Greater_Than_1000()
        {
            const int size = 1050;

            try
            {
                var cloudBlockStorageValidator = CloudBlockStorageValidator.Default;
                cloudBlockStorageValidator.ValidateVolumeSize(size);
                Assert.True(false, "Expected was not thrown.");
            }
            catch (Exception exc)
            {
                Assert.IsAssignableFrom<InvalidVolumeSizeException>(exc);
            }
        }
    }
}
