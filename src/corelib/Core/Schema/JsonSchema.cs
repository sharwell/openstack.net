namespace net.openstack.Core.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using net.openstack.Core.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class JsonSchema
    {
        [JsonProperty("id")]
        private string _id;

        [JsonProperty("$schema")]
        private string _schema;

        [JsonProperty("title")]
        private string _title;

        [JsonProperty("description")]
        private string _description;

        [JsonProperty("default")]
        private JToken _default;

        [JsonProperty("multipleOf")]
        private JValue _multipleOf;

        [JsonProperty("maximum")]
        private JValue _maximum;

        [JsonProperty("exclusiveMaximum")]
        private bool? _exclusiveMaximum;

        [JsonProperty("minimum")]
        private JValue _minimum;

        [JsonProperty("exclusiveMinimum")]
        private bool? _exclusiveMinimum;

        [JsonProperty("maxLength")]
        private int? _maxLength;

        [JsonProperty("minLength")]
        private int? _minLength;

        [JsonProperty("pattern")]
        private string _pattern;

        [JsonProperty("additionalItems")]
        private JToken _additionalItems;

        [JsonProperty("items")]
        private JToken _items;

        [JsonProperty("maxItems")]
        private int? _maxItems;

        [JsonProperty("minItems")]
        private int? _minItems;

        [JsonProperty("uniqueItems")]
        private bool? _uniqueItems;

        [JsonProperty("maxProperties")]
        private int? _maxProperties;

        [JsonProperty("minProperties")]
        private int? _minProperties;

        [JsonProperty("required")]
        private string[] _required;

        [JsonProperty("additionalProperties")]
        private JToken _additionalProperties;

        [JsonProperty("definitions")]
        private Dictionary<string, JsonSchema> _definitions;

        [JsonProperty("properties")]
        private Dictionary<string, JsonSchema> _properties;

        [JsonProperty("patternProperties")]
        private Dictionary<string, JsonSchema> _patternProperties;

        [JsonProperty("dependencies")]
        private Dictionary<string, JToken> _dependencies;

        [JsonProperty("enum")]
        private JToken[] _enum;

        [JsonProperty("type")]
        private JToken _type;

        [JsonProperty("allOf")]
        private JsonSchema[] _allOf;

        [JsonProperty("anyOf")]
        private JsonSchema[] _anyOf;

        [JsonProperty("oneOf")]
        private JsonSchema[] _oneOf;

        [JsonProperty("not")]
        private JsonSchema _not;

        #region Hyper Schema Additional Properties

        [JsonProperty("links")]
        private LinkDescription[] _links;

        [JsonProperty("fragmentResolution")]
        private string _fragmentResolution;

        [JsonProperty("media")]
        private MediaDescription _media;

        [JsonProperty("pathStart")]
        private string _pathStart;

        #endregion Hyper Schema Additional Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSchema"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected JsonSchema()
        {
        }

        public static JsonSchema Empty
        {
            get
            {
                return new JsonSchema();
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
        }

        public Uri Schema
        {
            get
            {
                if (_schema == null)
                    return null;

                return new Uri(_schema, UriKind.Absolute);
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        private JToken Default
        {
            get
            {
                return _default;
            }
        }

        public JValue MultipleOf
        {
            get
            {
                return _multipleOf;
            }
        }

        public JValue Maximum
        {
            get
            {
                return _maximum;
            }
        }

        public bool ExclusiveMaximum
        {
            get
            {
                return _exclusiveMaximum ?? false;
            }
        }

        public JValue Minimum
        {
            get
            {
                return _minimum;
            }
        }

        public bool ExclusiveMinimum
        {
            get
            {
                return _exclusiveMinimum ?? false;
            }
        }

        public int? MaxLength
        {
            get
            {
                return _maxLength;
            }
        }

        public int MinLength
        {
            get
            {
                return _minLength ?? 0;
            }
        }

        public string Pattern
        {
            get
            {
                return _pattern;
            }
        }

        public bool AllowAdditionalItems
        {
            get
            {
                if (_additionalItems == null)
                    return false;

                if (_additionalItems is JObject)
                    return true;

                return _additionalItems.ToObject<bool>();
            }
        }

        public JsonSchema AdditionalItemsSchema
        {
            get
            {
                JObject obj = _additionalItems as JObject;
                if (obj != null)
                    return obj.ToObject<JsonSchema>();

                if (AllowAdditionalItems)
                    return Empty;

                return null;
            }
        }

        public ReadOnlyCollection<JsonSchema> Items
        {
            get
            {
                if (_items == null)
                    return null;

                if (_items is JArray)
                    return _type.ToObject<ReadOnlyCollection<JsonSchema>>();

                return new ReadOnlyCollection<JsonSchema>(new[] { _type.ToObject<JsonSchema>() });
            }
        }

        public int? MaxItems
        {
            get
            {
                return _maxItems;
            }
        }

        public int MinItems
        {
            get
            {
                return _minItems ?? 0;
            }
        }

        public bool UniqueItems
        {
            get
            {
                return _uniqueItems ?? false;
            }
        }

        public int? MaxProperties
        {
            get
            {
                return _maxProperties;
            }
        }

        public int MinProperties
        {
            get
            {
                return _minProperties ?? 0;
            }
        }

        public ReadOnlyCollection<string> Required
        {
            get
            {
                if (_required == null)
                    return new ReadOnlyCollection<string>(new string[0]);

                return new ReadOnlyCollection<string>(_required);
            }
        }

        public bool AllowAdditionalProperties
        {
            get
            {
                if (_additionalProperties is JValue)
                    return _additionalProperties.ToObject<bool>();

                return _additionalProperties is JObject;
            }
        }

        public JsonSchema AdditionalPropertiesSchema
        {
            get
            {
                JObject obj = _additionalProperties as JObject;
                if (obj != null)
                    return obj.ToObject<JsonSchema>();

                if (AllowAdditionalProperties)
                    return Empty;

                return null;
            }
        }

        public ReadOnlyDictionary<string, JsonSchema> Definitions
        {
            get
            {
                if (_definitions == null)
                    return new ReadOnlyDictionary<string, JsonSchema>(new Dictionary<string, JsonSchema>());

                return new ReadOnlyDictionary<string, JsonSchema>(_definitions);
            }
        }

        public ReadOnlyDictionary<string, JsonSchema> Properties
        {
            get
            {
                if (_properties == null)
                    return new ReadOnlyDictionary<string, JsonSchema>(new Dictionary<string, JsonSchema>());

                return new ReadOnlyDictionary<string, JsonSchema>(_properties);
            }
        }

        public ReadOnlyDictionary<string, JsonSchema> PatternProperties
        {
            get
            {
                if (_patternProperties == null)
                    return new ReadOnlyDictionary<string, JsonSchema>(new Dictionary<string, JsonSchema>());

                return new ReadOnlyDictionary<string, JsonSchema>(_patternProperties);
            }
        }

        private ReadOnlyDictionary<string, JToken> Dependencies
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private ReadOnlyCollection<JToken> Enum
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<SimpleType> Type
        {
            get
            {
                if (_type == null)
                    return null;

                if (_type is JArray)
                    return _type.ToObject<ReadOnlyCollection<SimpleType>>();

                return new ReadOnlyCollection<SimpleType>(new[] { _type.ToObject<SimpleType>() });
            }
        }

        public ReadOnlyCollection<JsonSchema> AllOf
        {
            get
            {
                if (_allOf == null)
                    return null;

                return new ReadOnlyCollection<JsonSchema>(_allOf);
            }
        }

        public ReadOnlyCollection<JsonSchema> AnyOf
        {
            get
            {
                if (_anyOf == null)
                    return null;

                return new ReadOnlyCollection<JsonSchema>(_anyOf);
            }
        }

        public ReadOnlyCollection<JsonSchema> OneOf
        {
            get
            {
                if (_oneOf == null)
                    return null;

                return new ReadOnlyCollection<JsonSchema>(_oneOf);
            }
        }

        public JsonSchema Not
        {
            get
            {
                return _not;
            }
        }

        public ReadOnlyCollection<LinkDescription> Links
        {
            get
            {
                if (_links == null)
                    return null;

                return new ReadOnlyCollection<LinkDescription>(_links);
            }
        }

        /// <summary>
        /// Gets the method to use for finding the appropriate instance within a document, given the fragment part.
        /// </summary>
        /// <remarks>
        /// When addressing a JSON document, the fragment part of the URI may be used to refer to a particular instance within the document.
        ///
        /// <para>This keyword indicates the method to use for finding the appropriate instance within a document, given the fragment part. The default fragment resolution protocol is "json-pointer", which is defined below. Other fragment resolution protocols MAY be used, but are not defined in this document.</para>
        ///
        /// <para>If the instance is described by a schema providing the a link with "root" relation, or such a link is provided in using the HTTP Link header [RFC5988], then the target of the "root" link should be considered the document root for the purposes of all fragment resolution methods that use the document structure (such as "json-pointer"). The only exception to this is the resolution of "root" links themselves.</para>
        /// </remarks>
        public string FragmentResolution
        {
            get
            {
                return _fragmentResolution;
            }
        }

        public MediaDescription Media
        {
            get
            {
                return _media;
            }
        }

        public Uri PathStart
        {
            get
            {
                if (_pathStart == null)
                    return null;

                return new Uri(_pathStart, UriKind.Absolute);
            }
        }
    }
}
