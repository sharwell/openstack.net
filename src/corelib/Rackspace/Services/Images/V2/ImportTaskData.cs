namespace Rackspace.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class ImportTaskData : ImageTaskData<ImportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTaskData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ImportTaskData()
        {
        }

        public ImportTaskData(ImportTaskInput input)
            : base(ImageTaskType.Import, input)
        {
        }

        public ImportTaskData(ImportTaskInput input, params JProperty[] extensionData)
            : base(ImageTaskType.Import, input, extensionData)
        {
        }

        public ImportTaskData(ImportTaskInput input, IDictionary<string, JToken> extensionData)
            : base(ImageTaskType.Import, input, extensionData)
        {
        }
    }
}
