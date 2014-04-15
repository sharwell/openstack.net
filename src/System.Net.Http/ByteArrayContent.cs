//
// ByteArrayContent.cs
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

using System.IO;
using System.Threading.Tasks;
using OpenStack.Threading;

namespace System.Net.Http
{
	/// <summary>
	/// Provides HTTP content based on a byte array.
	/// </summary>
	/// <threadsafety static="true" instance="false"/>
	public class ByteArrayContent: HttpContent
	{
		readonly byte[] content;
		readonly int offset, count;

		/// <summary>
		/// Initializes a new instance of the <see cref="ByteArrayContent"/> class.
		/// </summary>
		/// <param name="content">The content used to initialize the <see cref="ByteArrayContent"/>.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
		public ByteArrayContent (byte[] content)
		{
			if (content == null)
				throw new ArgumentNullException ("content");

			this.content = content;
			this.count = content.Length;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ByteArrayContent"/> class.
		/// </summary>
		/// <remarks>
		/// Only the range specified by the <paramref name="offset"/> parameter and the <paramref name="count"/> parameter is used as content.
		/// </remarks>
		/// <param name="content">The content used to initialize the <see cref="ByteArrayContent"/>.</param>
		/// <param name="offset">The offset, in bytes, in the <paramref name="content"/> parameter used to initialize the <see cref="ByteArrayContent"/>.</param>
		/// <param name="count">The number of bytes in the <paramref name="content"/> starting from the <paramref name="offset"/> parameter used to initialize the <see cref="ByteArrayContent"/>.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// If <paramref name="offset"/> is less than zero.
		/// <para>-or-</para>
		/// <para>If <paramref name="offset"/> is greater than the length of <paramref name="content"/>.</para>
		/// <para>-or-</para>
		/// <para>If <paramref name="count"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para>If <paramref name="count"/> is greater than the length of <paramref name="content"/> minus <paramref name="offset"/>.</para>
		/// </exception>
		public ByteArrayContent (byte[] content, int offset, int count)
			: this (content)
		{
			if (offset < 0 || offset > this.count)
				throw new ArgumentOutOfRangeException ("offset");

			if (count < 0 || count > (this.count - offset))
				throw new ArgumentOutOfRangeException ("count");

			this.offset = offset;
			this.count = count;
		}

		/// <summary>
		/// Creates an HTTP content stream as an asynchronous operation for reading whose backing store is memory from the <see cref="ByteArrayContent"/>.
		/// </summary>
		/// <inheritdoc/>
		protected override Task<Stream> CreateContentReadStreamAsync ()
		{
			return CompletedTask.FromResult<Stream> (new MemoryStream (content, offset, count));
		}

		/// <summary>
		/// Serialize and write the byte array provided in the constructor to an HTTP content stream as an asynchronous operation.
		/// </summary>
		/// <inheritdoc/>
		protected internal override Task SerializeToStreamAsync (Stream stream, TransportContext context)
		{
			return stream.WriteAsync (content, offset, count);
		}

		/// <summary>
		/// Determines whether a byte array has a valid length in bytes.
		/// </summary>
		/// <remarks>
		/// This method always returns <see langword="true"/> for <see cref="ByteArrayContent"/>.
		/// </remarks>
		/// <param name="length">The length in bytes of the byte array.</param>
		/// <inheritdoc/>
		protected internal override bool TryComputeLength (out long length)
		{
			length = count;
			return true;
		}
	}
}
