using eTransport_Persistence.Interfaces;

namespace eTransport_Persistence.Factories
{
    public class RideShareFactory
    {
        public static IRideShareDal CreateRideShareDal()
        {
            return new RideShareDal();
        }
    }
}