using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class ModelDeviceEntity
    {
        [Key]
        [Required]
        public int ModelDeviceId { get; set; }
        public string ModelDeviceName { get; set; }
        //Relation with Device
        public ICollection<DeviceEntity> Devices { get; set; }
    }
}
