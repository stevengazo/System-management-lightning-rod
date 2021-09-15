using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SectorEntity
    {
        [Key]
        [Required]
        public int SectorId { get; set; }

        public string SectorName { get; set; }

        ///Relation with Clients 
        
        public ICollection<ClientEntity> Clients { get; set; }
    }
}
