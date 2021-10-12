using System;
using eTransport_Persistence.DbContext;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Collections.Generic;
using System.Linq;
using eTransport_Utility.Exceptions;


namespace eTransport_Persistence
{
    public class TruckDal : ITruckDal
    {
        public int AddTruck(TruckDto truck)
        {
            try
            {
                var context = new DalDbContext();
                context.Trucks.Add(truck);
                context.SaveChanges();
                var id = truck.TruckId;
                return id;
            }
            catch (Exception)
            {
                throw new DalSQLException("Unable to Add truck to database");
            }
        }

        public bool RemoveTruck(TruckDto truck)
        {
            try
            {
                var context = new DalDbContext();
                var entry = context.Trucks.SingleOrDefault(result => result.TruckId == truck.TruckId);
                if (entry == null) throw new DalSQLException($"Could not find any truck with the Id: {truck.TruckId}");
                context.Trucks.Remove(entry);
                return context.SaveChanges() >= 0;
            }
            catch (Exception)
            {
                throw new DalSQLException("Unable to remove truck from database");
            }
        }

        public List<TruckDto> GetTrucks()
        {
            try
            {
                var context = new DalDbContext();
                var truckList = context.Trucks.ToList();
                return truckList;
            }
            catch (Exception)
            {
                throw new DalSQLException("Could not get any trucks from database!");
            }
        }

        public bool EditTruck(TruckDto truck)
        {
            try
            {
                var context = new DalDbContext();
                var entry = context.Trucks.SingleOrDefault(result => result.TruckId == truck.TruckId);
                if (entry == null) throw new DalSQLException($"Could not find any truck with the Id: {truck.TruckId}");
                context.Entry(entry).CurrentValues.SetValues(truck);
                return context.SaveChanges() >= 0;
            }
            catch (Exception)
            {
                throw new DalSQLException($"Could not edit truck with Id {truck.TruckId} in the database!");
            }
        }
    }
}