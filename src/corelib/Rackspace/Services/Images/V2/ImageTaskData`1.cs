namespace Rackspace.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageTaskData<TInput> : ExtensibleJsonObject
    {
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private ImageTaskType _type;

        [JsonProperty("input", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private TInput _input;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTaskData{TInput}"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImageTaskData()
        {
        }

        public ImageTaskData(ImageTaskType type, TInput input)
        {
            _type = type;
            _input = input;
        }

        public ImageTaskData(ImageTaskType type, TInput input, params JProperty[] extensionData)
            : base(extensionData)
        {
            _type = type;
            _input = input;
        }

        public ImageTaskData(ImageTaskType type, TInput input, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            _type = type;
            _input = input;
        }

        public ImageTaskType Type
        {
            get
            {
                return _type;
            }
        }

        public TInput Input
        {
            get
            {
                return _input;
            }
        }
    }
}
