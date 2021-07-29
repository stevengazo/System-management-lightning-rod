using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Device
    {
        #region CRUD

        /// <summary>
        /// Consult and return all the Devices in the Database
        /// </summary>
        /// <returns>Return a list of Devices</returns>
        public static List<DeviceEntity> ListOfDevices(string id = "")
        {
            using (var Db = new RayosNoDataContext())
            {
                if (id.Length == 0)
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).ToList();
                }
                else
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).Where(D => D.SaleManId == id).ToList();
                }

            }
        }

        /// <summary>
        /// Create a new Device object
        /// </summary>
        /// <param name="oDevice">Object type Device</param>
        public static void CreateDevice(DeviceEntity oDevice)
        {
            using (var Db = new RayosNoDataContext())
            {
                oDevice.DeviceId = oDevice.DeviceId.ToUpper();
                Db.Devices.Add(oDevice);
                Db.SaveChanges();
            }
        }

        /// <summary>
        /// Update a attributes of a Device in the database
        /// </summary>
        /// <param name="oDevice">Objet type Device</param>
        public static void UpdateDevice(DeviceEntity oDevice)
        {
            using (var Db = new RayosNoDataContext())
            {
                Db.Devices.Update(oDevice);
                Db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete Device from the table Devices 
        /// </summary>
        /// <param name="oDevice">Object to remove</param>
        public static void DeleteDevice(DeviceEntity oDevice)
        {
            using (var DB = new RayosNoDataContext())
            {
                var query = DB.Devices.LastOrDefault(d => d.DeviceId == oDevice.DeviceId);
                var BandEstado = HaveDependence(oDevice);
                if (BandEstado)
                {
                    DB.Devices.Remove(query);
                    DB.SaveChanges();
                }
            }
        }



        #endregion

        #region Search and Consults
        


        /// <summary>
        /// Count the Devices in the Data Base
        /// </summary>
        /// <returns>number of rows</returns>
        public static int CountDevices()
        {
            using (var DB = new RayosNoDataContext())
            {
                var aux = (from Device in DB.Devices select Device).Count();
                return aux;
            }
        }
        /// <summary>
        /// Get the list of devices by number of paging
        /// </summary>
        /// <param name="NumberOfPage">Number of the page</param>
        /// <param name="NumberOfDevices">Quantity of devices to get back</param>
        /// <returns></returns>
        public static List<DeviceEntity> GetPagingDevices (int NumberOfPage= 0, int NumberOfDevices = 30)
        {
            int skipping = 0;
            if (NumberOfPage<= 1)
            {
                skipping = 0;
            }
            else
            {
                skipping = (NumberOfPage - 1) * NumberOfDevices;
            }
            
            using(var DB = new RayosNoDataContext())
            {
                var aux = DB.Devices.OrderBy(I=>I.ClientId).Include(S => S.SaleMan).Include(C => C.Client).Skip(skipping).Take(NumberOfDevices);
                return aux.ToList();
            }
        }
        /// <summary>
        /// Search a device by the Device Id
        /// </summary>
        /// <param name="_SearchDeviceId">Device Id</param>
        /// <returns></returns>
        public static List<DeviceEntity> GetSearchOfDevice(string _SearchDeviceId = "0")
        {
            return (GetSearchOfDevice(_SearchDeviceId,"",""));
        }
        /// <summary>
        /// Search a Device in the table with the device Id
        /// </summary>
        /// <param name="SearchDeviceId">string to consult</param>
        /// <returns>List of devices </returns>
        public static List<DeviceEntity> GetSearchOfDevice(string _SearchDeviceId= "0", string _ClientId = "", string _SaleManId = "")
        {
            using ( var DB = new RayosNoDataContext())
            {
                List<DeviceEntity> oDevices = new List<DeviceEntity>();
                var aux = DB.Devices.FromSqlInterpolated($"exec dbo.SearchDeviceByDeviceId @_DeviceSearch ={_SearchDeviceId}");
                foreach (var item in aux)
                {
                    item.Client = B_Client.ClientById(item.ClientId);
                    item.SaleMan = B_SaleMan.SaleManById(item.SaleManId);
                    oDevices.Add(item);
                }
                return oDevices;
            }
        }
        /// <summary>
        /// Check if exists dependences in other tables with Linq
        /// </summary>
        /// <param name="deviceEntity">Entity to search</param>
        /// <returns>true if exists a dependency</returns>
        public static bool HaveDependence(DeviceEntity deviceEntity)
        {
            var stg = deviceEntity.DeviceId;
            using (var DB = new RayosNoDataContext())
            {
                var cMaintenance = DB.Maintenances.Last(M => M.DeviceId == stg);
                var cWarranty = DB.Warranties.Last(W => W.DeviceId == stg);
                var cReplacement = DB.Replacements.Last(R => R.DeviceId == stg);
                if ((cMaintenance == null) || (cWarranty == null) || (cReplacement == null))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// Get a Device by the id using Linq
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns>objet type Device</returns>
        public static DeviceEntity DeviceById(string id)
        {
            using (var Db = new RayosNoDataContext())
            {
                return Db.Devices.Include(D=>D.SaleMan).Include(D=>D.Client).ToList().LastOrDefault(D=>D.DeviceId==id);
            }
        }


        #endregion
    }

}
