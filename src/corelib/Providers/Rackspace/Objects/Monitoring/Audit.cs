namespace net.openstack.Providers.Rackspace.Objects.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Audit
    {
        [JsonProperty("id")]
        private AuditId _id;

        [JsonProperty("headers")]
        private Dictionary<string, string> _headers;

        [JsonProperty("url")]
        private string _url;

        [JsonProperty("app")]
        private string _app;

        [JsonProperty("query")]
        private object _query;

        [JsonProperty("txnId")]
        private string _transactionId;

        [JsonProperty("payload")]
        private string _payload;

        [JsonProperty("method")]
        private string _method;

        [JsonProperty("account_id")]
        private string _accountId;

        [JsonProperty("who")]
        private object _who;

        [JsonProperty("why")]
        private object _why;

        [JsonProperty("statusCode")]
        private int _statusCode;
    }
}
