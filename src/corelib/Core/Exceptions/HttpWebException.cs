namespace net.openstack.Core.Exceptions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when an <see cref="HttpResponseMessage"/> indicates
    /// a <see cref="WebExceptionStatus.ProtocolError"/> occurred during an HTTP request
    /// sent using <see cref="HttpClient"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [Serializable]
    public class HttpWebException : WebException
    {
        [NonSerialized]
        private ExceptionData _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessage"/> class
        /// with the specified response message.
        /// </summary>
        /// <param name="response">The response to the web request. In most cases, the <see cref="HttpResponseMessage.IsSuccessStatusCode"/> property will return <see langword="false"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="response"/> is <see langword="null"/>.</exception>
        public HttpWebException(HttpResponseMessage response)
            : base(response.ReasonPhrase, WebExceptionStatus.ProtocolError)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            _state.ResponseMessage = response;
#if !NET35
            SerializeObjectState += (ex, args) => args.AddSerializedState(_state);
#endif
        }

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> for the web request.
        /// </summary>
        public HttpResponseMessage ResponseMessage
        {
            get
            {
                return _state.ResponseMessage;
            }
        }

        [Serializable]
        private struct ExceptionData : ISafeSerializationData
        {
            public HttpResponseMessage ResponseMessage
            {
                get;
                set;
            }

            void ISafeSerializationData.CompleteDeserialization(object deserialized)
            {
                ((HttpWebException)deserialized)._state = this;
            }
        }
    }
}
