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
        public static List<TechnicianEntity>  GetListOftechnicians()
        {
            using (var DB = new RayosNoDataContext())
            {
                var aux =DB.Technicians.ToList();
                return aux;
            }
        }
    }
}
