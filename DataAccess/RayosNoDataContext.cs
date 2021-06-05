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
    }
}
