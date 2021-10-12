using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace eTransport_Logic
{
    public class CargoCollection
    {
        private static CargoCollection _instance;
        private static readonly object Padlock = new object();
        public readonly List<Cargo> _cargoList;
        private readonly ICargoDal _dal;

        public CargoCollection()
        {
            _dal = CargoFactory.CreateCargoDal();
            _cargoList = new List<Cargo>();
        }

        public static CargoCollection Instance
        {
            get
            {
                lock (Padlock)
                {
                    // ReSharper disable once InvertIf
                    if (_instance == null)
                    {
                        _instance = new CargoCollection();
                        _instance.ReloadCargos();
                    }
                    return _instance;
                }
            }
        }

        public bool AddCargo(string userId, string destination, int requestId, int maxCapacity)
        {
            var selectedTrucks = new List<int>();
            var cargo = new Cargo(userId, 0, requestId, destination, selectedTrucks, maxCapacity);
            var cargoId = _dal.AddCargo(cargo.ConvertToDto());
            cargo.EditId(cargoId);
            _cargoList.Add(cargo);
            return true;
        }

        public bool RemoveCargo(string userId, int cargoId)
        {
            var cargo = _cargoList.FirstOrDefault(cargoEntry => cargoEntry.CargoId == cargoId);
            if (cargo == null) throw new LogicException($"Unable to find cargo with id {cargoId}!");
            var user = UserCollection.Instance._usersList.SingleOrDefault(res => res.UserId == userId);
            if (user == null) throw new LogicException($"Unable to find user with id {userId}!");
            user.RemoveRequest(cargo.RequestId);
            _cargoList.Remove(cargo);

            return _dal.RemoveCargo(cargo.ConvertToDto());
        }

        private void ReloadCargos()
        {
            var cargos = _dal.GetCargos();
            _cargoList.Clear();
            foreach (var dto in cargos)
            {
                _cargoList.Add(new Cargo(dto));
            }
        }

        public IReadOnlyCollection<Cargo> GetUserCargos(string userId)
        {
            return Instance._cargoList.Where(cargo => cargo.UserId == userId).ToList();
        }

        public IReadOnlyCollection<Cargo> GetCargos()
        {
            return Instance._cargoList.AsReadOnly();
        }
    }
}
