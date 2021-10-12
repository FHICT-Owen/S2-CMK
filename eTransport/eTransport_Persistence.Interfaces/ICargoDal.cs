using System.Collections.Generic;
using eTransport_Utility;

namespace eTransport_Persistence.Interfaces
{
    public interface ICargoDal
    {
        int AddCargo(CargoDto cargo);
        bool RemoveCargo(CargoDto cargo);
        List<CargoDto> GetCargos();
        bool EditCargo(CargoDto cargo);
        bool SetRequest(int cargoId, int requestId);
        bool SetTruckLink(int cargoId, string selectedTrucks);
    }
}