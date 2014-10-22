using net.openstack.Providers.Rackspace;
using Xunit;

namespace OpenStackNet.Testing.Unit.Providers.Rackspace
{
    public class EncodeDecodeProviderTests
    {
        [Fact]
        public void Should_Encode_Spaces()
        {
            const string stringToBeEncoded = "This is a test with spaces";
            var encodeDecodeProvider = EncodeDecodeProvider.Default;
            var result = encodeDecodeProvider.UrlEncode(stringToBeEncoded);
            Assert.Equal("This%20is%20a%20test%20with%20spaces", result);
        }

        [Fact]
        public void Should_Decode_Spaces()
        {
            const string stringToBeDecoded = "This%20is%20a%20test%20with%20spaces";
            var encodeDecodeProvider = EncodeDecodeProvider.Default;
            var result = encodeDecodeProvider.UrlDecode(stringToBeDecoded);
            Assert.Equal("This is a test with spaces", result);
        }

        [Fact]
        public void Should_Encode_Special_Characters()
        {
            const string stringToBeEncoded = "This $ is & a ? test * with @ spaces";
            var encodeDecodeProvider = EncodeDecodeProvider.Default;
            var result = encodeDecodeProvider.UrlEncode(stringToBeEncoded);
            Assert.Equal("This%20%24%20is%20%26%20a%20%3f%20test%20*%20with%20%40%20spaces", result);
        }

        [Fact]
        public void Should_Decode_Special_Characters()
        {
            const string stringToBeEncoded = "This%20%24%20is%20%26%20a%20%3f%20test%20*%20with%20%40%20spaces";
            var encodeDecodeProvider = EncodeDecodeProvider.Default;
            var result = encodeDecodeProvider.UrlDecode(stringToBeEncoded);
            Assert.Equal("This $ is & a ? test * with @ spaces", result);
        }

    }
}
