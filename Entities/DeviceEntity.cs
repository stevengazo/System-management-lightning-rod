using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public float Longitude { get; set; }
        /// <summary>
        /// Latitud of the device
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Country were the device is installed
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Date of the installation of the device
        /// </summary>
        public DateTime InstallationDate { get; set; }
        /// <summary>
        /// Limit date of every year to made the device
        /// </summary>
        public DateTime RecomendedDateOfMaintenance { get; set; }
        /// <summary>
        /// type of the device (Sale, rent, leasing, proof)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Model of the device
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Especificate if the device is operative 
        /// </summary>
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

        // Relation With Replacements
        public ICollection<ReplacementDeviceEntity> Replacements { get; set; }

        // Relation With Incidents
        public ICollection<IncidentEntity> Incidents { get; set; }
    }
}
