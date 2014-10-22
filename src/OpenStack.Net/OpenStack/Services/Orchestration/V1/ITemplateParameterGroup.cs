namespace OpenStack.Services.Orchestration.V1
{
    using System.Collections.ObjectModel;

    public interface ITemplateParameterGroup
    {
        string Label
        {
            get;
        }

        string Description
        {
            get;
        }

        ReadOnlyCollection<TemplateParameterName> Parameters
        {
            get;
        }
    }
}
