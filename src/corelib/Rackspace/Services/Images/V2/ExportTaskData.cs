namespace Rackspace.Services.Images.V2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class ExportTaskData : ImageTaskData<ExportTaskInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTaskData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected ExportTaskData()
        {
        }

        public ExportTaskData(ExportTaskInput input)
            : base(ImageTaskType.Export, input)
        {
        }

        public ExportTaskData(ExportTaskInput input, params JProperty[] extensionData)
            : base(ImageTaskType.Export, input, extensionData)
        {
        }

        public ExportTaskData(ExportTaskInput input, IDictionary<string, JToken> extensionData)
            : base(ImageTaskType.Export, input, extensionData)
        {
        }
    }
}
