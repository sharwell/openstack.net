namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class Deployment : DeploymentData
    {
        private DeploymentId _id;

        private JToken _outputValues;

        private JToken _inputValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deployment"/> class
        /// during JSON deserialization.
        /// </summary>
        [JsonConstructor]
        protected Deployment()
        {
        }

        public DeploymentId Id
        {
            get
            {
                return _id;
            }
        }
    }
}
