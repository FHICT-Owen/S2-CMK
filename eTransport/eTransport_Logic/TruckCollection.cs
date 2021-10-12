using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Collections.Generic;
using System.Linq;
using eTransport_Utility.Exceptions;

namespace eTransport_Logic
{
    public class TruckCollection
    {
        private static TruckCollection _instance;
        private static readonly object Padlock = new object();
        public readonly List<Truck> _trucksList;
        private readonly ITruckDal _dal;

        public TruckCollection()
        {
            _dal = TruckFactory.CreateTruckDal();
            _trucksList = new List<Truck>();
        }

        public static TruckCollection Instance
        {
            get
            {
                lock (Padlock)
                {
                    // ReSharper disable once InvertIf
                    if (_instance == null)
                    {
                        _instance = new TruckCollection();
                        _instance.ReloadTrucks();
                    }
                    return _instance;
                }
            }
        }

        public bool AddTruck(string userId, string truckRange, string weight, string licensePlate, float fuelUsage, float height, float capacity, TruckBrand brand, MaterialState cargoType)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var truck = new Truck(0, truckRange, weight, licensePlate, fuelUsage, height, capacity, brand, cargoType, false);
            var truckId = _dal.AddTruck(truck.ConvertToDto());
            truck.EditId(truckId);
            _trucksList.Add(truck);
            return true;
        }

        public bool RemoveTruck(string userId, int truckId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var truck = _trucksList.FirstOrDefault(truckEntry => truckEntry.TruckId == truckId);
            if (truck == null) throw new LogicException($"Could not find any truck that matches id: {truckId}!");
            if (truck.InUse) throw new LogicException("Cannot remove a truck that is currently in use!");
            _trucksList.Remove(truck);

            return _dal.RemoveTruck(truck.ConvertToDto());
        }

        private void ReloadTrucks()
        {
            var trucks = _dal.GetTrucks();
            _trucksList.Clear();
            foreach (var dto in trucks)
            {
                _trucksList.Add(new Truck(dto));
            }
        }

        public static IReadOnlyCollection<Truck> GetTrucks()
        {
            return Instance._trucksList;
        }

        public static IReadOnlyCollection<TruckDto> GetDtoTrucks()
        {
            var convertedList = Instance._trucksList.Select(truck => new TruckDto(truck.TruckId, truck.TruckRange, truck.Weight, truck.LicensePlate, truck.FuelUsage, truck.Height, truck.Capacity, truck.Brand, truck.CargoType, truck.InUse)).ToList();
            return convertedList.AsReadOnly();
        }
    }
}
