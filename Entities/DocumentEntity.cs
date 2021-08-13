using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class DocumentEntity
    {
        [Key]
        [Required]
        public string DocumentId { get; set; }
        [Required]
        public string TypeOfDocument { get; set; }
        
        public string Author { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
