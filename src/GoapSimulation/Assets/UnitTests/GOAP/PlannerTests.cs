using System.Collections.Generic;
using System.Linq;
using GOAP;
using NUnit.Framework;
using UnitTests.GOAP.TestFixtures;

namespace UnitTests.GOAP
{
    public class GoapTestCase
    {
        public Goal Goal;
        public string TestCaseName;
        public List<Action> AvailableActions;
        public List<StateItem> InitialState;
        public List<Action> ExpectedActionQueue;
    }
    
    [TestFixture]
    public class PlannerTests
    {
        [Test]
        public void Determine_Plan_Gets_Executed_Correctly([ValueSource(nameof(GoapTestCases))] GoapTestCase testCase)
        {
            var sut = new Planner();

            var calculatedPlan = sut.DeterminePlan(testCase.Goal, testCase.AvailableActions, testCase.InitialState);
            Assert.IsNotNull(calculatedPlan, $"Calculated plan was null in TestCase {testCase.TestCaseName}");
            var calculatedList = calculatedPlan.ToList();
            Assert.AreEqual(testCase.ExpectedActionQueue.Count, calculatedList.Count, DumpListComparison(testCase.TestCaseName, calculatedList, testCase.ExpectedActionQueue));
            for (var i = 0; i < calculatedPlan.Count; i++)
            {
                Assert.AreEqual(calculatedList[i].Name, testCase.ExpectedActionQueue[i].Name);
            }
        }

        private static string DumpListComparison(string testCaseName, IEnumerable<Action> calculatedList, IEnumerable<Action> expectedList)
        {
            return $"Calculated plan differed from expected plan in TestCase {testCaseName}.\r\nCalculated:\r\n{calculatedList.Aggregate( "", (total, action )=>$"{total}{action.Name}\r\n")}\r\nExpected:\r\n{expectedList.Aggregate( "", (total, action )=>$"{total}{action.Name}\r\n")}";
        }

        private static IEnumerable<GoapTestCase> GoapTestCases()
        {
            yield return new GoapTestCase
            {
                TestCaseName = "No Action available - no plan",
                Goal = new Goal { Name = "Get to hospital", RequiredStates = new () {StateItem.AtHospital} },
                InitialState = new () {StateItem.AtHome },
                AvailableActions = new List<Action> { },
                ExpectedActionQueue = new List<Action> { },
            };
            
            yield return new GoapTestCase
            {
                TestCaseName = "Simple: Get from home to hospital",
                Goal = new Goal { Name = "Get to hospital", RequiredStates = new () {StateItem.AtHospital} },
                InitialState = new () {StateItem.AtHome },
                AvailableActions = TestActions.AllActions,
                ExpectedActionQueue = new List<Action> { TestActions.GoToHospitalAction },
            };

            yield return new GoapTestCase
            {
                TestCaseName = "Complex: Get from home to cubicle",
                Goal = new Goal { Name = "Get treated", RequiredStates = new () { StateItem.InCubicle} },
                InitialState = new () {StateItem.AtHome },
                AvailableActions = TestActions.AllActions,
                ExpectedActionQueue = new List<Action> { TestActions.GoToHospitalAction, TestActions.GoToReceptionAction,TestActions.GoToWaitingRoomAction, TestActions.GoToCubicle },
            };

        }
    }
}
