using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stateflow.Utils;

namespace Stateflow.Workflow
{
    public class WorkflowDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<State> States { get; set; }
        public IList<Trigger> Triggers { get; set; }
        public IList<Transition> Transitions { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }

        public static WorkflowDefinition Deserialize(string json)
        {
            Enforce.ArgumentNotNull(json, "json");

            return JsonConvert.DeserializeObject<WorkflowDefinition>(json);
        }


    }
}