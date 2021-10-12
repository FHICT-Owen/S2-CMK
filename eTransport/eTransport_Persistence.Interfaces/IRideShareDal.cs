using System.Collections.Generic;
using eTransport_Utility;

namespace eTransport_Persistence.Interfaces
{
    public interface IRideShareDal
    {
        int AddRideShare(RideShareDto rideShare);
        bool RemoveRideShare(RideShareDto rideShare);
        List<RideShareDto> GetRideShares();
        bool EditRideShare(RideShareDto rideShare);
        bool SetRequestLink(int rideShareId, string requests);
        bool SetTruck(int rideShareId, int selectedTruck);
    }
}