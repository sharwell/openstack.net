namespace net.openstack.Core.Domain
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceHints
    {
#pragma warning disable 649 // Field 'fieldName' is never assigned to, and will always have its default value {value}
        [JsonProperty("allow", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _allow;

        [JsonProperty("representations", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _representations;

        [JsonProperty("accept-patch", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _acceptPatch;

        [JsonProperty("accept-post", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _acceptPost;

        [JsonProperty("accept-put", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _acceptPut;

        [JsonProperty("accept-ranges", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _acceptRanges;

        [JsonProperty("prefer", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _prefer;

        [JsonProperty("docs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _docs;

        [JsonProperty("precondition-req", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string[] _preconditionReq;

        [JsonProperty("auth-req", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private AuthenticationRequirement[] _authReq;

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private string _status;
#pragma warning restore 649

        public ReadOnlyCollection<string> Allow
        {
            get
            {
                if (_allow == null)
                    return null;

                return new ReadOnlyCollection<string>(_allow);
            }
        }

        public ReadOnlyCollection<string> Representations
        {
            get
            {
                if (_representations == null)
                    return null;

                return new ReadOnlyCollection<string>(_representations);
            }
        }

        public ReadOnlyCollection<string> AcceptPatch
        {
            get
            {
                if (_acceptPatch == null)
                    return null;

                return new ReadOnlyCollection<string>(_acceptPatch);
            }
        }

        public ReadOnlyCollection<string> AcceptPost
        {
            get
            {
                if (_acceptPost == null)
                    return null;

                return new ReadOnlyCollection<string>(_acceptPost);
            }
        }

        public ReadOnlyCollection<string> AcceptPut
        {
            get
            {
                if (_acceptPut == null)
                    return null;

                return new ReadOnlyCollection<string>(_acceptPut);
            }
        }

        public ReadOnlyCollection<string> AcceptRanges
        {
            get
            {
                if (_acceptRanges == null)
                    return null;

                return new ReadOnlyCollection<string>(_acceptRanges);
            }
        }

        public ReadOnlyCollection<string> Prefer
        {
            get
            {
                if (_prefer == null)
                    return null;

                return new ReadOnlyCollection<string>(_prefer);
            }
        }

        public string Docs
        {
            get
            {
                return _docs;
            }
        }

        public ReadOnlyCollection<string> Preconditions
        {
            get
            {
                if (_preconditionReq == null)
                    return null;

                return new ReadOnlyCollection<string>(_preconditionReq);
            }
        }

        public ReadOnlyCollection<AuthenticationRequirement> AuthenticationRequirements
        {
            get
            {
                if (_authReq == null)
                    return null;

                return new ReadOnlyCollection<AuthenticationRequirement>(_authReq);
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }
    }
}
