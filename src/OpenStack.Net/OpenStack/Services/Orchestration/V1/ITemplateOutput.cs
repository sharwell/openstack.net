namespace OpenStack.Services.Orchestration.V1
{
    using Newtonsoft.Json.Linq;

    public interface ITemplateOutput
    {
        string Description
        {
            get;
        }

        JToken Value
        {
            get;
        }
    }
}
