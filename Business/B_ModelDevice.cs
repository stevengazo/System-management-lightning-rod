using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;
namespace Business
{
    public static class B_ModelDevice
    {

        #region CRUD
        /// <summary>
        /// Create a new model of device in the database
        /// </summary>
        /// <param name="oModel">Model to create </param>
        public static void CreateModel(ModelDeviceEntity oModel)
        {
            using(var db = new RayosNoDataContext())
            {
                db.ModelDevices.Add(oModel);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Enlist all the records in the table ModelsDevices
        /// </summary>
        /// <returns></returns>
        public static List<ModelDeviceEntity> ListOfModels()
        {
            using(var db = new RayosNoDataContext())
            {
                return db.ModelDevices.ToList();
            }
        }
        /// <summary>
        /// Update an exist Model in the database 
        /// </summary>
        /// <param name="oModel"></param>
        public static void UpdateModel(ModelDeviceEntity oModel)
        {
            using(var db= new RayosNoDataContext())
            {
                db.ModelDevices.Update(oModel);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete an exist model in the database 
        /// </summary>
        /// <param name="Omodel"></param>
        public static void DeleteModel(ModelDeviceEntity Omodel)
        {
            using(var db= new RayosNoDataContext())
            {
                db.ModelDevices.Remove(Omodel);
                db.SaveChanges();
            }
        }

        #endregion
        #region Consult
        public static ModelDeviceEntity GetModelById(int _id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.ModelDevices.FirstOrDefault(M => M.ModelDeviceId== _id);
            }
        }


        public static bool HasDependences (int _id)
        {
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    var query = (from device in DB.Devices select device).FirstOrDefault(D=>D.ModelDeviceId== _id); 
                    if(query!=null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckIfExistsId(int _id)
        {
            ModelDeviceEntity query;
            using (var DB = new RayosNoDataContext())
            {
                query = DB.ModelDevices.FirstOrDefault(M => M.ModelDeviceId == _id);
                if(query != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
