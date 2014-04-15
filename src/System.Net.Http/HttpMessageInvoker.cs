//
// HttpMessageInvoker.cs
//
// Authors:
//	Marek Safar  <marek.safar@gmail.com>
//
// Copyright (C) 2012 Xamarin Inc (http://www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>
	/// A specialty class that allows applications to call the <see cref="SendAsync"/> method on an Http handler chain.
	/// </summary>
	/// <remarks>
	/// This class is the base type for <see cref="HttpClient"/> and other message originators.
	/// <para>
	/// Most applications that are connecting to a web site will use one of the <see cref="O:System.Net.Http.HttpClient.SendAsync"/> methods on the <see cref="HttpClient"/> class.
	/// </para>
	/// </remarks>
	/// <threadsafety static="true" instance="false"/>
	public class HttpMessageInvoker : IDisposable
	{
		HttpMessageHandler handler;
		readonly bool disposeHandler;

		/// <summary>
		/// Initializes an instance of a <see cref="HttpMessageInvoker"/> class with a specific <see cref="HttpMessageHandler"/>.
		/// </summary>
		/// <param name="handler">The <see cref="HttpMessageHandler"/> responsible for processing the HTTP response messages.</param>
		public HttpMessageInvoker (HttpMessageHandler handler)
			: this (handler, true)
		{
		}

		/// <summary>
		/// Initializes an instance of a <see cref="HttpMessageInvoker"/> class with a specific <see cref="HttpMessageHandler"/>.
		/// </summary>
		/// <param name="handler">The <see cref="HttpMessageHandler"/> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler"><see langword="true"/> if the inner handler should be disposed of by <see cref="HttpMessageHandler.Dispose()"/>, <see langword="false"/> if you intend to reuse the inner handler.</param>
		public HttpMessageInvoker (HttpMessageHandler handler, bool disposeHandler)
		{
			if (handler == null)
				throw new ArgumentNullException ("handler");

			this.handler = handler;
			this.disposeHandler = disposeHandler;
		}

		/// <summary>
		/// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpMessageInvoker"/>.
		/// </summary>
		public void Dispose ()
		{
			Dispose (true);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="HttpMessageInvoker"/> and optionally disposes of the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to releases only unmanaged resources.</param>
		protected virtual void Dispose (bool disposing)
		{
			if (disposing && disposeHandler && handler != null) {
				handler.Dispose ();
				handler = null;
			}
		}

		/// <summary>
		/// Send an HTTP request as an asynchronous operation.
		/// </summary>
		/// <remarks>
		/// This operation will not block. The returned <see cref="Task{TResult}"/> object will complete once the entire response including content is read.
		/// <para>
		/// Most applications that are connecting to a web site will use one of the <see cref="O:System.Net.Http.HttpClient.SendAsync"/> methods on the <see cref="HttpClient"/> class.
		/// </para>
		/// </remarks>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="request"/> is <see langword="null"/>.</exception>
		public virtual Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return handler.SendAsync (request, cancellationToken);
		}
	}
}
