using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        [Key]
        [Required]
        public string CategoryId { get; set; }
        public string CategoryType { get; set; }
        public ICollection<ClientEntity> Clients { get; set;}

    }
}
