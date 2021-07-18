using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class IncidentEntity
    {
        [Required]
        [Key]
        public string IncidentId { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public DateTime RevisionDate { get; set; }
        [MaxLength(800)]
        public string ReportDescripcion { get; set; }
        [MaxLength(800)]
        public string RevisionInformation { get; set; }
        [MaxLength(800)]
        public string DDCEStatus { get; set; }
        public float SpatOhms { get; set; }
        public float DeviceOhms { get; set; }
        public float Ampers { get; set; }
        [MaxLength(800)]
        public string Recomentations { get; set; }
        public string TechnicianName { get; set; }

        //Relation With Device 
        public string DeviceId { get; set; }
        public DeviceEntity Device { get; set; }
    }
}
