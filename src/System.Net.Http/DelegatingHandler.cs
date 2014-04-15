//
// DelegatingHandler.cs
//
// Authors:
//	Marek Safar  <marek.safar@gmail.com>
//
// Copyright (C) 2011 Xamarin Inc (http://www.xamarin.com)
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
	/// A type for HTTP handlers that delegate the processing of HTTP response messages to another handler, called the inner handler.
	/// </summary>
	/// <remarks>
	/// This application normally instantiate this class and then set the inner handler or provide an inner handler in the constructor.
	/// <para>
	/// Note that <see cref="InnerHandler"/> property may be a delegating handler too. This approach allows the creation of handler stacks to process the HTTP response messages.
	/// </para>
	/// </remarks>
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		bool disposed;

		/// <summary>
		/// Creates a new instance of the <see cref="DelegatingHandler"/> class.
		/// </summary>
		/// <remarks>
		/// The inner handle can be set using the <see cref="InnerHandler"/>.
		/// </remarks>
		protected DelegatingHandler ()
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="DelegatingHandler"/> class with a specific inner handler.
		/// </summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
			if (innerHandler == null)
				throw new ArgumentNullException ("innerHandler");
			
			InnerHandler = innerHandler;
		}

		/// <summary>
		/// Gets or sets the inner handler which processes the HTTP response messages.
		/// </summary>
		/// <remarks>
		/// This <see cref="InnerHandler"/> property can only be set before the class is used (the <see cref="SendAsync"/> method is called).
		/// <para>
		/// Note that <see cref="InnerHandler"/> property may be a delegating handler too, although this is uncommon. This approach allows the creation of handler stacks for the HTTP response messages.
		/// </para>
		/// </remarks>
		/// <value>
		/// The inner handler for HTTP response messages.
		/// </value>
		public HttpMessageHandler InnerHandler { get; set; }

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="DelegatingHandler"/>, and optionally disposes of the managed resources.
		/// </summary>
		/// <inheritdoc/>
		protected override void Dispose (bool disposing)
		{
			if (disposing && !disposed) {
				disposed = true;
				InnerHandler.Dispose ();
			}
			
			base.Dispose (disposing);
		}

		/// <summary>
		/// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
		/// </summary>
		/// <remarks>
		/// This operation does not block. This overridable implementation of <see cref="SendAsync"/> method forwards the HTTP request to the inner handler to send to the server as an asynchronous operation.
		/// <para>
		/// The <see cref="SendAsync"/> method is mainly used by the system and not by applications. When this method is called, it calls the <see cref="SendAsync"/> method on the inner handler.
		/// </para>
		/// </remarks>
		/// <inheritdoc/>
		protected internal override Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return InnerHandler.SendAsync (request, cancellationToken);
		}
	}
}
