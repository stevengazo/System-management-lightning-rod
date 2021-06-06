using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public class B_Maintenance
    {
        /// <summary>
        /// Return the list of Maintenance
        /// </summary>
        /// <returns>List of Maintenance</returns>
        public List<MaintenanceEntity> ListOfMaintenance()
        {
            using ( var DB = new RayosNoDataContext())
            {
                return DB.Maintenances.ToList();
            }
        }

        /// <summary>
        /// Create a new Maintenance
        /// </summary>
        /// <param name="oMaintenance">New object to insert</param>
        public void Create(MaintenanceEntity oMaintenance)
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
        public void Update ( MaintenanceEntity oMaintenance)
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
        public void Delete ( MaintenanceEntity oMaintenance)
        {
            using(var DB = new RayosNoDataContext())
            {
                DB.Maintenances.Remove(oMaintenance);
                DB.SaveChanges();
            }
        }

    }
}
