using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BikeRental
{
    [TestFixture]
    class FirstTest
    {
        [TestCase]
        public void SimpleTest()
        {
            int n = 1;
            
            Assert.AreEqual(n, 1);
        }



    }
}
