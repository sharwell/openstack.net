namespace OpenStack.Net
{
    using System;
    using System.Threading.Tasks;
    using CancellationToken = System.Threading.CancellationToken;
    using HttpCompletionOption = System.Net.Http.HttpCompletionOption;
    using HttpRequestMessage = System.Net.Http.HttpRequestMessage;
    using HttpResponseMessage = System.Net.Http.HttpResponseMessage;
    using Stream = System.IO.Stream;

    public interface IHttpApiCallFactory
    {
        HttpApiCall<T> CreateJsonApiCall<T>(HttpRequestMessage requestMessage);

        HttpApiCall<Stream> CreateStreamingApiCall(HttpRequestMessage requestMessage);

        HttpApiCall CreateBasicApiCall(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);

        HttpApiCall<T> CreateCustomApiCall<T>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, Func<HttpResponseMessage, CancellationToken, Task<T>> deserializeResult);
    }
}
