using GOAP;
using NUnit.Framework;

namespace UnitTests.GOAP
{
    public class PlannerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Determin_Plan_Gets_Executed()
        {
            var sut = new Planner();
            sut.DeterminePlan();
        }

    }
}
