using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public class B_SaleMan
    {
        public List<SaleManEntity> ListOfSaleMans()
        {
            using(var DB = new RayosNoDataContext())
            {
                return DB.Salemans.ToList();
            }
        }
        public void CreateSaleMan(SaleManEntity oSaleMan)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Salemans.Add(oSaleMan);
                DB.SaveChanges();
            }
        }

        public void UpdateSaleMan(SaleManEntity oSaleMan)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Salemans.Update(oSaleMan);
                DB.SaveChanges();
            }
        }
        public void DeleteSaleMan(SaleManEntity oSaleMan)
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
        public bool HaveDependence( SaleManEntity oSaleMan)
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
