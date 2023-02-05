using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class Node
    {
        public Node Parent { get; }
        public float CostSoFar { get; }
        public List<StateItem> State { get; }
        public Action Action { get; }

        public Node(Node parent, float costSoFar, List<StateItem> state, Action action)
        {
            Parent = parent;
            State = state;
            CostSoFar = costSoFar;
            Action = action;
        }
    }

    public class Planner
    {
        public Queue<Action> DeterminePlan(Goal goal, List<Action> availableActions, List<StateItem> initialState)
        {
            var leaves = new List<Node>();
            var startNode = new Node(null, 0, initialState, null);
            BuildGraph(goal, startNode, leaves, availableActions);

            
            if (leaves.Count == 0) return new Queue<Action>();
            
            var nextNode = leaves
                .OrderBy(l => l.CostSoFar)
                .First();

            var actionList = new List<Action>();

            while (nextNode != null)
            {
                if(nextNode.Action!=null) actionList.Add(nextNode.Action);
                nextNode = nextNode.Parent;
            }

            actionList.Reverse();
            return new Queue<Action>(actionList);
        }

        private void BuildGraph(Goal goal, Node parent, List<Node> leaves, List<Action> remainingActions)
        {
            var usableActions = remainingActions
                .Where(ra => ra.IsAchievableGiven(parent.State))
                .ToList();

            foreach (var action in usableActions)
            {
                var newEffects = action.Effects
                    .Where(effect => !parent.State.Contains(effect))
                    .ToList();

                var newState = new List<StateItem>(parent.State)
                    .Concat(newEffects).ToList();

                var nextNode = new Node(parent, parent.CostSoFar + action.Cost, newState, action);
                var reachedGoal = goal.RequiredStates.All(targetState => newState.Contains(targetState));

                if (reachedGoal)
                {
                    leaves.Add(nextNode);
                    return;
                }

                var reducedActions = remainingActions
                    .Where(a => !a.Equals(action))
                    .ToList();
                
                BuildGraph(goal, nextNode, leaves, reducedActions);
            }
        }
    }
}
