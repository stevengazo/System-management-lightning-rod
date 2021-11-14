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




        private static bool UserStatus(string id)
        {
            using (var db = new IDBContext())
            {
                var query = (from user in db.Users select user).FirstOrDefault(U => U.Id == id);
                if (query.LockoutEnabled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool UpdateUser(IdentityUser _user)
        {
            try
            {
                using (var db = new IDBContext())
                {
                    db.Users.Update(_user);
                    db.SaveChanges();
                        return true;

                }
                return false;
            }
            catch (Exception v)
            {
                Console.WriteLine($"Error {v.Message}");
                return false;
            }
        }




        /// <summary>
        /// Change the confirmation of the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the user is confirmed, false if the user it's not confirmed </returns>
        public static bool ChangeUserStatus(string id)
        {
           
            IdentityUser user = GetUser(id);
            bool BandEstatus = user.LockoutEnabled;

            using (var db = new IDBContext())
            {
                var query = (from use in db.Users select use).FirstOrDefault(U=>U.Id == id);
                query.LockoutEnabled = !BandEstatus;
                db.Users.Update(query);
                db.SaveChanges();
                return !BandEstatus;
            }
            return false;
        }

        /// <summary>
        /// check if exist 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns>true is exist, false if not exist</returns>
        public static bool CheckRole(string UserId, string RoleId)
        {
            using (var IdentityDB = new IDBContext())
            {
                var query = (from ur in IdentityDB.UserRoles select ur).Where(ur => ur.RoleId == RoleId && ur.UserId == UserId).FirstOrDefault();
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
        /// Delete an existant rol of an user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        public static void DeleteRol(string UserId, string RoleId)
        {
            using (var IdentityDB = new IDBContext())
            {
                IdentityUserRole<string> userRole = new IdentityUserRole<string>() { UserId = UserId, RoleId = RoleId };
                IdentityDB.UserRoles.Remove(userRole);
                IdentityDB.SaveChanges();
            }
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
