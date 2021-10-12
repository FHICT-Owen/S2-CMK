using eTransport_Persistence.DbContext;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Collections.Generic;
using System.Linq;

namespace eTransport_Persistence
{
    public class RideShareDal : IRideShareDal
    {
        public int AddRideShare(RideShareDto rideShare)
        {
            var context = new DalDbContext();
            context.RideShares.Add(rideShare);
            context.SaveChanges();
            var id = rideShare.RideShareId;
            return id;
        }

        public bool RemoveRideShare(RideShareDto rideShare)
        {
            var context = new DalDbContext();
            var entry = context.RideShares.SingleOrDefault(result => result.RideShareId == rideShare.RideShareId);
            if (entry == null) return false;
            context.RideShares.Remove(entry);
            return context.SaveChanges() >= 0;
        }
        public List<RideShareDto> GetRideShares()
        {
            var context = new DalDbContext();
            var rideShareList = context.RideShares.ToList();
            return rideShareList;
        }

        public bool EditRideShare(RideShareDto rideShare)
        {
            var context = new DalDbContext();
            var entry = context.RideShares.SingleOrDefault(result => result.RideShareId == rideShare.RideShareId);
            if (entry == null) return false;
            context.Entry(entry).CurrentValues.SetValues(rideShare);
            return context.SaveChanges() >= 0;
        }

        public bool SetRequestLink(int rideShareId, string requests)
        {
            var context = new DalDbContext();
            var entry = context.RideShares.SingleOrDefault(result => result.RideShareId == rideShareId);
            if (entry == null) return false;
            entry.Requests = requests;
            return context.SaveChanges() >= 0;
        }

        public bool SetTruck(int rideShareId, int selectedTruck)
        {
            var context = new DalDbContext();
            var entry = context.RideShares.SingleOrDefault(result => result.RideShareId == rideShareId);
            if (entry == null) return false;
            entry.SelectedTruck = selectedTruck;
            return context.SaveChanges() >= 0;
        }
    }
}
