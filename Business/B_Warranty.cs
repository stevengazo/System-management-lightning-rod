using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public class B_Warranty
    {
        public List<WarrantyEntity> ListOfWarranties()
        {
            using (var DB= new RayosNoDataContext())
            {
                return DB.Warranties.ToList();
            }
        }
        public void Create (WarrantyEntity oWarranty)
        {
            using(var DB = new RayosNoDataContext())
            {
                DB.Warranties.Add(oWarranty);
                DB.SaveChanges();
            }
        }
        public void Update(WarrantyEntity oWarranty)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Warranties.Update(oWarranty);
                DB.SaveChanges();
            }
        }

        public void Delete(WarrantyEntity oWarranty)
        {
            using( var DB = new RayosNoDataContext())
            {
                DB.Warranties.Remove(oWarranty);
                DB.SaveChanges();
            }
        }

    }
}
