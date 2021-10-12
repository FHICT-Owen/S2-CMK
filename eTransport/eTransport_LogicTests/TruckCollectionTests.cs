using eTransport_Logic;
using eTransport_Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace eTransport_LogicTests
{
    [TestClass()]
    public class TruckCollectionTests
    {
        [TestMethod()]
        public void AddTruckTest()
        {
            var truckCollection = TruckCollection.Instance;
            truckCollection.AddTruck("d4c7e4a0-fe48-412d-9996-2ad989e0495b", "100km", "20 tons", "test-00", 1, 1, 1, TruckBrand.Daimler, MaterialState.Gas);
            var truck = truckCollection._trucksList.Last();
        }
    }
}