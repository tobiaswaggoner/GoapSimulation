using System.Collections.Generic;
using System.Linq;
using GOAP;
using NUnit.Framework;
using UnitTests.GOAP.TestFixtures;

namespace UnitTests.GOAP
{
    [TestFixture]
    public class ActionTests
    {
        [TestCase(new []{StateItem.AtHome}, true, TestName = "Preconditions are met")]
        [TestCase(new StateItem[0], false, TestName = "Preconditions are not met")]
        public void PreConditions_are_validated_correctly(StateItem[] stateItems, bool expectedToBeAchievable)
        {
            var sut = TestActions.GoToHospitalAction;
            var achievable = sut.IsAchievableGiven(stateItems.ToList());
            Assert.AreEqual(expectedToBeAchievable, achievable);
        }
    }
}