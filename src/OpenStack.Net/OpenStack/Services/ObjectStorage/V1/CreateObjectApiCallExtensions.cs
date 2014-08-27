namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;

    public static class CreateObjectApiCallExtensions
    {
        public const string DetectContentType = "X-Detect-Content-Type";

        public static CreateObjectApiCall WithMetadata(this CreateObjectApiCall apiCall, ObjectMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public static CreateObjectApiCall WithContentType(this CreateObjectApiCall apiCall, MediaTypeHeaderValue contentType)
        {
            if (apiCall == null)
                throw new ArgumentNullException("apiCall");

            apiCall.RequestMessage.Content.Headers.ContentType = contentType;
            return apiCall;
        }

        public static CreateObjectApiCall WithDetectContentType(this CreateObjectApiCall apiCall, bool value)
        {
            if (apiCall == null)
                throw new ArgumentNullException("apiCall");

            HttpRequestHeaders requestHeaders = apiCall.RequestMessage.Headers;
            requestHeaders.Remove(DetectContentType);
            if (value)
                requestHeaders.Add(DetectContentType, "true");

            return apiCall;
        }
    }
}
