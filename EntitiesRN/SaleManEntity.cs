using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesRN
{
    class SaleManEntity
    {
        [Required]
        public string SaleManId { get; set; }
        public string Name { get; set; }
        public string QuantityOfDevice { get; set; }

        //Relation with Device 
        public ICollection<DeviceEntity> Devices { get; set; }
    }
}
