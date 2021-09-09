using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TypeDeviceEntity
    {
        [Key]
        [Required]
        public int TypeDeviceId { get; set; }
        [MaxLength(30)]
        public string TypeDeviceName { get; set; }

        /// Relation with Device  
        public ICollection<DeviceEntity> Devices { get; set; }
    }
}
