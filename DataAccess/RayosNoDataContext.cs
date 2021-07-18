using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Entities;
using System.IO;

namespace DataAccess
{
    public class RayosNoDataContext : DbContext
    {
        #region Internal Attributes
        /// <summary>
        /// Temporal Connection String (for internal use of the .dll)
        /// </summary>
        internal string MyConnectionString { get; set; }
        internal IConfiguration Configuration { get; set; }
        #endregion

        #region Public Attributes
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MaintenanceEntity> Maintenances { get; set; } 
        public DbSet<ReplacementDeviceEntity> Replacements { get; set; }
        public DbSet<SaleManEntity> Salemans { get; set; }
        public DbSet<WarrantyEntity> Warranties { get; set; }
        public DbSet<IncidentEntity> Incidents { get; set; }
        #endregion

        #region Creating Model and seeding of data
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            GetConnectionString();
            if (!options.IsConfigured)
            {
                options.UseSqlServer(MyConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClientEntity oClient = new ClientEntity { Id = Guid.NewGuid().ToString(), Name = "Prueba" };
            SaleManEntity oSaleMan = new SaleManEntity { SaleManId = Guid.NewGuid().ToString(), Name = "Prueba" };
            DeviceEntity oDevice = new DeviceEntity { DeviceId = Guid.NewGuid().ToString(), Alias = "Prueba", ClientId = oClient.Id, SaleManId = oSaleMan.SaleManId };
            MaintenanceEntity oMaintenance = new MaintenanceEntity { MaintenanceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            ReplacementDeviceEntity oReplace = new ReplacementDeviceEntity { ReplacementDeviceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            WarrantyEntity oWarranty = new WarrantyEntity { Id = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            IncidentEntity oIncident = new IncidentEntity { IncidentId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientEntity>().HasData(oClient);
            modelBuilder.Entity<SaleManEntity>().HasData(oSaleMan);
            modelBuilder.Entity<DeviceEntity>().HasData(oDevice);
            modelBuilder.Entity<MaintenanceEntity>().HasData(oMaintenance);
            modelBuilder.Entity<ReplacementDeviceEntity>().HasData(oReplace);
            modelBuilder.Entity<WarrantyEntity>().HasData(oWarranty);
            modelBuilder.Entity<IncidentEntity>().HasData(oIncident); 
        }
        #endregion

        /// <summary>
        /// Create a new configuration builder and connect to appsettings.json and read the 
        /// connection string and set the value
        /// </summary>
        private void GetConnectionString (string connectionStringName = "RayosNoConnection")
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json") 
                              .AddEnvironmentVariables(); 
            Configuration = builder.Build();
            MyConnectionString = Configuration.GetConnectionString(connectionStringName);
        }

    }
}
