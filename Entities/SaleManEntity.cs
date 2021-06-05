using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
 public   class SaleManEntity
    {
        [Required]
        [Key]
        public string SaleManId { get; set; }
        public string Name { get; set; }
        public string QuantityOfDevice { get; set; }

        //Relation with Device 
        public ICollection<DeviceEntity> Devices { get; set; }
    }
}
