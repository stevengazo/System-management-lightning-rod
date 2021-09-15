using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class StatusEntity
    {
        [Key]
        [Required]
        public int StatusId { get; set; }
        [MaxLength(20)]
        public string StatusName { get; set; }

        // relation with warranties
        public ICollection<WarrantyEntity> Warranties { get; set; }
    }
}
