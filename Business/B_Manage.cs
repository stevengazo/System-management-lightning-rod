using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;
using IdentityDataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Business
{
    public static class B_Manage
    {
        public static void CreateUser()
        {
            using (var IdentityDB = new IDBContext())
            {
                var roleStore = (IRoleStore<IdentityRole>)new RoleStore<IdentityRole>(IdentityDB);
                var roleManager = new RoleManager<IdentityRole>(roleStore, null, null, null, null);

                var userStore = new UserStore<IdentityUser>(IdentityDB);
                var userManager = new UserManager<IdentityUser>()


            }
        }

    } 
}


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
