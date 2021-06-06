using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public class B_Replacement
    {
        public List<ReplacementDeviceEntity> ListOfReplacement()
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Replacements.ToList();
            }
        }
        public void Create(ReplacementDeviceEntity oReplacement)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Replacements.Add(oReplacement);
                DB.SaveChanges();
            }
        }

        public void Update(ReplacementDeviceEntity oReplacement) 
        { 
            using (var DB = new RayosNoDataContext())
            {
                DB.Replacements.Update(oReplacement);
                DB.SaveChanges();
            }
        }

        public void Delete(ReplacementDeviceEntity oReplacement)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Replacements.Remove(oReplacement);
                DB.SaveChanges();
            }
        }
    }
}
