using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using eTransport_Utility.Exceptions;

namespace eTransport_Logic
{
    public class RideShareCollection
    {
        private static RideShareCollection _instance;
        private static readonly object Padlock = new object();
        public readonly List<RideShare> _rideSharesList;
        private readonly IRideShareDal _dal;

        public RideShareCollection()
        {
            _dal = RideShareFactory.CreateRideShareDal();
            _rideSharesList = new List<RideShare>();
        }

        public static RideShareCollection Instance
        {
            get
            {
                lock (Padlock)
                {
                    // ReSharper disable once InvertIf
                    if (_instance == null)
                    {
                        _instance = new RideShareCollection();
                        _instance.ReloadRideShares();
                    }
                    return _instance;
                }
            }
        }

        public bool AddRideShare(string userId, string destination, int selectedTruck, int maxCapacity)
        {
            if(UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var requests = new List<int>();
            var rideShare = new RideShare(0, requests, destination, 0, maxCapacity);
            rideShare.LinkTruck(userId, selectedTruck);
            var rideShareId = _dal.AddRideShare(rideShare.ConvertToDto());
            rideShare.EditId(rideShare, rideShareId);
            _rideSharesList.Add(rideShare);
            return true;
        }

        public bool RemoveRideShare(string userId, int rideShareId)
        {
            if (UserCollection.CheckAdmin(userId) == false) throw new PermissionException("You do not have enough privileges!");
            var rideShare = _rideSharesList.FirstOrDefault(rideShareEntry => rideShareEntry.RideShareId == rideShareId);
            if (rideShare == null) throw new LogicException($"Unable to remove rideshare with id: {rideShareId}!");
            if (rideShare.SelectedTruck != 0) TruckCollection.Instance._trucksList.
                SingleOrDefault(res => res.TruckId == rideShare.SelectedTruck)?.Unlink();
            _rideSharesList.Remove(rideShare);

            return _dal.RemoveRideShare(rideShare.ConvertToDto());
        }

        private void ReloadRideShares()
        {
            var rideShares = _dal.GetRideShares();
            _rideSharesList.Clear();
            foreach (var dto in rideShares)
            {
                _rideSharesList.Add(new RideShare(dto));
            }
        }

        public static IReadOnlyCollection<RideShare> GetRideShares()
        {
            return Instance._rideSharesList.AsReadOnly();
        }
    }
}
