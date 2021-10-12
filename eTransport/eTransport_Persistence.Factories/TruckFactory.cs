using eTransport_Persistence.Interfaces;

namespace eTransport_Persistence.Factories
{
    public class TruckFactory
    {
        public static ITruckDal CreateTruckDal()
        {
            return new TruckDal();
        }
    }
}