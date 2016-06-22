using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using xCarpaccio.client;

namespace extreme_carpatio.test
{
    public class Tests
    {
        [Test]
        public void EmptyTest()
        {
            Assert.IsFalse(false);
            Assert.That(false,Is.False);
        }

        [Test]
        public void TestReduction()
        {
            Assert.AreEqual(BillCalculator.ApplyReduction(60000),60000/0.15);
            Assert.AreEqual(BillCalculator.ApplyReduction(20000), 20000 /0.10);
        }
    }
}
