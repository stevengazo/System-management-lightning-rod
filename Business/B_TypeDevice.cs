using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
public static    class B_TypeDevice
    {
        #region CRUD    
        public static void CreateType(TypeDeviceEntity oType)
        {
            using (var db = new RayosNoDataContext())
            {
                db.TypesDevices.Add(oType);
                db.SaveChanges();
            }
        }

        public static List<TypeDeviceEntity> ListOfTDevices()
        {
            using (var db = new RayosNoDataContext())
            {
                return db.TypesDevices.ToList();
            }
        }
        #endregion
        #region CONsults
        public static TypeDeviceEntity GetTypeById (int _id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.TypesDevices.FirstOrDefault(T=>T.TypeDeviceId == _id);
            }
        }
        #endregion
    }
}
