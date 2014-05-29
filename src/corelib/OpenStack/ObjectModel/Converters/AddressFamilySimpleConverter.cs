namespace OpenStack.ObjectModel.Converters
{
#if PORTABLE
    using TAddressFamily = System.String;
#else
    using System;
    using AddressFamily = System.Net.Sockets.AddressFamily;
    using TAddressFamily = System.Nullable<System.Net.Sockets.AddressFamily>;
#endif

    /// <summary>
    /// This class supports deserialzing an <see cref="AddressFamily"/> from the
    /// form commonly used in OpenStack services. In particular, the text <c>ipv4</c>
    /// is deserialized to <see cref="AddressFamily.InterNetwork"/>, and <c>ipv6</c>
    /// is deserialized to <see cref="AddressFamily.InterNetworkV6"/> (both are
    /// case-insensitive).
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class AddressFamilySimpleConverter : SimpleStringJsonConverter<TAddressFamily>
    {
        /// <inheritdoc/>
        protected override TAddressFamily ConvertToObject(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

#if PORTABLE
            return str;
#else
            switch (str.ToLowerInvariant())
            {
            case "ipv4":
                return AddressFamily.InterNetwork;

            case "ipv6":
                return AddressFamily.InterNetworkV6;

            default:
                throw new NotSupportedException("Unsupported address family: " + str);
            }
#endif
        }
    }
}
