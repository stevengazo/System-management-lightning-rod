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
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    oIncident.IncidentId = Guid.NewGuid().ToString();
                    DB.Incidents.Add(oIncident);
                    DB.SaveChanges();
                }
            }
            catch (Exception s)
            {
                Console.WriteLine($"{s.Message}");

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
            using (var DB = new RayosNoDataContext())
            {
                return (from Incident in DB.Incidents select Incident).Where(I => I.DeviceId == _DeviceIdToSearch).Include(I => I.Device).Include(I => I.Device.Client).Include(I => I.Device.SaleMan).Include(I => I.Technician).ToList();
            }
        }

        /// <summary>
        /// Consult and return the list of incidents
        /// </summary>
        /// <returns></returns>
        public static List<IncidentEntity> GetListOfIncidents()
        {
            using (var DB = new RayosNoDataContext())
            {
                List<IncidentEntity> incidents = new List<IncidentEntity>();
                incidents = DB.Incidents.Include(D => D.Device).Include(I => I.Technician).Include(C => C.Device.Client).ToList();
                return incidents;
            }
        }


        public static int[] GetIncidentsByYears()
        {
            using (var DB = new RayosNoDataContext())
            {
                return (from incidents in DB.Incidents select incidents.ReportDate.Year).Distinct().ToArray();
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



        /// <summary>
        /// Get the information and execute the Stored Procedure to search the incident
        /// </summary>
        /// <param name="idToSearch">Id of the Device</param>
        /// <param name="aliasToSearch">Alias of the device</param>
        /// <param name="yearToSearch">Year of the incident</param>
        /// <returns></returns>
        public static List<IncidentEntity> SearchIncidents(string idToSearch = null, string aliasToSearch = null, string yearToSearch= null )
        {
            try
            {
                using( var DB = new RayosNoDataContext())
                {
                    var aux = DB.Incidents.FromSqlInterpolated($"EXEC dbo.searchIncidents @_DeviceId = {idToSearch}, @_Alias = {aliasToSearch}, @_Year = {yearToSearch}");
                    foreach( var item in aux)
                    {
                        item.Device = B_Device.DeviceById(item.DeviceId);
                        item.Technician = B_Technician.GetTechnicianById(item.TechnicianId);

                    }
                    return aux.ToList();
                }
            }
            catch(Exception r)
            {
                Console.WriteLine($"Error: {r.Message}");
                return null;

            }
            
        }
        /// <summary>
        /// This functions calculate the total of incidents by year and return the quantities
        /// </summary>
        /// <returns>Dictionary  of integers(year,quantity)</returns>
        public static Dictionary<int,int> QuantitiesOfIncidentsByYear()
        {
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    Dictionary<int, int> tmpDict = new Dictionary<int, int>();
                    var temp = (
                        from incident in DB.Incidents
                        select incident.IncidentDate.Year)
                        .Distinct().ToArray();
                    foreach (var year in temp)
                    {
                        var quantity = (from inciden in DB.Incidents select inciden).Where(I => I.IncidentDate.Year == year).Count();
                        tmpDict.Add(year, quantity);
                    }
                    return tmpDict;
                }
            }            
            catch (Exception r)
            {
                Console.WriteLine($"Error: {r.Message}");
                return null;

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
 