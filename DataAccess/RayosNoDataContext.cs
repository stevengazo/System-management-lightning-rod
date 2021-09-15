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
using Microsoft.Spatial;
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

        public DbSet<TypeDeviceEntity> TypesDevices { get; set; }
        public DbSet<ModelDeviceEntity> ModelDevices { get; set; }
        public DbSet<StatusEntity> Status {get;set;}
        public DbSet<CountryEntity>  Countries{get;set;}
        public DbSet<SectorEntity>  Sectors {get;set;}
        public DbSet<TechnicianEntity> Technicians {get;set;}
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


        protected void GenerateSeedOfData(ModelBuilder model)
        {
            #region COUNTRIES
            CountryEntity oCountry = new CountryEntity() { CountryId = 506, CountryName = "Costa Rica" };
            model.Entity<CountryEntity>().HasData(oCountry);
            #endregion


            #region STATUS
            StatusEntity oStatus = new StatusEntity() { StatusId = 1, StatusName = "Garantia Emitida" };
            model.Entity<StatusEntity>().HasData(oStatus);
            #endregion


            #region SALEMAN
            SaleManEntity oSaleMan = new SaleManEntity();
            oSaleMan.SaleManId = Guid.NewGuid().ToString();
            oSaleMan.Name = "sample";
            model.Entity<SaleManEntity>().HasData(oSaleMan);
            #endregion

            #region SECTORS
            SectorEntity oPSector = new SectorEntity() { SectorId = 1, SectorName = "Privado" };
            SectorEntity oPuSector= new SectorEntity() { SectorId = 2, SectorName = "Publico" };
            model.Entity<SectorEntity>().HasData(oPSector, oPuSector);
            #endregion

            #region CLIENT
            ClientEntity oClient = new ClientEntity { Id = Guid.NewGuid().ToString(), Name = "Prueba", SectorId = oPSector.SectorId };
            model.Entity<ClientEntity>().HasData(oClient);
            #endregion

            #region TYPES
            TypeDeviceEntity oVtype = new TypeDeviceEntity() { TypeDeviceId = 01, TypeDeviceName = "Venta" };
            TypeDeviceEntity oAtype = new TypeDeviceEntity() { TypeDeviceId = 04, TypeDeviceName = "Alquiler" };
            TypeDeviceEntity oLtype = new TypeDeviceEntity() { TypeDeviceId = 02, TypeDeviceName = "Leasing" };
            TypeDeviceEntity oPtype = new TypeDeviceEntity() { TypeDeviceId = 03, TypeDeviceName = "Prueba" };
            model.Entity<TypeDeviceEntity>().HasData(oVtype,oAtype,oLtype,oPtype);
            #endregion

            #region MODELS
            ModelDeviceEntity oModel = new ModelDeviceEntity() { ModelDeviceId = 1, ModelDeviceName = "DDCE-100" };
            model.Entity<ModelDeviceEntity>().HasData(oModel);
            #endregion

            #region Technician
            TechnicianEntity otech = new TechnicianEntity() { TechnicianId = 1, TechnicianName = "Sample" };
            model.Entity<TechnicianEntity>().HasData(otech);
            #endregion

            #region PROJECT
            DeviceEntity oDevice = new DeviceEntity
            {
                DeviceId = Guid.NewGuid().ToString(),
                Alias = "Prueba",
                ClientId = oClient.Id,
                SaleManId = oSaleMan.SaleManId,
                CountryId = oCountry.CountryId,
                ModelDeviceId = oModel.ModelDeviceId,
                TypeDeviceId = oVtype.TypeDeviceId
            };
            model.Entity<DeviceEntity>().HasData(oDevice);
            #endregion

            #region MAINTENANCE
            MaintenanceEntity oMaintenance = new MaintenanceEntity { MaintenanceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId, TechnicianId=otech.TechnicianId };
            #endregion

            #region INCIDENT
            IncidentEntity oIncident = new IncidentEntity { IncidentId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId ,TechnicianId = otech.TechnicianId};
            #endregion

            #region WARRANTY
            WarrantyEntity oWarranty = new WarrantyEntity { Id = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId, StatusId=oStatus.StatusId  };
            model.Entity<WarrantyEntity>().HasData(oWarranty);
            #endregion

            #region REPLACEMENT
            ReplacementDeviceEntity oReplace = new ReplacementDeviceEntity { ReplacementDeviceId = Guid.NewGuid().ToString(), DeviceId = oDevice.DeviceId };
            model.Entity<ReplacementDeviceEntity>().HasData(oReplace);
            #endregion

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);
            GenerateSeedOfData(modelBuilder);
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
