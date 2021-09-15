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
    public static class B_SaleMan
    {

        
        #region CRUD
        public static List<SaleManEntity> ListOfSaleMans()
        {
            using(var DB = new RayosNoDataContext())
            {
                IEnumerable<SaleManEntity> aux =  DB.Salemans.ToList().OrderBy(S => S.Name);
                return aux.ToList();
            }
        }
        public  static void CreateSaleMan(SaleManEntity oSaleMan)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Salemans.Add(oSaleMan);
                DB.SaveChanges();
            }
        }

        public static void UpdateSaleMan(SaleManEntity oSaleMan)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Salemans.Update(oSaleMan);
                DB.SaveChanges();
            }
        }
        public  static void DeleteSaleMan(SaleManEntity oSaleMan)
        {
            bool BandDependece = HaveDependence(oSaleMan);
            if (!BandDependece)
            {
                using(var DB = new RayosNoDataContext())
                {
                    DB.Salemans.Remove(oSaleMan);
                    DB.SaveChanges();
                }
            }

        }
        #endregion

        #region Consults


        /// <summary>
        /// Search a list of salemans 
        /// </summary>
        /// <param name="_id">id of the Saleman</param>
        /// <param name="_name">name of the saleman</param>
        /// <returns>list of salemans</returns>
        public static List<SaleManEntity> SearchSaleman(string _id=null, string _name =null)
        {
            List<SaleManEntity> salemans = new List<SaleManEntity>();
            using (var DB = new RayosNoDataContext())
            {
                if( (_id != null) && (_name != null))
                {
                    var aux = DB.Salemans.FromSqlInterpolated($@"   SELECT * FROM SALEMANS
                                                                WHERE	(SaleManId like concat('%',{_id},'%'))
                                                                and		(Name LIKE CONCAT('%',{_name},'%'))");
                    salemans = aux.ToList();
                }
                else if ((_id == null) && (_name != null))
                {
                    var aux = DB.Salemans.FromSqlInterpolated($@"   SELECT * FROM SALEMANS
                                                                    WHERE	(Name LIKE CONCAT('%',{_name},'%')) ");
                    salemans = aux.ToList();
                }
                else if ((_id != null) && (_name == null))
                {
                    var aux = DB.Salemans.FromSqlInterpolated($@"   SELECT * FROM SALEMANS
                                                                WHERE	(SaleManId like concat('%',{_id},'%'))");
                    salemans = aux.ToList();
                }

            }
            return salemans;
        }
        public static SaleManEntity SaleManById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {

                return DB.Salemans.ToList().LastOrDefault(S => S.SaleManId == id);
            }
        }


        /// <summary>
        /// Verificate if exists dependences in others tables
        /// </summary>
        /// <param name="oSaleMan">Object with dependences</param>
        /// <returns>True if exist dependences, False if not exist dependences</returns>
        public static bool HaveDependence( SaleManEntity oSaleMan)
        {
            using (var Db = new RayosNoDataContext())
            {  
                var query = Db.Devices.Where(D => D.SaleManId == oSaleMan.SaleManId);
                if ( query.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
    }
}
