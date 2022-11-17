using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace Business
{
    public static class B_Warranty
    {

        /// <summary>
        /// Get the list of devices without maintenance in specific year
        /// </summary>
        /// <param name="year">the year to consult</param>
        /// <returns>list of devices without maintenances</returns>
        public static List<DeviceEntity> GetDevicesWithoutWarrantiyesByYear(string year)
        {
            using (var DB = new RayosNoDataContext())
            {
                try
                {
                    List<DeviceEntity> oDevices = new List<DeviceEntity>();
                    ///Execute a Stored Procedure saved in the DB
                    var query = DB.Devices.FromSqlInterpolated($"Exec GetDevicesWithoutWarrantiesByYear @_Year={year.ToString()}");
                    //Convert the IQueryable result, to list of DeviceEntity to send to the 
                    var clients = new List<ClientEntity>();
                    var models = new List<ModelDeviceEntity>();
                    var salemans = new List<SaleManEntity>();
                    var typeofdevice = new List<TypeDeviceEntity>();
                    var countries = new List<CountryEntity>();
                    using (var db = new RayosNoDataContext())
                    {
                        countries = db.Countries.ToList();
                        clients = db.Clients.ToList();
                        models = db.ModelDevices.ToList();
                        salemans = db.Salemans.ToList();
                        typeofdevice = db.TypesDevices.ToList();

                    }

                    foreach (var i in query)
                    {

                        var tmp = i.ClientId; ;
                        i.Client = clients.Find(C => C.Id == tmp);
                        i.SaleMan = salemans.Find(S => S.SaleManId == i.SaleManId);
                        i.ModelDevice = models.Find(M => M.ModelDeviceId == i.ModelDeviceId);
                        i.TypeDevice = typeofdevice.Find(T => T.TypeDeviceId == i.TypeDeviceId);
                        i.Country = countries.Find(C => C.CountryId == i.CountryId);
                    }
                    oDevices = query.ToList();
                    return oDevices;
                }
                catch (Exception d)
                {
                    Console.WriteLine(d.Message);
                    List<DeviceEntity> i = new List<DeviceEntity>();
                    return i;
                }
            }
        }

        public static List<WarrantyEntity> SearchWarranties( string _DeviceId= null, string _Estatus= null, int _Year=0)
        {
            using(var DB = new RayosNoDataContext())
            {
                List<WarrantyEntity> aux = new List<WarrantyEntity>();
                if ((_DeviceId != null) && (_Estatus != null) && (_Year != 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    Where	(	DeviceId like {"%"+_DeviceId+"%"} )                                                          
                                                                    and		(	YEAR(DateSend)= {_Year.ToString()}	)
                                                                    and		(	StatusId = {_Estatus})").ToList();
                }
                else if ((_DeviceId != null) && (_Estatus != null) && (_Year == 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    Where	(	DeviceId like {"%" + _DeviceId + "%"} )                                                          
                                                                   and		(	StatusId = {_Estatus})").ToList();
                }
                else if ((_DeviceId != null) && (_Estatus == null) && (_Year == 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    Where	(	DeviceId like {"%" + _DeviceId + "%"})").ToList();
                }
                else if ((_DeviceId != null) && (_Estatus != null) && (_Year != 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    WHERE	( DeviceId like {"%" + _DeviceId + "%"} )                                                                                                                    
                                                                    and		(	YEAR(DateSend)= {_Year.ToString()}	)
                                                                    and		(	StatusId = {_Estatus})").ToList();
                }
                else if ((_DeviceId == null) && (_Estatus != null) && (_Year != 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    WHERE	(	YEAR(DateSend)= {_Year.ToString()}	)
                                                                    and		(	StatusId = {_Estatus})").ToList();
                }
                else if ((_DeviceId == null) && (_Estatus != null) && (_Year == 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    WHERE	StatusId = {_Estatus}").ToList();
                }
                else if ((_DeviceId == null) && (_Estatus == null) && (_Year != 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                                    WHERE	(	YEAR(DateSend)= {_Year.ToString()}	)").ToList();
                }
                else if ((_DeviceId != null) && (_Estatus == null) && (_Year != 0))
                {
                    aux = DB.Warranties.FromSqlInterpolated($@"SELECT * FROM Warranties
                                                               where	(	DeviceId like {"%" + _DeviceId + "%"} )                                                                                                                 
                                                               and		(	YEAR(DateSend)= {_Year.ToString()}	)").ToList();
                }

                return aux.ToList();
            }
            
        }
        /// <summary>
        /// Search all the registered years in the warranties and return
        /// </summary>
        /// <returns>List of years registered</returns>
        public static List<int> GetYearsOfSend()
        {
            using (var DB = new RayosNoDataContext())
            {
                var aux = (from Warranty in DB.Warranties select Warranty.DateSend.Year).Distinct().ToList();
                return aux;
            }
        }
        public static List<WarrantyEntity> GetPaging(int NumberOfPage = 0, int Quantity = 40)
        {
            int Skipping = 0;
            if (NumberOfPage <= 1)
            {
                Skipping = 0;
            }
            else
            {
                Skipping = (NumberOfPage - 1) * Quantity;
            }
            using( var DB = new RayosNoDataContext())
            {
                var aux = DB.Warranties.Skip(Skipping).Take(Quantity).OrderByDescending(W => W.DateSend);
                return aux.ToList();
            }

        }
        public static int Count()
        {
            using( var DB = new RayosNoDataContext())
            {
                var aux = (from Warranty in DB.Warranties select Warranty).Count();
                return aux;
            }
        }
        public static WarrantyEntity WarrantyById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Warranties.Include(D => D.Status).FirstOrDefault( W=>W.Id == id);
            }
        }
        public static List<WarrantyEntity> ListOfWarrantiesById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                IEnumerable<WarrantyEntity> aux = DB.Warranties.OrderBy(W => W.DeviceId).Where(W=>W.DeviceId== id);
                return aux.ToList();
            }
        }


        #region CRUD
        public static List<WarrantyEntity> ListOfWarranties()
        {
            using (var DB= new RayosNoDataContext())
            {
                IEnumerable<WarrantyEntity> aux = DB.Warranties.OrderBy(W=>W.DeviceId);
                return aux.ToList();
            }
        }
        public static async Task<bool> CreateAsync (WarrantyEntity oWarranty)
        {
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    await DB.Warranties.AddAsync(oWarranty);
                    await DB.SaveChangesAsync();
                }
                await B_StorageManage.createFolderAsync($"/{oWarranty.DeviceId}/{oWarranty.DateSend.Year.ToString()}-Warranties", oWarranty.Id);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static void Update(WarrantyEntity oWarranty)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Warranties.Update(oWarranty);
                DB.SaveChanges();
            }
        }

        public static void Delete(WarrantyEntity oWarranty)
        {
            using( var DB = new RayosNoDataContext())
            {
                DB.Warranties.Remove(oWarranty);
                DB.SaveChanges();
            }
        }
        #endregion
    }
}
