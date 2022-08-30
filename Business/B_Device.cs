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
        /// Get an return n number of rows in the database
        /// </summary>
        /// <param name="Quantity">Quantity of devices to return</param>
        /// <param name="skip">Quantity of devices to skip</param>
        /// <returns>list of devices </returns>
        public static List<DeviceEntity> TakeDevices(int Quantity = 50, int skip= 0)
        {
            using ( var DB = new RayosNoDataContext())
            {
                List<DeviceEntity> aux = new List<DeviceEntity>();
                if(skip <=1 )
                {
                    aux = (from rows in DB.Devices select rows).Include(S => S.SaleMan).Include(C => C.Client).Take(Quantity).ToList();
                }
                else
                {
                    int QuantityToSkip = Quantity * skip;
                    aux = (from rows in DB.Devices select rows).Include(S => S.SaleMan).Include(C => C.Client).Skip(QuantityToSkip).Take(Quantity).ToList();
                }
                return aux;
            }
        }

        /// <summary>
        /// Consult and return all the Devices in the Database
        /// </summary>
        /// <returns>Return a list of Devices</returns>
        public static List<DeviceEntity> ListOfDevices(string _SalemanId = "")
        {
            using (var Db = new RayosNoDataContext())
            {
                if (_SalemanId.Length == 0)
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).ToList();
                }
                else
                {
                    return Db.Devices.Include(S => S.SaleMan).Include(C => C.Client).Where(D => D.SaleManId == _SalemanId).ToList();
                }

            }
        }

        /// <summary>
        /// Create a new Device object
        /// </summary>
        /// <param name="oDevice">Object type Device</param>
        public static async void CreateDevice(DeviceEntity oDevice)
        {
            using (var Db = new RayosNoDataContext())
            {
                oDevice.DeviceId = oDevice.DeviceId.ToUpper();
                Db.Devices.Add(oDevice);
                Db.SaveChanges();
            }
            // Crea una carpeta para el numero de serie del dispositivo
            await B_StorageManage.createFolderAsync("/", oDevice.DeviceId);
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
                var BandEstado = HaveDependence(oDevice);
                if (BandEstado)
                {
                    DB.Devices.Remove(oDevice);
                    DB.SaveChanges();
                }

            }
        }



        #endregion

        #region Search and Consults

        /// <summary>
        /// Check the limit dates of any device in the database in a especific month
        /// </summary>
        /// <param name="idMonth">number of month to search</param>
        /// <returns>List of devices</returns>
        public static List<DeviceEntity> GetDevWithOutMainByMonth(int idMonth= 0)
        {      
            try
            {
                using(  var  db = new RayosNoDataContext())
                {
                    if( idMonth!= 0)
                    {
                        return (
                                        from device
                                        in db.Devices
                                        where (device.RecomendedDateOfMaintenance.Year == DateTime.Today.Year) && (device.RecomendedDateOfMaintenance.Month == idMonth)
                                        orderby device.RecomendedDateOfMaintenance ascending
                                        select device
                                    ).Include(D => D.Client)
                                     .Include(D => D.SaleMan).ToList();
                    }
                    else
                    {
                        return null;
                    }
                    
                }
            }
            catch( Exception rf)
            {
                Console.WriteLine($"Error {rf.Message}");
                return null;
            }
        }

        public static List<DeviceEntity> GetDevicesBySaleman(string id)
        {
            try
            {
                using (var db = new RayosNoDataContext())
                {
                    var ResultDevices = (from devi
                                         in db.Devices
                                         where devi.SaleManId == id
                                         select devi
                                         ).Include(D => D.Client)
                                         .Include(D => D.Country)
                                         .Include(D => D.SaleMan)
                                         .Include(D => D.TypeDevice)
                                         .Include(D => D.ModelDevice)
                                         .Include(D => D.installer)
                                         .ToList();
                    return ResultDevices;
                }
            }
            catch (Exception b)
            {
                Console.WriteLine($"Error: {b.Message}");
                return null;
            }
        }
        public static Dictionary<int,int> GetDevicesByYear()
        {
            Dictionary<int, int> Devices = new Dictionary<int, int>();
            using (var DB = new RayosNoDataContext())
            {
                try
                {
                    var tmpYear = (from dev in DB.Devices select dev.InstallationDate.Year).Distinct().ToList();
                    foreach (var item in tmpYear)
                    {
                        var QuantityOfDevices = (
                            from dev in DB.Devices
                            where dev.InstallationDate.Year == item
                            select dev
                            ).ToList().Count();

                        Devices.Add(item, QuantityOfDevices);
                    }
                    return Devices;
                }
                catch(Exception b)
                {
                    Console.WriteLine($"Error: {b.Message}");
                    return (new Dictionary<int, int>()); 
                }
                
            }            
        }
        
        public static List<DeviceEntity> GetListofDevicesActive()
        {
            Dictionary<string, string> Devices = new Dictionary<string, string>();
            using (var DB = new RayosNoDataContext())
            {
                var aux = (from Device in DB.Devices select Device).Where(D => D.IsActive == true).Include(D => D.Client).ToList();
                return aux;
            }
        }



        /// <summary>
        /// Get the id of the devices actives and the namme of the client
        /// </summary>
        /// <returns>Dictionary of DeviceIds (Key) and Clients names (Value)</returns>
        public static Dictionary<string,string> GetDictonaryOfDevicesWithClient()
        {
            Dictionary<string, string> Devices = new Dictionary<string, string>();
            using( var DB= new RayosNoDataContext())
            {
                var aux = (from Device in DB.Devices select Device).Where(D=>D.IsActive== true).Include(D => D.Client);
                var tmp =  (
                    from dev in aux
                    where dev.IsActive == true
                    orderby dev.Client.Name
                    select new { Key = dev.DeviceId, Value = dev.Client.Name }).ToDictionary(D => D.Key, D => D.Value)
                    ;

                return tmp;
            }
        }

        /// <summary>
        /// Get a list of devices linked with a specific Installer 
        /// </summary>
        /// <param name="installerId">Id of the installer to check</param>
        /// <returns>Null if present error, List of DeviceEntity</returns>
        public static List<DeviceEntity> GetDevicesByInstaller ( string installerId)
        {
            try
            {                
                using(var db = new RayosNoDataContext())
                {
                    return (from dev in db.Devices select dev).Include(D=>D.Client).Include(D=>D.SaleMan).Where(D => D.InstallerId == installerId).ToList();
                }
            }catch(Exception f)
            {
                Console.WriteLine($"Error: {f.Message}");
                return null;
            }
        }

        public static int CountDevicesBySaleMan(string Salemanid = null)
        {
            using (var DB = new RayosNoDataContext())
            {
                if (Salemanid != null)
                {
                    return (from device in DB.Devices select device).Where(D => D.SaleManId == Salemanid).Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        public static int CountDevicesByClient( string ClientId= null)
        {
            using( var DB = new RayosNoDataContext())
            {
                if (ClientId != null)
                {
                    return  (from device in DB.Devices select device).Where(D => D.ClientId == ClientId).Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Search the devices of the saleman and return the devices 
        /// </summary>
        /// <param name="_SaleMan">id to Search</param>
        /// <param name="Quantity">Quantity of devices to return</param>
        /// <param name="Page">Number of page</param>
        /// <returns></returns>
        public static List<DeviceEntity> GetDevicesBySaleman(string _SaleMan = null, int Quantity = 10, int Page = 1)
        {
            List<DeviceEntity> Devices = new List<DeviceEntity>();
            int skipping = 0;
            if (Page >= 2)
            {
                skipping = Quantity * Page;
            }
            using (var DB = new RayosNoDataContext())
            {
                var aux = (from Device in DB.Devices select Device).Where(D => D.SaleManId == _SaleMan).Skip(skipping).Take(Quantity).Include(C => C.Client).Include(S => S.SaleMan);
                Devices = aux.ToList();
            }
            return Devices;
        }



        public static List<DeviceEntity> GetDevicesByClient(string _ClientId = null)
        {
            List<DeviceEntity> Devices = new List<DeviceEntity>();
          
            using (var DB = new RayosNoDataContext())
            {
                if (_ClientId != null)
                {
                    var aux = (from Device in DB.Devices select Device).Where(D => D.ClientId == _ClientId)
                        .Include(C => C.Client)
                        .Include(C => C.installer)
                        .Include(C => C.TypeDevice)
                        .Include(C => C.ModelDevice)
                        .Include(S => S.SaleMan);
                    Devices = aux.ToList();
                }
                else
                {
                    return null; 
                }
            }
            return Devices;
        }


        public static List<DeviceEntity> GetDevicesByClient(string _ClientId= null, int Quantity=10, int Page=1 )
        {
            List<DeviceEntity> Devices = new List<DeviceEntity>();
            int skipping = 0;
            if(Page >= 2)
            {
                skipping = Quantity * Page;
            }
            using( var DB = new RayosNoDataContext())
            {
                var aux = (from Device in DB.Devices select Device).Where(D => D.ClientId == _ClientId).Skip(skipping).Take(Quantity).Include(C => C.Client).Include(S => S.SaleMan);
                Devices = aux.ToList();
            }
            return  Devices;
        }
        /// <summary>
        /// Get the countries registered in the Database
        /// </summary>
        /// <returns>List of countries</returns>
        public static List<CountryEntity> GetCountries()
        {
            using( var DB = new RayosNoDataContext())
            {
                return (from country in DB.Countries select country).Distinct().ToList();
            }
        }

        /// <summary>
        /// Consult an existance device in the database
        /// </summary>
        /// <param name="_DeviceId">Device To search</param>
        /// <param name="_Alias">Alias to search</param>
        /// <param name="_Year">Year of installation</param>
        /// <returns>list</returns>
        public static List<DeviceEntity> GetDevicesByConsult( string _DeviceId =null, string _Alias=null, int _Year=0,string _Country = null)
        {
            List<DeviceEntity> Devices = new List<DeviceEntity>();
            using (var DB = new RayosNoDataContext())
            {
                IQueryable<DeviceEntity> qDevices;
                if( (_Year == 0)||(_Country==null))
                {
                    if(_Year==0 && _Country== null)
                    {
                        qDevices = DB.Devices.FromSqlInterpolated($@"EXEC SearchDevice @_DeviceId = {_DeviceId},	@_Alias = {_Alias},	@_Year = null,@_CountryId = null");
                        Devices = qDevices.ToList();
                    }
                    else if (_Year == 0 && _Country != null)
                    {
                        qDevices = DB.Devices.FromSqlInterpolated($@"EXEC SearchDevice @_DeviceId = {_DeviceId},	@_Alias = {_Alias},	@_Year = null,@_CountryId = {_Country.ToString()}");
                        Devices = qDevices.ToList();
                    }
                    else if (_Year != 0 && _Country == null)
                    {
                        qDevices = DB.Devices.FromSqlInterpolated($@"EXEC SearchDevice @_DeviceId = {_DeviceId},	@_Alias = {_Alias},	@_Year = {_Year.ToString()},@_CountryId = null");
                        Devices = qDevices.ToList();
                    }
                }
                else
                {
                        qDevices = DB.Devices.FromSqlInterpolated($@"EXEC SearchDevice @_DeviceId = {_DeviceId},	@_Alias = {_Alias},	@_Year = {_Year.ToString()},@_CountryId = {_Country.ToString()}");
                        Devices = qDevices.ToList();
                }
                if( Devices.Count != 0)
                {
                    var clientsIds = (from clid in Devices select clid.ClientId).Distinct().ToArray();
                    var SalemanIds = (from salId in Devices select salId.SaleManId).Distinct().ToArray();
                    List<ClientEntity> cli = new List<ClientEntity>();
                    List<SaleManEntity> sal = new List<SaleManEntity>();
                    foreach (var item in clientsIds)
                    {
                        cli = (from client in DB.Clients select client).Where(C => C.Id == item).ToList();
                    }
                    foreach (var item in SalemanIds)
                    {
                        sal = (from saleman in DB.Salemans select saleman).Where(C => C.SaleManId == item).ToList();
                    }
                    var query = (
                        from Client in cli
                        join dev in Devices on Client.Id equals dev.ClientId
                            into table
                        from subDev in table.DefaultIfEmpty()
                        select subDev
                        ).ToList();
                }               
                return Devices;
            }
        }


        /// <summary>
        /// Get the years of installation registered in the table 
        /// </summary>
        /// <returns>list of years</returns>
        public static List<int> ListOfYears()
        {            
            using(var DB = new RayosNoDataContext())
            {
                var Years= (from Devices in DB.Devices select Devices.InstallationDate.Year).Distinct().ToList();
                return Years;
            }
        }

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
                var aux = DB.Devices.OrderBy(I=>I.DeviceId).Skip(skipping).Take(NumberOfDevices).Include(S => S.SaleMan).Include(C => C.Client);
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
        /// <returns>true if not exists a dependency</returns>
        public static bool HaveDependence(DeviceEntity deviceEntity)
        {
            var stg = deviceEntity.DeviceId;
            using (var DB = new RayosNoDataContext())
            {
                var cMaintenance = DB.Maintenances.FromSqlInterpolated($"Select * from Maintenances Where DeviceId ={stg}").FirstOrDefault();
                var cIncident = DB.Incidents.FromSqlInterpolated($"Select * from Incidents Where DeviceId ={stg}").FirstOrDefault();
                var cWarranty = DB.Warranties.FromSqlInterpolated($"Select * from Warranties Where DeviceId ={stg}").FirstOrDefault();
                var cReplacement = DB.Replacements.FromSqlInterpolated($"Select * from Replacements Where DeviceId ={stg}").FirstOrDefault();
                if ((cMaintenance == null) && (cWarranty == null) && (cReplacement == null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Get a Device by the _SalemanId using Linq
        /// </summary>
        /// <param name="id">Device _SalemanId</param>
        /// <returns>objet type Device</returns>
        public static DeviceEntity DeviceById(string id)
        {
            using (var Db = new RayosNoDataContext())
            {
                var query = Db.Devices.Include(D => D.SaleMan).Include(D => D.Client).Include(D => D.Country).Include(D => D.ModelDevice).Include(D => D.TypeDevice).ToList().LastOrDefault(D => D.DeviceId == id);
                return query;
            }
        }


        #endregion
    }

}
