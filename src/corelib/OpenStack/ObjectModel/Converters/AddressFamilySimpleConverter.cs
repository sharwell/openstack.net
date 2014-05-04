namespace OpenStack.ObjectModel.Converters
{
#if PORTABLE
    using TAddressFamily = System.String;
#else
    using System;
    using AddressFamily = System.Net.Sockets.AddressFamily;
    using TAddressFamily = System.Nullable<System.Net.Sockets.AddressFamily>;
#endif

    public class AddressFamilySimpleConverter : SimpleStringJsonConverter<TAddressFamily>
    {
        protected override TAddressFamily ConvertToObject(string str)
        {
#if PORTABLE
            return str;
#else
            switch (str)
            {
            case "ipv4":
                return AddressFamily.InterNetwork;

            case "ipv6":
                return AddressFamily.InterNetworkV6;

            default:
                if (string.IsNullOrEmpty(str))
                    return null;

                throw new NotSupportedException("Unsupported address family: " + str);
            }
#endif
        }
    }
}
