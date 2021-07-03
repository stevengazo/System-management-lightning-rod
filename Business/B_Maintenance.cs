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
    public static class B_Maintenance
    {
        public static List<MaintenanceEntity> ListOfMaintenancesById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                IEnumerable<MaintenanceEntity> aux = DB.Maintenances.OrderBy(M => M.DeviceId).Where(D=>D.DeviceId==id);               
                return aux.ToList();
            }
        }
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
