using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;
namespace Business
{



    public static class B_Technician
    {

        #region CRUD    
        /// <summary>
        /// Search in the DB all the technician and return
        /// </summary>
        /// <returns>List of Technicians</returns>
        public static List<TechnicianEntity> GetListOftechnicians()
        {
            using (var DB = new RayosNoDataContext())
            {
                var aux = DB.Technicians.ToList();
                return aux;
            }
        }

        /// <summary>
        /// Create a new Techinician
        /// </summary>
        /// <param name="technician">Technician to create </param>
        public static void Create(TechnicianEntity technician)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Technicians.Add(technician);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Update an exists row in the table Technician
        /// </summary>
        /// <param name="technician">Object to update</param>
        public static void Update(TechnicianEntity technician)
        {
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    DB.Technicians.Update(technician);
                    DB.SaveChanges();
                }
            }
            catch (Exception f)
            {
                Console.WriteLine($"The function of update technician fail. Error: {f.Message}");
            }
        }

        /// <summary>
        /// Delete a technician from the database 
        /// </summary>
        /// <param name="technicianEntity">Technician to delete</param>
        public static void Delete(TechnicianEntity technicianEntity)
        {
            try
            {
                bool BandDependences = HaveDependences(technicianEntity.TechnicianId.ToString());
                if (BandDependences)
                {
                    using (var DB = new RayosNoDataContext())
                    {
                        DB.Technicians.Remove(technicianEntity);
                        DB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The delete function of the technician present error: {e.Message}");
            }
        }
      

        #endregion


        #region Consulting

        /// <summary>
        /// Seach a technician in the database
        /// </summary>
        /// <param name="Id">Id to search</param>
        /// <returns></returns>
        public static TechnicianEntity GetTechnicianById(int Id)
        {
            using (var DB = new RayosNoDataContext())
            {
                var query = (from tech in DB.Technicians select tech).Where(T => T.TechnicianId == Id).FirstOrDefault();
                return query;
            }

        }
        /// <summary>
        /// Seach a technician in the database
        /// </summary>
        /// <param name="Id">Id to search</param>
        /// <returns></returns>
        public static bool CheckId(int Id)
        {
            using (var DB = new RayosNoDataContext())
            {
                var query = (from tech in DB.Technicians select tech).Where(T => T.TechnicianId == Id).FirstOrDefault();
                if (query != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// Search if exists dependences in others tables and return a boolean
        /// </summary>
        /// <param name="id">Technician Id to Search</param>
        /// <returns>True if not exists dependences, False if have dependences or present error</returns>
        public static bool HaveDependences(string id)
        {
            try
            {
                using (var DB = new RayosNoDataContext())
                {
                    var query = (from Maintenance in DB.Maintenances select Maintenance).Where(T => T.TechnicianId.Equals(id)).ToList().Count;
                    if(query <= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write($"Se presento un error en la función HaveDependences en la clase B_Technician : {e}");
                return false;
            }
            
        }

        #endregion

    }
}
