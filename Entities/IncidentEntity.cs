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
        /// <summary>
        /// Date of the report
        /// </summary>
        public DateTime ReportDate { get; set; }
        /// <summary>
        /// Date of the incident
        /// </summary>
        public DateTime IncidentDate { get; set; }
        /// <summary>
        /// Date of assistance of the technician and review the device
        /// </summary>
        public DateTime RevisionDate { get; set; }
        /// <summary>
        /// Day when Mecsa send the report for the customer
        /// </summary>
        public DateTime SendReportDate { get; set; }
        /// <summary>
        /// Description of the incident made it by the client
        /// </summary>
        [MaxLength(800)]
        public string ReportDescripcion { get; set; }
        /// <summary>
        /// Description of the incident and information of the status of the system
        /// </summary>
        [MaxLength(800)]
        public string RevisionInformation { get; set; }
        /// <summary>
        /// Information about the status of the device
        /// </summary>
        [MaxLength(800)]
        public string DDCEStatus { get; set; }
        /// <summary>
        /// Recomendations maded by the Technician in site
        /// </summary>
        [MaxLength(800)]       
        public string Recomentations { get; set; }
        /// <summary>
        /// Person of Mecsa who assitance to the place 
        /// </summary>
        public string TechnicianName { get; set; }
        /// <summary>
        /// Person who made the report
        /// </summary>
        public string ContactReportingName { get; set; }


        //Relation With Device 
        public string DeviceId { get; set; }
        public DeviceEntity Device { get; set; }
    }
}
