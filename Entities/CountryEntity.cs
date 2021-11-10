using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public   class CountryEntity
    {
        [Key]
        [Required]
        [MaxLength(4)]
        public string CountryId { get; set; }

        public string CountryName { get; set; }


        //Relation with Device
        public ICollection<DeviceEntity> Devices { get; set; }

    }
}
