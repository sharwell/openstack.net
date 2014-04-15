//
// RangeItemHeaderValue.cs
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

namespace System.Net.Http.Headers
{
	/// <summary>
	/// Represents a byte range in a Range header value.
	/// </summary>
	/// <remarks>
	/// The <see cref="RangeHeaderValue"/> class provides support for a byte range in a Range header as defined in <see href="http://tools.ietf.org/html/rfc2616">RFC 2616</see> by the IETF.
	/// <para>A Range header can specify multiple byte ranges.</para>
	/// <para>An example of a byte-range in a Range header in an HTTP protocol request that requests the first 100 bytes would be would be the following:</para>
	/// <code>
	/// Range: bytes=0-99\r\n\r\n
	/// </code>
	/// <para>A HTTP server indicates support for Range headers with the Accept-Ranges header. An example of the Accept-Ranges header from a server that supports byte-ranges would be as follows:</para>
	/// <code>
	/// Accept-Ranges: bytes\r\n\r\n
	/// </code>
	/// <para>If an Accept-Ranges header is not received in the header of the response from the server, then the server does not support Range headers. An example of the Accept-Ranges header from a server that does not support ranges, but recognizes the Accept-Ranges header, would be as follows:</para>
	/// <code>
	/// Accept-Ranges: none\r\n\r\n
	/// </code>
	/// </remarks>
	/// <threadsafety static="true" instance="false"/>
	public class RangeItemHeaderValue : ICloneable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RangeItemHeaderValue"/> class.
		/// </summary>
		/// <remarks>
		/// An example of a byte-range in a Range header in an HTTP protocol request that requests the first 100 bytes would be would be the following:
		/// <code>
		/// Range: bytes=0-99\r\n\r\n
		/// </code>
		/// <para>For this example, the <paramref name="from"/> parameter would be specified as 0 and the <paramref name="to"/> parameter would be specified as 99.</para>
		/// </remarks>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// If <paramref name="from"/> is greater than <paramref name="to"/>.
		/// <para>-or-</para>
		/// <para>If <paramref name="from"/> or <paramref name="to"/> is less than 0.</para>
		/// </exception>
		public RangeItemHeaderValue (long? from, long? to)
		{
			if (from == null && to == null)
				throw new ArgumentException ();

			if (from != null && to != null && from > to) {
				throw new ArgumentOutOfRangeException ("from");
			}

			if (from < 0)
				throw new ArgumentOutOfRangeException ("from");

			if (to < 0)
				throw new ArgumentOutOfRangeException ("to");

			From = from;
			To = to;
		}

		/// <summary>
		/// Gets the position at which to start sending data.
		/// </summary>
		/// <value>
		/// The position at which to start sending data.
		/// </value>
		public long? From { get; private set; }

		/// <summary>
		/// Gets the position at which to stop sending data.
		/// </summary>
		/// <value>
		/// The position at which to stop sending data.
		/// </value>
		public long? To { get; private set; }

		object ICloneable.Clone ()
		{
			return MemberwiseClone ();
		}

		/// <inheritdoc/>
		public override bool Equals (object obj)
		{
			var source = obj as RangeItemHeaderValue;
			return source != null && source.From == From && source.To == To;
		}

		/// <inheritdoc/>
		public override int GetHashCode ()
		{
			return From.GetHashCode () ^ To.GetHashCode ();
		}

		/// <inheritdoc/>
		public override string ToString ()
		{
			if (From == null)
				return "-" + To.Value;

			if (To == null)
				return From.Value + "-";

			return From.Value + "-" + To.Value;
		}
	}
}
