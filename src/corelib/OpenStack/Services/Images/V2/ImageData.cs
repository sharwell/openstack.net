namespace OpenStack.Services.Images.V2
{
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageData : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageData()
        {
        }
    }
}
