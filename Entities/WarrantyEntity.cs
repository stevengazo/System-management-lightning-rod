using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class WarrantyEntity
    {
        [Required]
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime DateSend { get; set; }
        public DateTime DateReceived { get; set; }       
        public string Notes { get; set; }
        [Required]
        public string Estatus { get; set; }
        // Relation with Device
        public string DeviceId { get; set; }
        public DeviceEntity Device { get; set; }
        // Relation with Status
        public StatusEntity Status { get; set; }

        public int StatusId { get; set; }


    }
}
