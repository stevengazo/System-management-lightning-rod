using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Warranty
    {
        public static WarrantyEntity WarrantyById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Warranties.ToList().LastOrDefault(W => W.Id == id);
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
        public  static List<WarrantyEntity> ListOfWarranties()
        {
            using (var DB= new RayosNoDataContext())
            {
                IEnumerable<WarrantyEntity> aux = DB.Warranties.OrderBy(W=>W.DeviceId);
                return aux.ToList();
            }
        }
        public  static void Create (WarrantyEntity oWarranty)
        {
            using(var DB = new RayosNoDataContext())
            {
                DB.Warranties.Add(oWarranty);
                DB.SaveChanges();
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

    }
}
