using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesRN
{
    class Device
    {
        [Required]
        //SerialNumber
        public string  Id { get; set; }
        public string Alias { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Country { get; set; }
        public DateTime InstallationDate { get; set; }
        public string MaintenanceMonth { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public bool IsActive { get; set; }

    }
}
