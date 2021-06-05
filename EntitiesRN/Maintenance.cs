using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntitiesRN
{
    class Maintenance
    {
        public string Id { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string StatusOfDevice { get; set; }
        public float SpatOhms { get; set; }
        public float DeviceOhms { get; set; }
        public float Ampers { get; set; }
        public string TechnicianName { get; set; }
        public string ReportId { get; set; }

    }
}
