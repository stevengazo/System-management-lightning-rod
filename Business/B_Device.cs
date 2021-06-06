﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Device
    {

        public static DeviceEntity DeviceById(string id)
        {
            using (var Db = new RayosNoDataContext())
            {
                return Db.Devices.ToList().LastOrDefault(D=>D.DeviceId==id);
            }
        }
        /// <summary>
        /// Consult and return all the Devices in the Database
        /// </summary>
        /// <returns>Return a list of Devices</returns>
        public static List<DeviceEntity> ListOfDevices()
        {
            using (var Db = new RayosNoDataContext())
            {
                return Db.Devices.ToList();
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
                DB.Devices.Remove(query);
                DB.SaveChanges();
            }
        }
        
       
    }
}
