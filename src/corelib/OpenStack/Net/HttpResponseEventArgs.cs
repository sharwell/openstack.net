namespace OpenStack.Net
{
    using System;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// This class contains data for events that happen in the context of a
    /// <see cref="WebResponse"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class HttpResponseEventArgs : EventArgs
    {
        /// <summary>
        /// This is the backing field for the <see cref="Response"/> property.
        /// </summary>
        private readonly HttpResponseMessage _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseEventArgs"/> class
        /// with the specified web response.
        /// </summary>
        /// <param name="response">The HTTP web response.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="response"/> is <see langword="null"/>.</exception>
        public HttpResponseEventArgs(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            _response = response;
        }

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> associated with the event.
        /// </summary>
        public HttpResponseMessage Response
        {
            get
            {
                return _response;
            }
        }
    }
}
