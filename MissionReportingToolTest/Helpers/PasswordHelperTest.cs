using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionReportingTool.Helpers;

namespace MissionReportingToolTest.Helpers
{
    [TestClass]
    public class PasswordHelperTest
    {
        [TestMethod]
        public void testHash()
        {
            var password = "test";
            var actual = PasswordHelper.Hash(password);
            Assert.AreNotEqual(password, actual);
        }

        [TestMethod]
        public void testCheck()
        {
            var password = "test";
            var hash = PasswordHelper.Hash(password);
            Assert.IsTrue(PasswordHelper.Check(hash, password));
        }

        [TestMethod]
        public void testCheckWithWrongHashFormatThrows()
        {
            var password = "test";
            Assert.ThrowsException<FormatException>(() => PasswordHelper.Check(password, password));
        }

        [TestMethod]
        public void testCheckWithWrongHash()
        {
            var password = "test";
            var hash = PasswordHelper.Hash(password + " new");
            Assert.IsFalse(PasswordHelper.Check(hash, password));
        }
    }
}
