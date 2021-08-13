using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore; 

namespace Business
{
    public static class B_Incident
    {
        #region CRUD
        /// <summary>
        /// Create a new incident in the database
        /// </summary>
        /// <param name="oIncident">Incident to registered</param>
        public static void CreateIncident(IncidentEntity oIncident)
        {
            using (var DB = new RayosNoDataContext())
            {
                oIncident.IncidentId = Guid.NewGuid().ToString();
                DB.Incidents.Add(oIncident);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oIncident"></param>
        public static void Delete(IncidentEntity oIncident)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Incidents.Remove(oIncident);
                DB.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// Search all the incidents of a specific device in the database 
        /// </summary>
        /// <param name="_DeviceIdToSearch">Id of the device to search</param>
        /// <returns>List of incidents</returns>
        public static List<IncidentEntity> GetListOfIncidentsByDevice(string _DeviceIdToSearch)
        {
            using(var DB = new RayosNoDataContext())
            {
                return  (from Incident in DB.Incidents select Incident).Where(I => I.DeviceId == _DeviceIdToSearch).Include(I=>I.Device).Include(I=>I.Device.Client).Include(I => I.Device.SaleMan).ToList();
            }
        }

        /// <summary>
        /// Consult and return the list of incidents
        /// </summary>
        /// <returns></returns>
        public static List<IncidentEntity> GetListOfIncidents()
        {
            using(var DB = new RayosNoDataContext())
            {
                List<IncidentEntity> incidents = new List<IncidentEntity>();
                incidents = DB.Incidents.Include(D => D.Device).Include(C => C.Device.Client).ToList();
                return incidents;
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


        public static IncidentEntity GetIncidentById(string IncidentId)
        {
            using (var DB = new RayosNoDataContext())
            {
                var aux = DB.Incidents.FromSqlInterpolated($"Select * from Incidents where Incidents.IncidentId={IncidentId}").Include(D=>D.Device).FirstOrDefault();                
                return aux;
            }
        }
    }
}
 