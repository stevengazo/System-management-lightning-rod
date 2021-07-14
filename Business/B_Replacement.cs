using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public  static class B_Replacement
    {   /// <summary>
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
    }
}
