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
        public static void CreateUser(IdentityUser User, string password, UserManager<IdentityUser> userManager)
        {
         var result =  userManager.CreateAsync(User, password);
         
        }


        public static void AddRole(string UserId,string RoleId)
        {
            using (var IdentityDB = new IDBContext())
            {
                IdentityUserRole<string> userRole = new IdentityUserRole<string>() { UserId=UserId, RoleId=RoleId};

                IdentityDB.UserRoles.Add(userRole);
                IdentityDB.SaveChanges();
            }
        }
        public static IdentityUser GetUser(string UserId)
        {
            using (var IdentityDB = new IDBContext())
            {
                var query = (from user in IdentityDB.Users select user).Where(U => U.Id == UserId).FirstOrDefault();
                return query;
            }
        }


        public static List<IdentityRole> GetListOfRolesByUser(string UserId)
        {
            using (var IdentityDB = new IDBContext())
            {
                var query = (from rol in IdentityDB.UserRoles select rol).Where(UR => UR.UserId == UserId).ToList();
                var roles= new List<IdentityRole>(); 
                foreach(var i in query)
                {
                    var query1 = IdentityDB.Roles.Find(i.RoleId);
                    if(query1 != null)
                    {
                        roles.Add(query1);
                    }
                }
                return roles;
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
