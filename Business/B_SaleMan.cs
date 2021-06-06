using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_SaleMan
    {
        public  static List<SaleManEntity> ListOfSaleMans()
        {
            using(var DB = new RayosNoDataContext())
            {
                return DB.Salemans.ToList();
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
        public static bool HaveDependence( SaleManEntity oSaleMan)
        {
            using (var Db = new RayosNoDataContext())
            {
                var query = Db.Devices.Where(D => D.SaleManId == oSaleMan.SaleManId);
                if( query == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
