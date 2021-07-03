﻿using System;
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

        public static DeviceEntity DeviceById(string id)
        {
            using (var Db = new RayosNoDataContext())
            {
                return Db.Devices.Include(D=>D.SaleMan).Include(D=>D.Client).ToList().LastOrDefault(D=>D.DeviceId==id);
            }
        }
        /// <summary>
        /// Consult and return all the Devices in the Database
        /// </summary>
        /// <returns>Return a list of Devices</returns>
        public static List<DeviceEntity> ListOfDevices(string id="")
        {
            using (var Db = new RayosNoDataContext())
            {
                if (id.Length == 0)
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).ToList();
                }
                else
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).Where(D=>D.SaleManId==id).ToList();
                }
                
            }
        }
        /// <summary>
        /// Create a new Device object
        /// </summary>
        /// <param name="oDevice">Object type Device</param>
        public static void CreateDevice( DeviceEntity oDevice)
        {
            using(var Db = new RayosNoDataContext())
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
        public static void UpdateDevice( DeviceEntity oDevice)
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
        public static void DeleteDevice( DeviceEntity oDevice)
        {
            using( var DB = new RayosNoDataContext())
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
        
       
    }
}
