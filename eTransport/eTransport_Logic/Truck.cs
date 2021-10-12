using System.ComponentModel.DataAnnotations;
using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Linq;
using eTransport_Utility.Exceptions;

namespace eTransport_Logic
{
    public class Truck
    {
        #region properties
        private readonly ITruckDal _dal;
        [Required]
        public int TruckId { get; private set; }
        public string TruckRange { get; private set; }
        [Required]
        public string Weight { get; private set; }
        [Required]
        public string LicensePlate { get; private set; }
        [Required]
        public float FuelUsage { get; private set; }
        [Required]
        public float Height { get; private set; }
        [Required]
        public float Capacity { get; private set; }
        [Required]
        public TruckBrand Brand { get; private set; }
        [Required]
        public MaterialState CargoType { get; private set; }
        public bool InUse { get; private set; }
        #endregion

        public Truck(int truckId, string truckRange, string weight, string licensePlate, float fuelUsage, float height, float capacity, TruckBrand brand, MaterialState cargoType, bool inUse)
        {
            TruckId = truckId;
            TruckRange = truckRange;
            Weight = weight;
            LicensePlate = licensePlate;
            FuelUsage = fuelUsage;
            Height = height;
            Capacity = capacity;
            Brand = brand;
            CargoType = cargoType;
            InUse = inUse;
            _dal = TruckFactory.CreateTruckDal();
        }

        public Truck(TruckDto truckDto) 
            : this(truckDto.TruckId, truckDto.TruckRange, truckDto.Weight, truckDto.LicensePlate, truckDto.FuelUsage, truckDto.Height, truckDto.Capacity, truckDto.Brand, truckDto.CargoType, truckDto.InUse)
        {

        }

        public TruckDto ConvertToDto()
        {
            return new TruckDto(TruckId, TruckRange, Weight, LicensePlate, FuelUsage, Height, Capacity, Brand, CargoType, InUse);
        }

        public bool Link()
        {
            if (InUse) throw new LogicException("Unable to link truck! Already in use!");
            InUse = true;

            return _dal.EditTruck(ConvertToDto());
        }

        public bool Unlink()
        {
            if (InUse == false) throw new LogicException("Truck is already not linked!");
            InUse = false;

            return _dal.EditTruck(ConvertToDto());
        }

        public void EditId(int id)
        {
            TruckId = id;
        }

        public static Truck GetTruck(int truckId)
        {
            var entry = TruckCollection.Instance._trucksList.SingleOrDefault(result => result.TruckId == truckId);
            return entry;
        }

        public bool EditTruck(string userId, string truckRange, string weight, string licensePlate, float fuelUsage, float height, float capacity, TruckBrand brand, MaterialState cargoType)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            if (InUse) throw new LogicException("Cannot edit a truck that is already in use!");
            TruckRange = truckRange;
            Weight = weight;
            LicensePlate = licensePlate;
            FuelUsage = fuelUsage;
            Height = height;
            Capacity = capacity;
            Brand = brand;
            CargoType = cargoType;

            return _dal.EditTruck(ConvertToDto());
        }
    }
}
