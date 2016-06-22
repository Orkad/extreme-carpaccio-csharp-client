using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
            
        }
    }
}
