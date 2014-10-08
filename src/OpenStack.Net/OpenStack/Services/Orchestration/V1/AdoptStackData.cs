namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class models the JSON object used in the <see cref="AdoptStackApiCall"/> HTTP API call, which extends the
    /// <see cref="StackData"/> model by providing a property to specify the existing resources which are adopted by a
    /// new <see cref="Stack"/> resource in the OpenStack Orchestration Service.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class AdoptStackData : StackData
    {
        /// <summary>
        /// This is the backing field for the <see cref="AdoptStack"/> property.
        /// </summary>
        [JsonProperty("adopt_stack_data", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _adoptStackData;

        /// <summary>
        /// Initializes a new instance of the <see cref="V1.AdoptStackData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected AdoptStackData()
        {
        }

        public AdoptStackData(StackName name, Stack abandonedStack)
            : base(name, abandonedStack.TemplateUri, abandonedStack.Template, abandonedStack.Environment != null ? JsonConvert.SerializeObject(abandonedStack.Environment) : null, abandonedStack.Files, abandonedStack.Parameters, abandonedStack.Timeout, abandonedStack.DisableRollback)
        {
            _adoptStackData = JsonConvert.SerializeObject(abandonedStack);
        }

        public AdoptStackData(StackName name, string adoptStackData, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback)
            : base(name, templateUri, template, environment, files, parameters, timeout, disableRollback)
        {
            _adoptStackData = adoptStackData;
        }

        public AdoptStackData(StackName name, string adoptStackData, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, params JProperty[] extensionData)
            : base(name, templateUri, template, environment, files, parameters, timeout, disableRollback, extensionData)
        {
            _adoptStackData = adoptStackData;
        }

        public AdoptStackData(StackName name, string adoptStackData, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, IDictionary<string, JToken> extensionData)
            : base(name, templateUri, template, environment, files, parameters, timeout, disableRollback, extensionData)
        {
            _adoptStackData = adoptStackData;
        }

        /// <summary>
        /// Gets the representation of existing resources which should be used to create the new <see cref="Stack"/>.
        /// </summary>
        /// <remarks>
        /// <para>This value is typically obtained as the response body from the <see cref="AbandonStackApiCall"/> HTTP
        /// API call.</para>
        /// </remarks>
        /// <value>
        /// <para>The representation of existing resources which should be used to create the new
        /// <see cref="Stack"/>.</para>
        /// <token>NullIfNotIncluded</token>
        /// </value>
        public string AdoptStack
        {
            get
            {
                return _adoptStackData;
            }
        }
    }
}
