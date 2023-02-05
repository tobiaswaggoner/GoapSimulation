using System.Collections.Generic;

namespace GOAP
{
    public class Goal
    {
        public string Name { get; set; }
        public List<StateItem> RequiredStates { get; set; }
    }
}