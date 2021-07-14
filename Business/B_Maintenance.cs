using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Business
{
    public static class B_Maintenance
    {

        /// <summary>
        /// Get the list of devices without maintenance in specific year
        /// </summary>
        /// <param name="year">the year to consult</param>
        /// <returns>list of devices without maintenances</returns>
        public static List<DeviceEntity> GetDevicesWithoutMaintenancesByYear(string year)
        {
            using(var DB = new RayosNoDataContext())
            {
                try
                {
                    List<DeviceEntity> oDevices = new List<DeviceEntity>();
                    var query = DB.Devices.FromSqlInterpolated($"Exec GetDeviceByUnreallizedMaintenanceByYear @_Year={year.ToString()}");
                    oDevices = query.ToList();
                    return oDevices;
                }
                catch (Exception d)
                {
                    List<DeviceEntity> i = new List<DeviceEntity>();
                    return i;
                }
            }
        }
        /// <summary>
        /// Get the maintenances by the device id
        /// </summary>
        /// <param name="DeviceId">device to search</param>
        /// <returns>list of maintenances</returns>
        public static List<MaintenanceEntity> GetMaintenancesByDeviceId(string DeviceId)
        {
            using(var DB = new RayosNoDataContext())
            {                
                var query= DB.Maintenances.FromSqlInterpolated($"Select * from Maintenances where Maintenances.DeviceId={DeviceId}").ToList();
                return query;
            }
        }
       
        /// <summary>
        /// Return a list of the years registered in the rows in Maintenances
        /// </summary>
        /// <returns>list of integers</returns>
        public static List<int> ListOfYears()
        {
            List<int> YearList = new List<int>();
            List<int> aux = new List<int>();
            using (var DB = new RayosNoDataContext())
            {
                var MaintenancesDates = from data in DB.Maintenances    
                                        select (
                                          data.MaintenanceDate
                                        ) ;
                foreach (var item in MaintenancesDates)
                {
                    aux.Add(item.Year);
                }
                YearList.AddRange(aux.Distinct());
                YearList.Sort();
                return YearList;
            }
        }

        /// <summary>
        /// Get the list of maintenances of a device
        /// </summary>
        /// <param name="DeviceId">device to search</param>
        /// <returns>List of maintenances of a specific device</returns>
        public static List<MaintenanceEntity> ListOfMaintenancesById(string DeviceId)
        {
            using (var DB = new RayosNoDataContext())
            {
                IEnumerable<MaintenanceEntity> aux = DB.Maintenances.OrderBy(M => M.DeviceId).Where(D=>D.DeviceId==DeviceId);               
                return aux.ToList();
            }
        }
        /// <summary>
        /// Get the Maintenance by their id
        /// </summary>
        /// <param name="id">id of the maintenance</param>
        /// <returns>Object type maintenance</returns>
        public static MaintenanceEntity MaintenanceById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Maintenances.Include(D=>D.Device).ToList().LastOrDefault(M => M.MaintenanceId == id);   
            }
        }
        /// <summary>
        /// Return the list of Maintenance
        /// </summary>
        /// <returns>List of Maintenance</returns>
        public  static List<MaintenanceEntity> ListOfMaintenance()
        {
            using ( var DB = new RayosNoDataContext())
            {
                IEnumerable<MaintenanceEntity> aux = DB.Maintenances.OrderBy(M=>M.DeviceId);
                return aux.ToList();
            }
        }

        /// <summary>
        /// Get a list of Maintenances by year of reallized
        /// </summary>
        /// <param name="Year">Year of maintenance</param>
        /// <returns>List of Maintenances</returns>
        public static List<MaintenanceEntity> ListOfMaintenanceByYear(int Year)
        {
            using (var DB = new RayosNoDataContext())
            {
                IEnumerable<MaintenanceEntity> aux = DB.Maintenances.OrderBy(M => M.DeviceId).Where(M => M.MaintenanceDate.Year == Year);
                return aux.ToList();
            }
        }

        /// <summary>
        /// Create a new Maintenance
        /// </summary>
        /// <param name="oMaintenance">New object to insert</param>
        public  static void Create(MaintenanceEntity oMaintenance)
        {
            using( var  DB = new RayosNoDataContext())
            {
                DB.Maintenances.Add(oMaintenance);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Update an exist Maintenance in the table Maintenances
        /// </summary>
        /// <param name="oMaintenance"></param>
        public static void Update ( MaintenanceEntity oMaintenance)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Maintenances.Update(oMaintenance);
                DB.SaveChanges();
            }
        }
        /// <summary>
        /// Delete an exist Maintenace
        /// </summary>
        /// <param name="oMaintenance"></param>
        public static void Delete ( MaintenanceEntity oMaintenance)
        {
            using(var DB = new RayosNoDataContext())
            {
                DB.Maintenances.Remove(oMaintenance);
                DB.SaveChanges();
            }
        }

    }
}
