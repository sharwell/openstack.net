namespace Net.OpenStack.Testing.Integration.Providers.Rackspace
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using global::OpenStack.Net;

    internal static class TestHelpers
    {
        public static void HandleBeforeAsyncWebRequest(object sender, HttpRequestEventArgs e)
        {
            HttpRequestMessage request = e.Request;

            Console.Error.WriteLine("{0} (Request) {1} {2}", DateTime.Now, e.Request.Method, e.Request.RequestUri);

            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            {
                Console.Error.WriteLine(string.Format("{0}: {1}", header.Key, string.Join(", ", header.Value)));
            }

            if (request.Content != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in request.Content.Headers)
                {
                    Console.Error.WriteLine(string.Format("{0}: {1}", header.Key, string.Join(", ", header.Value)));
                }

                Console.Error.WriteLine("<== " + Encoding.UTF8.GetString(request.Content.ReadAsByteArrayAsync().Result));
            }
        }

        public static void HandleAfterAsyncWebRequest(object sender, HttpResponseEventArgs e)
        {
            Console.Error.WriteLine("{0} (Result {1})", DateTime.Now, e.Response.StatusCode);
        }

        public static Task<Tuple<HttpResponseMessage, string>> ReadResult(Task<HttpResponseMessage> task, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>, CancellationToken, Task<Tuple<HttpResponseMessage, string>>> readResultImpl, bool reformat = true)
        {
            Task<Tuple<HttpResponseMessage, string>> result = readResultImpl(task, cancellationToken);
            LogResult(result.Result.Item1, result.Result.Item2, reformat);
            return result;
        }

        private static void LogResult(HttpResponseMessage response, string rawBody, bool reformat)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
            {
                Console.Error.WriteLine(string.Format("{0}: {1}", header.Key, string.Join(", ", header.Value)));
            }

            if (response.Content != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
                {
                    Console.Error.WriteLine(string.Format("{0}: {1}", header.Key, string.Join(", ", header.Value)));
                }
            }

            if (!string.IsNullOrEmpty(rawBody))
            {
                string formatted = rawBody;
                if (reformat)
                {
                    try
                    {
                        object parsed = JsonConvert.DeserializeObject(rawBody);
                        formatted = JsonConvert.SerializeObject(parsed, Formatting.Indented);
                    }
                    catch (JsonException)
                    {
                        // couldn't reformat as JSON
                    }
                }

                Console.Error.WriteLine("==> " + formatted);
            }
        }
    }
}
