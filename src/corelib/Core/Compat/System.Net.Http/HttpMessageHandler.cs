//
// HttpMessageHandler.cs
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
	/// A base type for HTTP message handlers.
	/// </summary>
	public abstract class HttpMessageHandler : IDisposable
	{
		/// <inheritdoc/>
		public void Dispose ()
		{
			Dispose (true);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="HttpMessageHandler"/> and optionally disposes of the managed resources.
		/// </summary>
		/// <remarks>
		/// This method is called by the public <see cref="Dispose()"/> method and the Finalize method. <see cref="Dispose()"/>
		/// invokes the protected <see cref="Dispose(Boolean)"/> method with the <paramref name="disposing"/> parameter set to
		/// <see langword="true"/>. Finalize invokes <see cref="Dispose(Boolean)"/> with <paramref name="disposing"/> set to
		/// <see langword="false"/>. When the <paramref name="disposing"/> parameter is <see langword="true"/>, this method
		/// releases all resources held by any managed objects that this <see cref="HttpMessageHandler"/> references. This
		/// method invokes the <see cref="Dispose()"/> method of each referenced object.
		/// </remarks>
		/// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to releases only unmanaged resources.</param>
		protected virtual void Dispose (bool disposing)
		{
		}

		/// <summary>
		/// Send an HTTP request as an asynchronous operation.
		/// </summary>
		/// <remarks>
		/// This operation will not block. The returned <see cref="Task{TResult}"/> object will complete once the entire response including content is read.
		/// <para>
		/// The <see cref="SendAsync"/> method is used primarily by the system. This method is called by the system one of the <see cref="O:System.Net.Http.HttpClient.SendAsync"/> methods is called. Most apps will never call this method.
		/// </para>
		/// </remarks>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="request"/> is <see langword="null"/>.</exception>
		protected internal abstract Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken);
	}
}
