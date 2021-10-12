using System.Collections.Generic;
using eTransport_Utility;

namespace eTransport_Persistence.Interfaces
{
    public interface ITruckDal
    {
        int AddTruck(TruckDto truck);
        bool RemoveTruck(TruckDto truck);
        List<TruckDto> GetTrucks();
        bool EditTruck(TruckDto truck);
    }
}