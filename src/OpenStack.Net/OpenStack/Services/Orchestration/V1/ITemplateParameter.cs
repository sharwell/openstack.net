using Newtonsoft.Json.Linq;

namespace OpenStack.Services.Orchestration.V1
{
    public interface ITemplateParameter
    {
        ITemplateParameterType Type
        {
            get;
        }

        string Label
        {
            get;
        }

        string Description
        {
            get;
        }

        JToken DefaultValue
        {
            get;
        }

        bool? Hidden
        {
            get;
        }

        //JToken Constraints
        //{
        //    get;
        //}
    }
}
