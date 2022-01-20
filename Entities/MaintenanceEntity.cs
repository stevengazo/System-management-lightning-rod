using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
   public  class MaintenanceEntity
    {
        [Key]
        [Required(ErrorMessage ="Información requerida")]
        public string MaintenanceId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        [Required(ErrorMessage ="Información Requerida")]       
        public string StatusOfDevice { get; set; }
        public float SpatOhms { get; set; }
        public float DeviceOhms { get; set; }
        public float Ampers { get; set; }        
        public string ReportId { get; set; }
        [Required(ErrorMessage = "Información Requerida")]        
        public string Recomendations { get; set; }
        //Relation With Device 
        public string DeviceId { get; set; }
        public DeviceEntity Device { get; set; }
        // Relation with Technician
        public TechnicianEntity Technician { get; set; }
        public int TechnicianId { get; set; }


    }
}
