﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DeviceEntity
    {
        [Required]
        [Key]
        //SerialNumber
        public string DeviceId { get; set; }
        public string Alias { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Country { get; set; }
        public DateTime InstallationDate { get; set; }
        public string MaintenanceMonth { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public bool IsActive { get; set; }

        // Relation with Client
        public string ClientId { get; set; }
        public ClientEntity Client { get; set; }

        // Relation with SaleMan
        public string SaleManId { get; set; }
        public SaleManEntity SaleMan { get; set; }

        //Relation with Maintenance
        public ICollection<MaintenanceEntity> Maintenances { get; set; }

        // Relation with Warranty
        public ICollection<WarrantyEntity> Warranties { get; set; }

        // Relation With Device 
        public ICollection<ReplacementDeviceEntity> Replacements { get; set; }
    }
}
