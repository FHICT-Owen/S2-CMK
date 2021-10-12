using EntityFramework.Exceptions.SqlServer;
using eTransport_Utility;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTransport_Persistence.DbContext
{
    public sealed class DalDbContext : IdentityDbContext<ETransportUser>
    {
        public DalDbContext()
        { 
            Database.SetConnectionString("Data Source=tcp:cgi-wradbserver.database.windows.net,1433;Initial Catalog=eTransport;User Id=grotebaas@cgi-wradbserver;Password=Grote7856");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
            optionsBuilder.UseExceptionProcessor();
        }

        public DbSet<TruckDto> Trucks { get; set; }
        public DbSet<CargoDto> Cargos { get; set; }
        public DbSet<RideShareDto> RideShares { get; set; }
    }
}
