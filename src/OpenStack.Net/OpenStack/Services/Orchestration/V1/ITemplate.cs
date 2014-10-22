namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;

    public interface ITemplate
    {
        TemplateVersion TemplateVersion
        {
            get;
        }

        ReadOnlyCollection<ITemplateParameterGroup> ParameterGroups
        {
            get;
        }

        ReadOnlyDictionary<TemplateParameterName, ITemplateParameter> Parameters
        {
            get;
        }

        ReadOnlyDictionary<string, ITemplateResource> Resources
        {
            get;
        }

        ReadOnlyDictionary<string, ITemplateOutput> Outputs
        {
            get;
        }
    }
}
