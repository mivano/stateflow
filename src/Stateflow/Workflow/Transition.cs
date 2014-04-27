using System.Collections.Generic;

namespace Stateflow.Workflow
{
    public class Transition
    {
        public string FromState { get; set; }
        public string ToState { get; set; }
        public string TriggerBy { get; set; }
        public IList<ICondition> Conditions { get; set; }
    }
}