namespace OpenStack.Services.Orchestration.V1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenStack.ObjectModel;

#warning not all properties have been added here

    [JsonObject(MemberSerialization.OptIn)]
    public class StackData : ExtensibleJsonObject
    {
        [JsonProperty("stack_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private StackName _name;

        [JsonProperty("template_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _templateUri;

        [JsonProperty("template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private StackTemplate _template;

        [JsonProperty("environment", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JToken _environment;

        [JsonProperty("files", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject _files;

        [JsonProperty("parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, string> _parameters;

        [JsonProperty("timeout_mins", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private int? _timeoutMins;

        [JsonProperty("disable_rollback", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? _disableRollback;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackData"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected StackData()
        {
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(JsonConvert.SerializeObject(environment)) : null, files, parameters, timeout, disableRollback);
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(JsonConvert.SerializeObject(environment)) : null, files, parameters, timeout, disableRollback);
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, TemplateEnvironment environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(JsonConvert.SerializeObject(environment)) : null, files, parameters, timeout, disableRollback);
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, string environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(environment) : null, files, parameters, timeout, disableRollback);
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, string environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, params JProperty[] extensionData)
            : base(extensionData)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(environment) : null, files, parameters, timeout, disableRollback);
        }

        public StackData(StackName name, Uri templateUri, StackTemplate template, string environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback, IDictionary<string, JToken> extensionData)
            : base(extensionData)
        {
            Initialize(name, templateUri, template, environment != null ? JValue.CreateString(environment) : null, files, parameters, timeout, disableRollback);
        }

        private void Initialize(StackName name, Uri templateUri, StackTemplate template, JToken environment, JObject files, IDictionary<string, string> parameters, TimeSpan? timeout, bool? disableRollback)
        {
            _name = name;
            _templateUri = templateUri != null ? templateUri.AbsoluteUri : null;
            _template = template;
            _environment = environment;
            _files = files;
            _parameters = parameters != null ? new Dictionary<string, string>(parameters) : null;
            _timeoutMins = timeout != null ? (int?)timeout.Value.TotalMinutes : null;
            _disableRollback = disableRollback;
        }

        public StackName Name
        {
            get
            {
                return _name;
            }
        }

        public Uri TemplateUri
        {
            get
            {
                if (_templateUri == null)
                    return null;

                return new Uri(_templateUri);
            }
        }

        public StackTemplate Template
        {
            get
            {
                return _template;
            }
        }

        public JToken Environment
        {
            get
            {
                return _environment;
            }
        }

        public JObject Files
        {
            get
            {
                return _files;
            }
        }

        public ReadOnlyDictionary<string, string> Parameters
        {
            get
            {
                if (_parameters == null)
                    return null;

                return new ReadOnlyDictionary<string, string>(_parameters);
            }
        }

        public TimeSpan? Timeout
        {
            get
            {
                if (_timeoutMins == null)
                    return null;

                return TimeSpan.FromMinutes(_timeoutMins.Value);
            }
        }

        public bool? DisableRollback
        {
            get
            {
                return _disableRollback;
            }
        }
    }
}
