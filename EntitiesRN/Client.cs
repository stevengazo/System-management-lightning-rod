﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntitiesRN
{
    class Client
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
