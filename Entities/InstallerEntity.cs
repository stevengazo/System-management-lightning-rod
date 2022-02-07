using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class InstallerEntity
        {/// <summary>
        /// Id the table 
        /// </summary>
        [Required]
        [Key]
        public string InstallerId { get; set; }
        /// <summary>
        /// Name of the installer
        /// </summary>
        [Required]        
        public string Name { get; set; }

        /// <summary>
        /// Date of begin as installer of Grupo Mecsa 
        /// </summary>
        public DateTime initDate { get; set; }

        /// Relation with Device 
        public ICollection<DeviceEntity> devices { get; set; }
                

    }
}
