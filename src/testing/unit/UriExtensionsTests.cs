namespace OpenStackNet.Testing.Unit
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenStack.Compat;

    [TestClass]
    public class UriExtensionsTests
    {
        [TestMethod]
        public void TestGetSegments()
        {
            Uri uri = new Uri("http://www.example.com/path/to/file");

            string[] expectedSegments = { "/", "path/", "to/", "file" };
            string[] referenceSegments = uri.Segments;
            CollectionAssert.AreEqual(expectedSegments, referenceSegments);

            string[] extensionSegments = uri.GetSegments();
            CollectionAssert.AreEqual(referenceSegments, extensionSegments);
        }
    }
}
