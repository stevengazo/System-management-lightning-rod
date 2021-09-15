using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ClientEntity
    {
        [Required]
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public int SectorId { get; set; }
        public SectorEntity Sector { get; set; }

        //Relation with Device
        public ICollection<DeviceEntity> Devices { get; set; }

    }
}
