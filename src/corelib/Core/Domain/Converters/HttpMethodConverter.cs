﻿namespace net.openstack.Core.Domain.Converters
{
    using System.Net.Http;

    public class HttpMethodConverter : SimpleStringJsonConverter<HttpMethod>
    {
        /// <inheritdoc/>
        protected override HttpMethod ConvertToObject(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            switch (str.ToUpperInvariant())
            {
            case "DELETE":
                return HttpMethod.Delete;
            case "GET":
                return HttpMethod.Get;
            case "HEAD":
                return HttpMethod.Head;
            case "OPTIONS":
                return HttpMethod.Options;
            case "POST":
                return HttpMethod.Post;
            case "PUT":
                return HttpMethod.Put;
            case "TRACE":
                return HttpMethod.Trace;
            default:
                return new HttpMethod(str.ToUpperInvariant());
            }
        }
    }
}
