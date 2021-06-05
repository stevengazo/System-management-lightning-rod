using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesRN
{
    class SaleMan
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string QuantityOfDevice { get; set; }
    }
}
