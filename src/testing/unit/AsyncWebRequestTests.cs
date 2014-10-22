namespace OpenStackNet.Testing.Unit
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using net.openstack.Core;
    using Xunit;

    /// <preliminary/>
    public class AsyncWebRequestTests
    {
        [Fact]
        public void TestAsyncWebRequest()
        {
            Uri uri = new Uri("http://google.com");
            WebRequest request = HttpWebRequest.Create(uri);
            Task<WebResponse> response = request.GetResponseAsync();
            response.Wait();
        }

        [Fact]
        public void TestAsyncWebRequestTimeout()
        {
            Uri uri = new Uri("http://google.com");
            WebRequest request = HttpWebRequest.Create(uri);
            request.Timeout = 0;
            Task<WebResponse> response = request.GetResponseAsync();
            try
            {
                response.Wait();
                Assert.True(false, "Expected an exception");
            }
            catch (AggregateException exception)
            {
                Assert.Equal(TaskStatus.Faulted, response.Status);

                ReadOnlyCollection<Exception> exceptions = exception.InnerExceptions;
                Assert.Equal(1, exceptions.Count);
                Assert.IsAssignableFrom<WebException>(exceptions[0]);

                WebException webException = (WebException)exceptions[0];
                Assert.Equal(WebExceptionStatus.Timeout, webException.Status);
            }
        }

        [Fact]
        public void TestAsyncWebRequestCancellation()
        {
            Uri uri = new Uri("http://google.com");
            WebRequest request = HttpWebRequest.Create(uri);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task<WebResponse> response = request.GetResponseAsync(cancellationTokenSource.Token);
            cancellationTokenSource.Cancel();
            try
            {
                response.Wait();
                Assert.True(false, "Expected an exception");
            }
            catch (AggregateException exception)
            {
                Assert.Equal(TaskStatus.Canceled, response.Status);

                ReadOnlyCollection<Exception> exceptions = exception.InnerExceptions;
                Assert.Equal(1, exceptions.Count);
                Assert.IsAssignableFrom<OperationCanceledException>(exceptions[0]);
            }
        }

        [Fact]
        public void TestAsyncWebRequestError()
        {
            Uri uri = new Uri("http://google.com/fail");
            WebRequest request = HttpWebRequest.Create(uri);
            Task<WebResponse> response = request.GetResponseAsync();
            try
            {
                response.Wait();
                Assert.True(false, "Expected an exception");
            }
            catch (AggregateException exception)
            {
                Assert.Equal(TaskStatus.Faulted, response.Status);

                ReadOnlyCollection<Exception> exceptions = exception.InnerExceptions;
                Assert.Equal(1, exceptions.Count);
                Assert.IsAssignableFrom<WebException>(exceptions[0]);

                WebException webException = (WebException)exceptions[0];
                Assert.Equal(HttpStatusCode.NotFound, ((HttpWebResponse)webException.Response).StatusCode);
            }
        }
    }
}
