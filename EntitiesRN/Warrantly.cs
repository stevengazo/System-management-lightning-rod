﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesRN
{
    class Warranty
    {
        [Required]
        public string Id { get; set; }
        public DateTime DateSend { get; set; }
        public DateTime DateReceived { get; set; }       
        public string Notes { get; set; }

    }
}
