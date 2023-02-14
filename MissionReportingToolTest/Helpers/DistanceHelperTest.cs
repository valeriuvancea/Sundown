using MissionReportingTool.Entitites;
using MissionReportingTool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Helpers
{

    [TestClass]
    public class DistanceHelperTest
    {
        [TestMethod]
        public void testGetDistance()
        {
            var actual = (int) DistanceHelper.GetDistance(0, 0, 45, 45);
            var expected = 6671;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void testCalculateTheClosestFacility()
        {
            var actual = DistanceHelper.CalculateTheClosestFacility(0, 0);
            var expected = Facility.AFRICA;
            Assert.AreEqual(expected, actual);
        }
    }
}
