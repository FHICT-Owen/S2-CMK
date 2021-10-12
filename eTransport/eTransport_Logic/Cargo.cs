using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using eTransport_Utility.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eTransport_Logic
{
    public class Cargo
    {
        #region properties

        private readonly ICargoDal _dal;
        [Required] 
        public string UserId { get; private set; }
        [Required]
        public int CargoId { get; private set; }
        [Required]
        public int RequestId { get; private set; }
        [Required]
        public string Destination { get; private set; }
        public List<int> SelectedTrucks { get; }
        [Required]
        public int MaxCapacity { get; private set; }

        #endregion

        public Cargo(string userId, int cargoId, int requestId, string destination, List<int> selectedTrucks, int maxCapacity)
        {
            UserId = userId;
            CargoId = cargoId;
            RequestId = requestId;
            Destination = destination;
            SelectedTrucks = selectedTrucks;
            MaxCapacity = maxCapacity;
            _dal = CargoFactory.CreateCargoDal();
        }

        private static string ConvertList(IEnumerable<int> requests)
        {
            var result = string.Join(",", requests);
            return result;
        }

        private static List<int> ConvertList(string requests)
        {
            if (string.IsNullOrEmpty(requests)) return new List<int>();
            var startingList = requests.Split(",").ToList();
            return startingList.Select(int.Parse).ToList();
        }

        public Cargo(CargoDto cargoDto)
            : this(cargoDto.UserId, cargoDto.CargoId, cargoDto.RequestId, cargoDto.Destination, ConvertList(cargoDto.SelectedTrucks),
                cargoDto.MaxCapacity)
        {
        }

        public CargoDto ConvertToDto()
        {
            return new CargoDto(UserId, CargoId, RequestId, Destination, ConvertList(SelectedTrucks), MaxCapacity);
        }

        public void EditId(int id)
        {
            CargoId = id;
        }

        public static CargoDto GetCargo(int cargoId)
        {
            var entry = CargoCollection.Instance._cargoList.SingleOrDefault(result => result.CargoId == cargoId);
            if (entry == null) throw new LogicException($"Could not convert cargo with id: {cargoId} to DTO!");
            return entry.ConvertToDto();
        }

        public bool EditCargo(string destination, int maxCapacity)
        {
            Destination = destination;
            MaxCapacity = maxCapacity;

            return _dal.EditCargo(ConvertToDto());
        }

        public bool LinkRequest(string userId, int requestId)
        {
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            if (user == null) throw new LogicException($"Could not find user with id: {userId}!");
            var request = user.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (request == null) throw new LogicException($"Could not find request with id: {requestId}!");
            if (request.IsLinked) throw new LogicException($"Request with id {requestId} is already linked!");
            RequestId = request.RequestId;
            request.Link(userId);

            return _dal.SetRequest(CargoId, RequestId);
        }

        public bool UnlinkRequest(string userId, int requestId)
        {
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            if (user == null) throw new LogicException($"Could not find user with id: {userId}!");
            var request = user.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (request == null) throw new LogicException($"Could not find request with id: {requestId}!");
            if (request.IsLinked == false) throw new LogicException($"Request with id {requestId} is not linked to any!");
            RequestId = 0;
            request.Unlink(userId);

            return _dal.SetRequest(CargoId, RequestId);
        }

        public bool LinkTruck(int truckId)
        {
            var truck = TruckCollection.Instance._trucksList.SingleOrDefault(result => result.TruckId == truckId);
            if (truck == null) throw new LogicException($"Could not find truck with id: {truckId}!");
            if (truck.InUse) throw new LogicException($"Truck with id {truckId} is already in use!");
            SelectedTrucks.Add(truckId);
            truck.Link();

            return _dal.SetTruckLink(truckId, ConvertList(SelectedTrucks));
        }

        public bool UnlinkTruck(int truckId)
        {
            var truck = TruckCollection.Instance._trucksList.SingleOrDefault(result => result.TruckId == truckId);
            if (truck == null) throw new LogicException($"Could not find truck with id: {truckId}!");
            if (truck.InUse) throw new LogicException($"Truck with id {truckId} is not linked to any!");
            SelectedTrucks.Remove(truckId);
            truck.Unlink();

            return _dal.SetTruckLink(truckId, ConvertList(SelectedTrucks));
        }
    }
}
