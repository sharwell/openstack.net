namespace Rackspace.Services.AutoScale.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;


    /// <summary>
    /// This class represents the launch configuration for a scaling group in the <see cref="IAutoScaleService"/>.
    /// </summary>
    /// <remarks>
    /// A launch configuration defines what to do when a new server is created, including information
    /// about the server image, the flavor of the server image, and the load balancer to which to
    /// connect. Currently, the only supported <see cref="LaunchType"/> is <see cref="V1.LaunchType.LaunchServer"/>.
    /// </remarks>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(LaunchConfiguration.Converter))]
    public abstract class LaunchConfiguration : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="LaunchType"/> property.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private LaunchType _launchType;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected LaunchConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration"/> class
        /// with the specified launch type.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        protected LaunchConfiguration(LaunchType launchType)
        {
            _launchType = launchType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration"/> class
        /// with the specified launch type and extension data.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="extensionData"/> contains any <see langword="null"/> values.</exception>
        protected LaunchConfiguration(LaunchType launchType, params JProperty[] extensionData)
            : base(extensionData)
        {
            _launchType = launchType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchConfiguration"/> class
        /// with the specified launch type and extension data.
        /// </summary>
        /// <param name="launchType">The server launch type.</param>
        /// <param name="extensionData">The extension data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="extensionData"/> is <see langword="null"/>.</exception>
        protected LaunchConfiguration(LaunchType launchType, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _launchType = launchType;
        }

        /// <summary>
        /// Gets the launch type for the Auto Scale launch configuration.
        /// </summary>
        public virtual LaunchType LaunchType
        {
            get
            {
                return _launchType;
            }
        }

        /// <summary>
        /// Deserializes a JSON object to a <see cref="LaunchConfiguration"/> instance of the proper type.
        /// </summary>
        /// <param name="jsonObject">The JSON object representing the launch configuration.</param>
        /// <returns>A <see cref="LaunchConfiguration"/> object corresponding to the JSON object.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="jsonObject"/> is <see langword="null"/>.</exception>
        public static LaunchConfiguration FromJObject(JObject jsonObject)
        {
            if (jsonObject == null)
                throw new ArgumentNullException("jsonObject");

            JToken launchType = jsonObject["type"];
            if (launchType == null || launchType.ToObject<LaunchType>() == LaunchType.LaunchServer)
                return jsonObject.ToObject<ServerLaunchConfiguration>();

            return jsonObject.ToObject<GenericLaunchConfiguration>();
        }

        /// <summary>
        /// This implementation of <see cref="JsonConverter"/> allows for JSON serialization
        /// and deserialization of <see cref="IPAddress"/> objects in the "address details"
        /// format used by operations such as <see cref="IComputeProvider.ListAddresses"/>
        /// and  <see cref="IComputeProvider.ListAddressesByNetwork"/>.
        /// </summary>
        /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/List_Addresses-d1e3014.html">List Addresses (OpenStack Compute API v2 and Extensions Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-compute/2/content/List_Addresses_by_Network-d1e3118.html">List Addresses by Network (OpenStack Compute API v2 and Extensions Reference)</seealso>
        /// <threadsafety static="true" instance="false"/>
        public class IPAddressDetailsConverter : JsonConverter
        {
            /// <remarks>
            /// Serialization is performed by creating an <see cref="AddressDetails"/> instance
            /// equivalent to the given <see cref="IPAddress"/> instance and serializing that as
            /// a JSON object.
            /// </remarks>
            /// <inheritdoc/>
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                IPAddress address = value as IPAddress;
                if (address == null)
                    throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Unexpected value when converting IP address. Expected {0}, got {1}.", typeof(IPAddress), value.GetType()));

                AddressDetails details = new AddressDetails(address);
                serializer.Serialize(writer, details);
            }

            /// <remarks>
            /// Deserialization is performed by deserializing the JSON value as an <see cref="AddressDetails"/>
            /// object, following by using <see cref="IPAddress.Parse"/> to convert the value of
            /// <see cref="AddressDetails.Address"/> to an <see cref="IPAddress"/> instance.
            /// </remarks>
            /// <inheritdoc/>
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (objectType != typeof(IPAddress))
                    throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Expected target type {0}, found {1}.", typeof(IPAddress), objectType));

                AddressDetails details = serializer.Deserialize<AddressDetails>(reader);
                if (details == null)
                    return null;

                return IPAddress.Parse(details.Address);
            }

            /// <returns><see langword="true"/> if <paramref name="objectType"/> equals <see cref="IPAddress"/>; otherwise, <see langword="false"/>.</returns>
            /// <inheritdoc/>
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(IPAddress);
            }

            /// <summary>
            /// Represents a network address in a format compatible with communication with the Compute Service APIs.
            /// </summary>
            /// <seealso cref="IComputeProvider.ListAddresses"/>
            /// <seealso cref="IComputeProvider.ListAddressesByNetwork"/>
            /// <threadsafety static="true" instance="false"/>
            [JsonObject(MemberSerialization.OptIn)]
            protected class AddressDetails
            {
                /// <summary>
                /// Gets the network address. This is an IPv4 address if <see cref="Version"/> is <c>"4"</c>,
                /// or an IPv6 address if <see cref="Version"/> is <c>"6"</c>.
                /// </summary>
                [JsonProperty("addr")]
                public string Address
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the network address version. The value is either <c>"4"</c> or <c>"6"</c>.
                /// </summary>
                [JsonProperty("version")]
                public string Version
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="AddressDetails"/> class.
                /// </summary>
                /// <remarks>
                /// This constructor is used for JSON deserialization.
                /// </remarks>
                [JsonConstructor]
                protected AddressDetails()
                {
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="AddressDetails"/> class
                /// using the given IP address.
                /// </summary>
                /// <param name="address">The IP address.</param>
                /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
                public AddressDetails(IPAddress address)
                {
                    if (address == null)
                        throw new ArgumentNullException("address");

                    Address = address.ToString();
                    switch (address.AddressFamily)
                    {
                    case AddressFamily.InterNetwork:
                        Version = "4";
                        break;

                    case AddressFamily.InterNetworkV6:
                        Version = "6";
                        break;

                    default:
                        throw new ArgumentException("The specified address family is not supported.");
                    }
                }
            }
        }
    }
}
