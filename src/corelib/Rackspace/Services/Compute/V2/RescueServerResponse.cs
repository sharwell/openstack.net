namespace Rackspace.Services.Compute.V2
{
    using OpenStack.ObjectModel;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation used by the <see cref="RescueServerApiCall"/>
    /// API call.
    /// </summary>
    /// <seealso cref="RescueExtensions.PrepareRescueServerAsync"/>
    /// <seealso cref="RescueExtensions.RescueServerAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class RescueServerResponse : ExtensibleJsonObject
    {
        /// <summary>
        /// This is the backing field for the <see cref="AdministratorPassword"/> property.
        /// </summary>
        [JsonProperty("adminPass", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _administratorPassword;

        /// <summary>
        /// Initializes a new instance of the <see cref="RescueServerResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected RescueServerResponse()
        {
        }

        /// <summary>
        /// Gets the temporary root password assigned for use during rescue mode.
        /// </summary>
        /// <value>
        /// The temporary root password assigned for use during rescue mode.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public string AdministratorPassword
        {
            get
            {
                return _administratorPassword;
            }
        }
    }
}
