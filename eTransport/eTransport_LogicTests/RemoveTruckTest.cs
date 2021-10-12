using eTransport_Logic;
using eTransport_Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace eTransport_LogicTests
{
    [TestClass()]
    public class RemoveTruckTests
    {
        [TestMethod()]
        public void RemoveTruckTest()
        {
            var truckCollection = TruckCollection.Instance;
            var truck = truckCollection._trucksList.Last();
            Assert.IsFalse(truckCollection.RemoveTruck("", truck.TruckId));
            Assert.IsTrue(truckCollection.RemoveTruck("d4c7e4a0-fe48-412d-9996-2ad989e0495b", truck.TruckId));
        }
    }
}