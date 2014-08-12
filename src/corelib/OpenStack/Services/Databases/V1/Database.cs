namespace OpenStack.Services.Databases.V1
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the JSON representation of a database resource in the <see cref="IDatabaseService"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Database : DatabaseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Database()
        {
        }
    }
}
