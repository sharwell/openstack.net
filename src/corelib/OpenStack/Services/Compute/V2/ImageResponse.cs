namespace OpenStack.Services.Compute.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    /// <summary>
    /// This class models the JSON representation used for the result of the API calls
    /// which return an <seealso cref="Image"/> resource.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageResponse : ExtensibleJsonObject
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        /// <summary>
        /// This is the backing field for the <see cref="Image"/> property.
        /// </summary>
        [JsonProperty("image", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private Image _image;
#pragma warning restore 649

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResponse"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageResponse()
        {
        }

        /// <summary>
        /// Gets an <see cref="Image"/> instance describing the image resource.
        /// </summary>
        /// <value>
        /// An <see cref="Image"/> instance describing the image resource.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if the JSON representation did not include the underlying property.</para>
        /// </value>
        public Image Image
        {
            get
            {
                return _image;
            }
        }
    }
}
