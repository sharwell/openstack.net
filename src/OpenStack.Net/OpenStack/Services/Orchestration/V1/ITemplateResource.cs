namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;
    using Newtonsoft.Json.Linq;

    public interface ITemplateResource
    {
        ResourceTypeName Type
        {
            get;
        }

        ReadOnlyDictionary<string, JToken> Properties
        {
            get;
        }

        ReadOnlyCollection<ResourceName> Dependencies
        {
            get;
        }

        JToken UpdatePolicy
        {
            get;
        }

        DeletionPolicy DeletionPolicy
        {
            get;
        }
    }
}
