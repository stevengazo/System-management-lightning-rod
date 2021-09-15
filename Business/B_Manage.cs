using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;
using IdentityDataAccess;
using Microsoft.AspNetCore.Identity;
namespace Business
{
    public static class B_Manage
    {
        public static List<IdentityRole> GetListOfRoles()
        {
            using (var IdentityDB = new IDBContext())
            {
                return IdentityDB.Roles.ToList();
            }
        }
        public static List<IdentityUser> ListOfUsers()
        {
            using (var IdentityDB = new IDBContext())
            {
                var query =IdentityDB.Users.ToList();
                return query;
            }
        }
    }
}
