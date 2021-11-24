using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;
namespace Business
{
    public static class B_Status
    {
        public static List<StatusEntity> GetStatus()
        {
            using ( var db = new RayosNoDataContext())
            {
                return db.Status.ToList();
            }
        }
    }
}
