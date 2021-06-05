using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesRN
{
    class ReplacementDeviceEntity
    {
        [Required]
        public string ReplacementDeviceId { get; set; }
        public string NewSerieDevice { get; set; }
        public string Notes { get; set; }

        //Relation with Device 
        public string DeviceId { get; set; }
        public DeviceEntity Device { get; set; }

    }
}
