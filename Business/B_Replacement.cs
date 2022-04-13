using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace Business
{
    public  static class B_Replacement
    {

        #region CRUD

        /// <summary>
        /// Get the list of replacements registered
        /// </summary>
        /// <returns>Return a list of replacements</returns>
        public static List<ReplacementDeviceEntity> ListOfReplacement()
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Replacements.ToList();
            }
        }

        /// <summary>
        /// save a new replacement
        /// </summary>
        /// <param name="oReplacement">Object to insert in the DB</param>
        public static void Create(ReplacementDeviceEntity oReplacement)
        {
            using (var DB = new RayosNoDataContext())
            {
                oReplacement.ReplacementDeviceId = Guid.NewGuid().ToString();
                DB.Replacements.Add(oReplacement);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Select and update an existent row in the table replacement
        /// </summary>
        /// <param name="oReplacement">Object to update</param>
        public static void Update(ReplacementDeviceEntity oReplacement) 
        { 
            using (var DB = new RayosNoDataContext())
            {
                DB.Replacements.Update(oReplacement);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Select and delete an existant row in the table replacement
        /// </summary>
        /// <param name="oReplacement">Object to delete</param>
        public static void Delete(ReplacementDeviceEntity oReplacement)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Replacements.Remove(oReplacement);
                DB.SaveChanges();
            }
        }
        #endregion

        #region Consult


        /// <summary>
        /// Get the quantities of replacements by year
        /// </summary>
        /// <returns>Quantity of replacements</returns>
        public static Dictionary<int,int> GetQuantityOfReplacement()
        {
            Dictionary<int,int> dicOfRemplacements = new Dictionary<int,int>();
            try
            {
                using (var db = new RayosNoDataContext())
                {
                    var tmpData = (from repl in db.Replacements select  repl.NewSerieDevice);

                    if(tmpData != null)
                    {
                        foreach (var item in tmpData)
                        {
                            var Year = B_Device.DeviceById(item).InstallationDate.Year;
                            if (dicOfRemplacements.ContainsKey(Year))
                            {
                                dicOfRemplacements[Year] = dicOfRemplacements[Year] + 1;
                            }
                            else
                            {
                                dicOfRemplacements.Add(Year, 1);
                            }
                        }
                        return dicOfRemplacements;
                    }
                    else
                    {
                        return new Dictionary<int, int>();
                    }                    
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error.Message}");
                return new Dictionary<int, int>();
            }

        }


        
        public static ReplacementDeviceEntity GetReplacementById(string ReplacementId = "")
        {
            using(var DB= new RayosNoDataContext())
            {
                var aux = (from Rempl in DB.Replacements select Rempl).FirstOrDefault(R=>R.ReplacementDeviceId.Equals(ReplacementId));
                return aux;
            }
        }

        /// <summary>
        /// Search the ids in the table replacements  and return the results
        /// </summary>
        /// <param name="IdReplacement">Id To Search</param>
        /// <returns>List of Replacements</returns>
        public static List<ReplacementDeviceEntity> ConsultReplacements(string IdReplacement = "")
        {
            List<ReplacementDeviceEntity> Replacements = new List<ReplacementDeviceEntity>();
            using( var DB = new RayosNoDataContext())
            {
                Replacements = (
                                from repl
                                in DB.Replacements
                                where repl.ReplacementDeviceId.Contains(IdReplacement) || repl.DeviceId.Contains(IdReplacement)
                                select repl
                    ).ToList();
            }

            return Replacements;
        }

        #endregion
    }
}
