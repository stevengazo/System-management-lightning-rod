﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Spatial;
namespace Entities
{
    public class DeviceEntity
    {
        /// <summary>
        /// Serial Number of the device
        /// </summary>
        [Required]
        [Key]
        
        public string DeviceId { get; set; }
        /// <summary>
        /// Alias to identificate the device
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Longitude of the device
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// Latitud of the device
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Date of the installation of the device
        /// </summary>
        public DateTime InstallationDate { get; set; }
        /// <summary>
        /// Limit date of every year to made the device
        /// </summary>
        public DateTime RecomendedDateOfMaintenance { get; set; }
        /// <summary>
        /// Especificate if the device is operative 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// This property only is used if the device is replacement
        /// </summary>
        public bool IsReplaced { get; set; }
        /// <summary>
        /// General notes about the device
        /// </summary>
        public string Note { get; set; }


        // Relation with Model
        public int ModelDeviceId { get; set; }
        public ModelDeviceEntity ModelDevice { get; set; }

        // Relation with Country
        public string CountryId { get; set; }
        public CountryEntity Country { get; set; }

        // Relation with Client
        public string ClientId { get; set; }
        public ClientEntity Client { get; set; }

        // Relation with SaleMan
        public string SaleManId { get; set; }
        public SaleManEntity SaleMan { get; set; }
        /// Relation with Installer
        public string InstallerId { get; set; }
        public InstallerEntity  installer { get; set; }

        /// Relation with TypeDevice
        public int TypeDeviceId { get; set; }
        public TypeDeviceEntity TypeDevice { get; set; }

        //Relation with Maintenance
        public ICollection<MaintenanceEntity> Maintenances { get; set; }

        // Relation with Warranty
        public ICollection<WarrantyEntity> Warranties { get; set; }

        // Relation With Replacements
        public ICollection<ReplacementDeviceEntity> Replacements { get; set; }

        // Relation With Incidents
        public ICollection<IncidentEntity> Incidents { get; set; }
    }
}
