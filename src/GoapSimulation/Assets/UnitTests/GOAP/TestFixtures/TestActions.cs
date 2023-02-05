using System.Collections.Generic;
using GOAP;

namespace UnitTests.GOAP.TestFixtures
{
    public static class TestActions
    {
        public static readonly Action GoToHospitalAction = new()
        {
            Name = "Go To Hospital",
            PreConditions = new List<StateItem>
            {
                StateItem.AtHome
            },
            Effects = new List<StateItem>
            {
                StateItem.AtHospital
            },
        };
        public static readonly Action GoToReceptionAction = new()
        {
            Name = "Go To Reception",
            PreConditions = new List<StateItem>
            {
                StateItem.AtHospital
            },
            Effects = new List<StateItem>
            {
                StateItem.AtReception
            },
        };
        public static readonly Action GoToWaitingRoomAction = new()
        {
            Name = "Go To Waiting Room",
            PreConditions = new List<StateItem>
            {
                StateItem.AtReception
            },
            Effects = new List<StateItem>
            {
                StateItem.InWaitingRoom
            },
        };
        public static readonly Action GoToCubicle = new()
        {
            Name = "Go To Cubicle",
            PreConditions = new List<StateItem>
            {
                StateItem.InWaitingRoom
            },
            Effects = new List<StateItem>
            {
                StateItem.InCubicle
            },
        };

        public static readonly List<Action> AllActions = new()
        {
            GoToHospitalAction, 
            GoToReceptionAction, 
            GoToWaitingRoomAction,
            GoToCubicle
        };
    }
}