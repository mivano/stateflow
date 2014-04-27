using System.Collections.Generic;

namespace Stateflow.Workflow
{
    public class State
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<IAction> EntryActions { get; set; } 
        public IList<IAction> ExitActions { get; set; }
    }
}