using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using eTransport_Utility.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eTransport_Logic
{
    public class RideShare
    {
        #region properties
        private readonly IRideShareDal _dal;
        [Required]
        public int RideShareId { get; private set; }
        public List<int> Requests { get; }
        [Required]
        public string Destination { get; private set; }
        [Required]
        public int SelectedTruck { get; private set; }
        [Required]
        public int MaxCapacity { get; private set; }
        #endregion

        public RideShare(int rideShareId, List<int> requests, string destination, int selectedTruck, int maxCapacity)
        {
            RideShareId = rideShareId;
            Requests = requests;
            Destination = destination;
            SelectedTruck = selectedTruck;
            MaxCapacity = maxCapacity;
            _dal = RideShareFactory.CreateRideShareDal();
        }

        private static string ConvertList(IEnumerable<int> requests)
        {
            var result = string.Join("|", requests);
            return result;
        }

        private static List<int> ConvertList(string requests)
        {
            var startingList = requests.Split("|").ToList();
            return startingList.Select(result =>
            {
                var converted = result == "" ? 0 : int.Parse(result);
                return converted;
            }).ToList();
        }

        public RideShare(RideShareDto rideShareDto)
            : this(rideShareDto.RideShareId, ConvertList(rideShareDto.Requests), rideShareDto.Destination, rideShareDto.SelectedTruck, rideShareDto.MaxCapacity)
        {
        }

        public RideShareDto ConvertToDto()
        {
            return new RideShareDto(RideShareId, ConvertList(Requests), Destination, SelectedTruck, MaxCapacity);
        }

        public void EditId(RideShare rideShare, int id)
        {
            rideShare.RideShareId = id;
        }

        public static RideShareDto GetRideShare(int rideShareId)
        {
            var entry = RideShareCollection.Instance._rideSharesList.SingleOrDefault(result => result.RideShareId == rideShareId);
            if (entry == null) throw new LogicException($"Could not convert Rideshare with id {rideShareId} to DTO!");
            return entry.ConvertToDto();
        }

        public bool EditRideShare(string userId, string destination, int maxCapacity)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            Destination = destination;
            MaxCapacity = maxCapacity;

            return _dal.EditRideShare(ConvertToDto());
        }

        public bool LinkRequest(string userId, int requestId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            if (user == null) throw new LogicException($"Could not find user with id: {userId}!");
            var request = user.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (request == null) throw new LogicException($"Could not find request with id: {requestId}!");
            if (request.IsLinked) throw new LogicException("This request is already linked!");
            request.Link(userId);
            Requests.Add(requestId);
            return _dal.SetRequestLink(RideShareId, ConvertList(Requests));
        }

        public bool UnlinkRequest(string userId,  int requestId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            if (user == null) throw new LogicException($"Could not find user with id: {userId}!");
            var request = user.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (request == null) throw new LogicException($"Could not find request with id: {requestId}!");
            request.Unlink(userId);
            Requests.Remove(requestId);
            return _dal.SetRequestLink(RideShareId, ConvertList(Requests));
        }

        public bool LinkTruck(string userId, int truckId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var truck = TruckCollection.Instance._trucksList.SingleOrDefault(result => result.TruckId == truckId);
            if (truck == null) throw new LogicException($"Could not find truck with id: {truckId}!");
            if (truck.InUse) throw new LogicException("This truck is already in use!");
            SelectedTruck = truck.TruckId;
            truck.Link();
            return _dal.SetTruck(RideShareId, SelectedTruck);
        }

        public bool UnlinkTruck(string userId, int truckId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var truck = TruckCollection.Instance._trucksList.SingleOrDefault(result => result.TruckId == truckId);
            if (truck == null) throw new LogicException($"Could not find truck with id: {truckId}!");
            if (truck.InUse == false) throw new LogicException("Truck is already not linked to any!");
            SelectedTruck = 0;
            truck.Unlink();
            return _dal.SetTruck(RideShareId, SelectedTruck);
        }
    }
}
