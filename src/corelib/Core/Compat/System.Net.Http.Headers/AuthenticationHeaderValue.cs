//
// AuthenticationHeaderValue.cs
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

using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>
	/// Represents authentication information in Authorization, ProxyAuthorization, WWW-Authenticate, and Proxy-Authenticate header values.
	/// </summary>
	/// <remarks>
	/// The <see cref="AuthenticationHeaderValue"/> class provides support for the Authorization, ProxyAuthorization, WWW-Authenticate, and Proxy-Authenticate HTTP header values as defined in RFC 2616 by the IETF.
	/// </remarks>
	/// <threadsafety static="true" instance="false"/>
	public class AuthenticationHeaderValue : ICloneable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticationHeaderValue"/> class.
		/// </summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		public AuthenticationHeaderValue (string scheme)
			: this (scheme, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticationHeaderValue"/> class.
		/// </summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
		public AuthenticationHeaderValue (string scheme, string parameter)
		{
			Parser.Token.Check (scheme);

			this.Scheme = scheme;
			this.Parameter = parameter;
		}

		private AuthenticationHeaderValue ()
		{
		}

		/// <summary>
		/// Gets the credentials containing the authentication information of the user agent for the resource being requested.
		/// </summary>
		/// <value>
		/// The credentials containing the authentication information.
		/// </value>
		public string Parameter { get; private set; }

		/// <summary>
		/// Gets the scheme to use for authorization.
		/// </summary>
		/// <value>
		/// The scheme to use for authorization.
		/// </value>
		public string Scheme { get; private set; }

		object ICloneable.Clone ()
		{
			return MemberwiseClone ();
		}

		/// <summary>
		/// Determines whether the specified Object is equal to the current <see cref="AuthenticationHeaderValue"/> object.
		/// </summary>
		/// <inheritdoc/>
		public override bool Equals (object obj)
		{
			var source = obj as AuthenticationHeaderValue;
			return source != null &&
				string.Equals (source.Scheme, Scheme, StringComparison.OrdinalIgnoreCase) &&
				source.Parameter == Parameter;
		}

		/// <summary>
		/// Serves as a hash function for an <see cref="AuthenticationHeaderValue"/> object.
		/// </summary>
		/// <inheritdoc/>
		public override int GetHashCode ()
		{
			int hc = Scheme.ToLowerInvariant ().GetHashCode ();
			if (!string.IsNullOrEmpty (Parameter)) {
				hc ^= Parameter.ToLowerInvariant ().GetHashCode ();
			}

			return hc;
		}

		/// <summary>
		/// Converts a string to an <see cref="AuthenticationHeaderValue"/> instance.
		/// </summary>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <returns>An <see cref="AuthenticationHeaderValue"/> instance.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="input"/> is <see langword="null"/>.</exception>
		/// <exception cref="FormatException">If <paramref name="input"/> is not valid authentication header value information.</exception>
		public static AuthenticationHeaderValue Parse (string input)
		{
			AuthenticationHeaderValue value;
			if (TryParse (input, out value))
				return value;

			throw new FormatException (input);
		}

		/// <summary>
		/// Determines whether a string is valid <see cref="AuthenticationHeaderValue"/> information.
		/// </summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="AuthenticationHeaderValue"/> version of the string.</param>
		/// <returns><see langword="true"/> if <paramref name="input"/> is valid <see cref="AuthenticationHeaderValue"/> information; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse (string input, out AuthenticationHeaderValue parsedValue)
		{
			var lexer = new Lexer (input);
			Token token;
			if (TryParseElement (lexer, out parsedValue, out token) && token == Token.Type.End)
				return true;

			parsedValue = null;
			return false;
		}

		internal static bool TryParse (string input, int minimalCount, out List<AuthenticationHeaderValue> result)
		{
			return CollectionParser.TryParse (input, minimalCount, TryParseElement, out result);
		}

		static bool TryParseElement (Lexer lexer, out AuthenticationHeaderValue parsedValue, out Token t)
		{
			t = lexer.Scan ();
			if (t != Token.Type.Token) {
				parsedValue = null;
				return false;
			}

			parsedValue = new AuthenticationHeaderValue ();
			parsedValue.Scheme = lexer.GetStringValue (t);

			t = lexer.Scan ();
			if (t == Token.Type.Token) {
				// TODO: Wrong with multi value parsing
				parsedValue.Parameter = lexer.GetRemainingStringValue (t.StartPosition);
				t = new Token (Token.Type.End, 0, 0);
			}

			return true;
		}

		/// <summary>
		/// Returns a string that represents the current <see cref="AuthenticationHeaderValue"/> object.
		/// </summary>
		/// <inheritdoc/>
		public override string ToString ()
		{
			return Parameter != null ?
				Scheme + " " + Parameter :
				Scheme;
		}
	}
}
