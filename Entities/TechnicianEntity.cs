using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class TechnicianEntity
    {
        [Key]
        [Required]
        public int TechnicianId { get; set; }
        [MaxLength(40)]
        public string TechnicianName { get; set; }

        // Relation with Incidents
        public ICollection<IncidentEntity> Incidents { get; set; }
        // Relation with Maintenances 
        public ICollection<MaintenanceEntity> Maintenances { get; set; }
    }
}
