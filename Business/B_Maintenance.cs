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
        #region Consults and others
       
        public static List<MaintenanceEntity> GetMaintenancesByConsult(string _Device = "", string _Name ="",int _Year = 0, int _Month=0)
        {
            List<MaintenanceEntity> Maintenances = new List<MaintenanceEntity>();
            using(var DB = new RayosNoDataContext())
            {
                if ((_Device != null) && (_Name != null) && (_Year != 0) && (_Month != 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	MaintenanceId like Concat('%',{_Device},'%') 
		                                                                and (TechnicianName like CONCAT('%',{_Name},'%'))
		                                                                and (YEAR(MaintenanceDate ) = {_Year.ToString()}) 
		                                                                and (MONTH(maintenanceDate) = {_Month.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device != null) && (_Name == null) && (_Year == 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	DeviceId like Concat('%',{_Device},'%')");
                    Maintenances = aux.ToList();
                }
                else if ((_Device != null) && (_Name != null) && (_Year == 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	MaintenanceId like Concat('%',{_Device},'%') 
		                                                                and (TechnicianName like CONCAT('%',{_Name},'%'))");
                    Maintenances = aux.ToList();
                }
                else if ((_Device != null) && (_Name != null) && (_Year != 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	MaintenanceId like Concat('%',{_Device},'%') 
		                                                                and (TechnicianName like CONCAT('%',{_Name},'%'))
		                                                                and (YEAR(MaintenanceDate ) = {_Year.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name == null) && (_Year == 0) && (_Month != 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE (MONTH(maintenanceDate) = {_Month.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name == null) && (_Year != 0) && (_Month != 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	 (YEAR(MaintenanceDate ) = {_Year.ToString()}) 
		                                                                and (MONTH(maintenanceDate) = {_Month.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name != null) && (_Year != 0) && (_Month != 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	(TechnicianName like CONCAT('%',{_Name},'%'))
		                                                                and (YEAR(MaintenanceDate ) = {_Year.ToString()}) 
		                                                                and (MONTH(maintenanceDate) = {_Month.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device != null) && (_Name == null) && (_Year != 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	MaintenanceId like Concat('%',{_Device},'%') 		                                                                
		                                                                and (YEAR(MaintenanceDate ) = {_Year.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name != null) && (_Year == 0) && (_Month != 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	(TechnicianName like CONCAT('%',{_Name},'%'))
		                                                                and (YEAR(MaintenanceDate ) = {_Year.ToString()})");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name != null) && (_Year == 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	(TechnicianName like CONCAT('%',{_Name},'%'))");
                    Maintenances = aux.ToList();
                }
                else if ((_Device == null) && (_Name == null) && (_Year != 0) && (_Month == 0))
                {
                    var aux = DB.Maintenances.FromSqlInterpolated($@"   SELECT * FROM MAINTENANCES
                                                                        WHERE	(YEAR(MaintenanceDate ) = {_Year.ToString()})");
                    Maintenances = aux.ToList();
                }
            }            
            return Maintenances;
        }

        /// <summary>
        /// Count all the records in the table Maintenances
        /// </summary>
        /// <returns>number of Maintenances</returns>
        public static int CountMaintenances()
        {           
            using(var DB= new RayosNoDataContext())
            {
                var aux = (from Maintenance in DB.Maintenances select Maintenance).Count();
                return aux;
            }
        }

        /// <summary>
        /// Get the years of the Maintenance Dates registered in the database
        /// </summary>
        /// <returns>list of years</returns>
        public static List<int> GetYears()
        {
            using( var DB = new RayosNoDataContext())
            {
                var aux = (from Maintenance in DB.Maintenances select Maintenance.MaintenanceDate.Year).Distinct().ToList();
                return aux;
            }
        }

        /// <summary>
        /// Get the Technians registered in the table Maintenances
        /// </summary>
        /// <returns>list of tecnicians</returns>
        public static List<TechnicianEntity> GetTechnicians()
        {
            using(var DB = new RayosNoDataContext())
            {
                var aux = (from Techni in DB.Technicians select Techni).Distinct().ToList();
                return aux;
            }
        }

        /// <summary>
        /// Get a specific quantity of maintenances and retun
        /// </summary>
        /// <param name="NumberOfPage">number of page </param>
        /// <param name="QuantityOfMaintenances">quantity of devices to return</param>
        /// <returns>list of devices to return</returns>
        public static List<MaintenanceEntity> GetPagingMaintenances(int NumberOfPage = 0, int QuantityOfMaintenances = 40)
        {
            int Skipping = 0;
            if(NumberOfPage<= 1)
            {
                Skipping = 0;
            }
            else
            {
                Skipping = (NumberOfPage - 1) * QuantityOfMaintenances;
            }
            using(var DB = new RayosNoDataContext())
            {
                var Maintenances = DB.Maintenances.Skip(Skipping).Take(QuantityOfMaintenances).OrderByDescending(M => M.MaintenanceDate).Include(M => M.Device);
                return Maintenances.ToList();
            }
        }


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
                IEnumerable<MaintenanceEntity> aux = DB.Maintenances.OrderBy(M => M.DeviceId).Where(D=>D.DeviceId==DeviceId).Include(M=>M.Technician);               
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
                return DB.Maintenances.Include(D=>D.Device).Include(T=>T.Technician).ToList().LastOrDefault(M => M.MaintenanceId == id);   
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
        #endregion

        #region CRUD
        /// <summary>
        /// Create a new Maintenance
        /// </summary>
        /// <param name="oMaintenance">New object to insert</param>
        public static void Create(MaintenanceEntity oMaintenance)
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
        #endregion
    }
}
