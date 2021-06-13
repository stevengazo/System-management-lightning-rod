using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Entities;


namespace DataAccess
{
    public class RayosNoDataContext : DbContext
    {
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<MaintenanceEntity> Maintenances { get; set; } 
        public DbSet<ReplacementDeviceEntity> Replacements { get; set; }
        public DbSet<SaleManEntity> Salemans { get; set; }
        public DbSet<WarrantyEntity> Warranties { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=(Local);Database=RNTesting;User Id=TestingUserDb; Password=TestingUserDb1");                
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClientEntity oClient = new ClientEntity { Id = Guid.NewGuid().ToString(), Name = "Prueba" };
            SaleManEntity oSaleMan = new SaleManEntity { SaleManId = Guid.NewGuid().ToString(), Name = "Prueba" };
            DeviceEntity oDevice = new DeviceEntity { DeviceId = Guid.NewGuid().ToString(), Alias = "Prueba",ClientId= oClient.Id, SaleManId = oSaleMan.SaleManId };
            MaintenanceEntity oMaintenance = new MaintenanceEntity { MaintenanceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            ReplacementDeviceEntity oReplace = new ReplacementDeviceEntity { ReplacementDeviceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            WarrantyEntity oWarranty = new WarrantyEntity { Id = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientEntity>().HasData(oClient);            
            modelBuilder.Entity<SaleManEntity>().HasData(oSaleMan);
            modelBuilder.Entity<DeviceEntity>().HasData(oDevice);
            modelBuilder.Entity<MaintenanceEntity>().HasData(oMaintenance);
            modelBuilder.Entity<ReplacementDeviceEntity>().HasData(oReplace);
            modelBuilder.Entity<WarrantyEntity>().HasData(oWarranty);
        }
    }
}
