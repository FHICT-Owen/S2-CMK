using System;
using eTransport_Persistence.DbContext;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using eTransport_Utility.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace eTransport_Persistence
{
    public class CargoDal : ICargoDal
    {
        
        public int AddCargo(CargoDto cargo)
        {
            try
            {
                var context = new DalDbContext();
                context.Cargos.Add(cargo);
                context.SaveChanges();
                var id = cargo.CargoId;
                return id;
            }
            catch (Exception)
            {
                throw new DalSQLException("Unable to Add cargo to database");
            }
        }

        public bool RemoveCargo(CargoDto cargo)
        {
            try
            {
                var context = new DalDbContext();
                var entry = context.Cargos.SingleOrDefault(result => result.CargoId == cargo.CargoId);
                if (entry == null) return false;
                context.Cargos.Remove(entry);
                return context.SaveChanges() >= 0;
            } catch (Exception)
            {
                throw new DalSQLException("Unable to Remove cargo from database");
            }

        }
        public List<CargoDto> GetCargos()
        {
            var context = new DalDbContext();
            var cargoList = context.Cargos.ToList();
            return cargoList;
        }

        public bool EditCargo(CargoDto cargo)
        {
            try {
                var context = new DalDbContext();
                var entry = context.Cargos.SingleOrDefault(result => result.CargoId == cargo.CargoId);
                if (entry == null) return false;
                context.Entry(entry).CurrentValues.SetValues(cargo);
                return context.SaveChanges() >= 0;
            } catch (Exception)
            {
                throw new DalSQLException("Unable to edit cargo on database");
            }
        }

        public bool SetRequest(int cargoId, int requestId)
        {
            try
            {
                var context = new DalDbContext();
                var entry = context.Cargos.SingleOrDefault(result => result.CargoId == cargoId);
                if (entry == null) return false;
                entry.RequestId = requestId;
                return context.SaveChanges() >= 0;
            } catch (Exception)
            {
                throw new DalSQLException("Unable to set the request on selected cargo in the database");
            }

        }

        public bool SetTruckLink(int cargoId, string selectedTrucks)
        {
            try
            {
                var context = new DalDbContext();
                var entry = context.Cargos.SingleOrDefault(result => result.CargoId == cargoId);
                if (entry == null) return false;
                entry.SelectedTrucks = selectedTrucks;
                return context.SaveChanges() >= 0;
            }catch (Exception)
            {
                throw new DalSQLException("Unable to Add cargo to database");
            }

        }
    }
}
