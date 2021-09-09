using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Sectors
    {
        #region CRUD    
        public static void CreateSector(SectorEntity oSector)
        {
            using(var db = new RayosNoDataContext())
            {
                db.Sectors.Add(oSector);
                db.SaveChanges();
            }
        }
        public static List<SectorEntity> ListOfSectors()
        {
            using (var db = new RayosNoDataContext())
            {
                return db.Sectors.ToList();
            }
        }
        #endregion
    }
}
