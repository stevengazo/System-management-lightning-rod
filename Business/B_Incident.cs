using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Incident
    {
        /// <summary>
        /// Consult and return the list of incidents
        /// </summary>
        /// <returns></returns>
        public static List<IncidentEntity> GetListOfIncidents()
        {
            using(var DB = new RayosNoDataContext())
            {
                return (DB.Incidents.ToList());
            }
        }

        public static void CreateIncident(IncidentEntity oIncident)
        {
            using (var DB= new RayosNoDataContext())
            {
                oIncident.IncidentId = Guid.NewGuid().ToString();
                DB.Incidents.Add(oIncident);
                DB.SaveChanges();
            }
        }

        public static void Delete ( IncidentEntity oIncident)
        {
            using(var DB= new RayosNoDataContext())
            {
                DB.Incidents.Remove(oIncident);
                DB.SaveChanges();
            }
        }
        public static void Update( IncidentEntity oIncident)
        {
            using(var DB = new RayosNoDataContext())
            {
                DB.Incidents.Update(oIncident);
                DB.SaveChanges();
            }
        }
    }
}
