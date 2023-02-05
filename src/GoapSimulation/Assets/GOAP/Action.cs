using System.Collections.Generic;

namespace GOAP
{
    public class Action
    {
        public string Name { get; set; }
        public List<StateItem> PreConditions { get; set; }
        public List<StateItem> Effects { get; set; }
        public float Cost { get; set; } = 1.0f;

        public bool IsAchievableGiven(List<StateItem> conditions)
        {
            return PreConditions.TrueForAll(conditions.Contains);
        }
    }
}