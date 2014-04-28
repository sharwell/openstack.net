namespace OpenStack.Net
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Rackspace.Threading;

#if !NET40PLUS
    using OpenStack.Compat;
#endif

    public class HttpApiCall : HttpApiCall<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
            : base(httpClient, requestMessage, completionOption)
        {
        }

        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate)
            : base(httpClient, requestMessage, completionOption, validate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApiCall"/> class
        /// with the specified request message.
        /// </summary>
        /// <param name="requestMessage">The <see cref="HttpRequestMessage"/> representing the HTTP API request.</param>
        /// <param name="disposeMessage"><see langword="true"/> to call <see cref="IDisposable.Dispose"/> on the <paramref name="requestMessage"/> object when this object is disposed; otherwise, <see langword="false"/>. The default value is <see langword="true"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="requestMessage"/> is <see langword="null"/>.</exception>
        public HttpApiCall(HttpClient httpClient, HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<Task<HttpResponseMessage>, CancellationToken, Task<HttpResponseMessage>> validate, bool disposeMessage)
            : base(httpClient, requestMessage, completionOption, validate, disposeMessage)
        {
        }

        public static bool IsAcceptable(HttpResponseMessage responseMessage)
        {
            bool acceptable = true;
            if (responseMessage.RequestMessage != null)
            {
                HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> acceptHeaders = responseMessage.RequestMessage.Headers.Accept;
                if (acceptHeaders.Count > 0)
                {
                    MediaTypeHeaderValue contentType = responseMessage.Content.Headers.ContentType;
                    acceptable = false;
                    foreach (var acceptHeader in acceptHeaders)
                    {
                        if (IsAcceptable(acceptHeader, contentType))
                        {
                            acceptable = true;
                            break;
                        }
                    }
                }
            }

            return acceptable;
        }

        public static bool IsAcceptable(MediaTypeHeaderValue acceptHeader, MediaTypeHeaderValue contentType)
        {
            if (acceptHeader == null)
                throw new ArgumentNullException("acceptHeader");
            if (string.IsNullOrEmpty(acceptHeader.MediaType))
                throw new ArgumentException("acceptHeader.MediaType cannot be empty");

            // if the response doesn't include a Content-Type header, assume not acceptable
            if (contentType == null || string.IsNullOrEmpty(contentType.MediaType))
                return false;

            // handle wildcards
            if (acceptHeader.MediaType == "*/*")
                return true;

            if (acceptHeader.MediaType.EndsWith("/*"))
            {
                // include the slash, but not the *
                string acceptType = acceptHeader.MediaType.Substring(0, acceptHeader.MediaType.Length - 1);

                if (contentType.MediaType.Length < acceptType.Length)
                    return false;

                // case-insensitive "starts with"
                return StringComparer.OrdinalIgnoreCase.Equals(contentType.MediaType.Substring(0, acceptType.Length), acceptType);
            }

            // accept header without wildcards
            return StringComparer.OrdinalIgnoreCase.Equals(contentType.MediaType, acceptHeader.MediaType);
        }

        protected override Task<string> DeserializeResultImplAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.Content == null)
                return CompletedTask.FromResult(default(string));

            return response.Content.ReadAsStringAsync();
        }
    }
}
