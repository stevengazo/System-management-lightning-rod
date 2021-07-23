﻿using System;
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
        /// Method to consult and get the list of maintenances in a especific year and month
        /// </summary>
        /// <param name="Year">Year to consult</param>
        /// <param name="Month">Month to consult</param>
        /// <returns>List of maintenances</returns>
        public static List<MaintenanceEntity> GetMaintenancesByYearAndMonth(string Year="0001", string Month = "1")
        {
            using(var DB = new RayosNoDataContext())
            { 
                List<MaintenanceEntity> maintenances = new List<MaintenanceEntity>();
                var aux = DB.Maintenances.FromSqlInterpolated($@"   Select *
                                                                    From Maintenances
                                                                    Where(Year(MaintenanceDate) = {Year.ToString()}) and(MONTH(MaintenanceDate) = {Month.ToString()})");
                if((aux.Count())== 0)
                {
                    maintenances = aux.ToList();                    
                }
                else
                {
                   maintenances = aux.ToList();
                }
                return maintenances;
            }
        }

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
                    ///Execute a Stored Procedure saved in the DB
                    var query = DB.Devices.FromSqlInterpolated($"Exec GetDeviceByUnreallizedMaintenanceByYear @_Year={year.ToString()}");
                    //Convert the IQueryable result, to list of DeviceEntity to send to the 
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
        /// Update the Device and return the Maintenance
        /// </summary>
        /// <param name="_DeviceId">Id of the device</param>
        /// <returns></returns>
        public static void UpdateDevice(string _DeviceId)
        {
            using ( var DB = new RayosNoDataContext())
            {
                try
                {
                    var oMaintenance = new MaintenanceEntity();
                   var aux = DB.Database.ExecuteSqlInterpolated($@"
                                                                        	UPDATE Devices
	                                                                        SET Devices.RecomendedDateOfMaintenance = DATEADD(YEAR,1, tmpM.MaintenanceDate)
	                                                                        FROM Devices , (SELECT MAX(M.MaintenanceDate) as MaintenanceDate, M.DeviceId
					                                                                        FROM Maintenances AS M
					                                                                        WHERE M.DeviceId = {_DeviceId}
					                                                                        GROUP BY M.DeviceId) AS tmpM
	                                                                        where Devices.DeviceId = tmpM.DeviceId
                    ");
                }
                catch (Exception f)
                {
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
                    if (item.Year != DateTime.Today.Year)
                    {
                        aux.Add(item.Year);
                    }                    
                }
                YearList.Add(DateTime.Today.Year);
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
