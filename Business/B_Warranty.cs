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
        public  static async Task Create (WarrantyEntity oWarranty)
        {
            var path = oWarranty.DeviceId.ToString();
            var relativePath = $"{path}/{oWarranty.DateSend.Year.ToString()}-Warranty";
            try
            {
                /* BASE PATH*/
               await B_StorageManage.createFolder(relativePath, oWarranty.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                using (var DB = new RayosNoDataContext())
                {
                    oWarranty.FilesPaths = $"{relativePath}/{oWarranty.Id}";
                    DB.Warranties.Add(oWarranty);
                    DB.SaveChanges();
                }
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
