//
// HttpMethod.cs
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

namespace System.Net.Http
{
	/// <summary>
	/// A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods.
	/// </summary>
	/// <threadsafety static="true" instance="false"/>
	public class HttpMethod : IEquatable<HttpMethod>
	{
		static readonly HttpMethod delete_method = new HttpMethod ("DELETE");
		static readonly HttpMethod get_method = new HttpMethod ("GET");
		static readonly HttpMethod head_method = new HttpMethod ("HEAD");
		static readonly HttpMethod options_method = new HttpMethod ("OPTIONS");
		static readonly HttpMethod post_method = new HttpMethod ("POST");
		static readonly HttpMethod put_method = new HttpMethod ("PUT");
		static readonly HttpMethod trace_method = new HttpMethod ("TRACE");

		readonly string method;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpMethod"/> class with a specific HTTP method.
		/// </summary>
		/// <remarks>
		/// If an app needs a different value for the HTTP method from one of the static properties, the <see cref="HttpMethod"/> constructor initializes a new instance of the <see cref="HttpMethod"/> with an HTTP method that the app specifies.
		/// </remarks>
		/// <param name="method">The HTTP method.</param>
		public HttpMethod (string method)
		{
			if (string.IsNullOrEmpty (method))
				throw new ArgumentException ("method");

			Headers.Parser.Token.Check (method);

			this.method = method;
		}

		/// <summary>
		/// Represents an HTTP DELETE protocol method.
		/// </summary>
		public static HttpMethod Delete {
			get {
				return delete_method;
			}
		}

		/// <summary>
		/// Represents an HTTP GET protocol method.
		/// </summary>
		public static HttpMethod Get {
			get {
				return get_method;
			}
		}

		/// <summary>
		/// Represents an HTTP HEAD protocol method.
		/// </summary>
		public static HttpMethod Head {
			get {
				return head_method;
			}
		}

		/// <summary>
		/// Represents an HTTP METHOD protocol method.
		/// </summary>
		public string Method {
			get {
				return method;
			}
		}

		/// <summary>
		/// Represents an HTTP OPTIONS protocol method.
		/// </summary>
		public static HttpMethod Options {
			get {
				return options_method;
			}
		}

		/// <summary>
		/// Represents an HTTP POST protocol method.
		/// </summary>
		public static HttpMethod Post {
			get {
				return post_method;
			}
		}

		/// <summary>
		/// Represents an HTTP PUT protocol method.
		/// </summary>
		public static HttpMethod Put {
			get {
				return put_method;
			}
		}

		/// <summary>
		/// Represents an HTTP TRACE protocol method.
		/// </summary>
		public static HttpMethod Trace {
			get {
				return trace_method;
			}
		}

		/// <summary>
		/// The equality operator for comparing two <see cref="HttpMethod"/> objects.
		/// </summary>
		/// <param name="left">The left <see cref="HttpMethod"/> to an equality operator.</param>
		/// <param name="right">The right <see cref="HttpMethod"/> to an equality operator.</param>
		/// <returns><see langword="true"/> if the specified <paramref name="left"/> and <paramref name="right"/> parameters are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator == (HttpMethod left, HttpMethod right)
		{
			if ((object) left == null || (object) right == null)
				return ReferenceEquals (left, right);

			return left.Equals (right);
		}

		/// <summary>
		/// The inequality operator for comparing two <see cref="HttpMethod"/> objects.
		/// </summary>
		/// <param name="left">The left <see cref="HttpMethod"/> to an inequality operator.</param>
		/// <param name="right">The right <see cref="HttpMethod"/> to an inequality operator.</param>
		/// <returns><see langword="true"/> if the specified <paramref name="left"/> and <paramref name="right"/> parameters are inequal; otherwise, <see langword="false"/>.</returns>
		public static bool operator != (HttpMethod left, HttpMethod right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Determines whether the specified <see cref="HttpMethod"/> is equal to the current <see cref="Object"/>.
		/// </summary>
		/// <param name="other">The HTTP method to compare with the current object.</param>
		/// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
		public bool Equals(HttpMethod other)
		{
			return string.Equals (method, other.method, StringComparison.OrdinalIgnoreCase);
		}

		/// <inheritdoc/>
		public override bool Equals (object obj)
		{
			var other = obj as HttpMethod;
			return !ReferenceEquals (other, null) && Equals (other);
		}

		/// <inheritdoc/>
		public override int GetHashCode ()
		{
			return method.GetHashCode ();
		}

		/// <inheritdoc/>
		public override string ToString ()
		{
			return method;
		}
	}
}
