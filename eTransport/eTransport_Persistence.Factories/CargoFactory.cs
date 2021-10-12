using eTransport_Persistence.Interfaces;

namespace eTransport_Persistence.Factories
{
    public class CargoFactory
    {
        public static ICargoDal CreateCargoDal()
        {
            return new CargoDal();
        }
    }
}