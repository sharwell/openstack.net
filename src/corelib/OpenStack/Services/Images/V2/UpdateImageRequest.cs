namespace OpenStack.Services.Images.V2
{
    using System;
    using Newtonsoft.Json;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class UpdateImageRequest : ExtensibleJsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateImageRequest"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected UpdateImageRequest()
        {
        }

        public UpdateImageRequest(ImageData imageData)
        {
            throw new NotImplementedException();
        }
    }
}
