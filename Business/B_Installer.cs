using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public static class B_Installer
    {
        /// <summary>
        ///  Try to update an exist row in the table installers 
        /// </summary>
        /// <param name="objInstaller">Object to update</param>
        /// <returns>True if update the installer, false if present erros</returns>
        public static async Task UpdateInstaller(InstallerEntity objInstaller)
        {
            try
            {
                using var db = new RayosNoDataContext();
                db.Installers.Update(objInstaller);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }

        /// <summary>
        /// Search the last entity in the database 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public static InstallerEntity GetLastInstaller( string _id)
        {            
            try
            {
                using var db = new RayosNoDataContext();
                var tmp = (from inst
                           in db.Installers
                           orderby inst.InstallerId descending
                           where inst.InstallerId.Contains(_id)
                           select inst
                           ).FirstOrDefault();
                return tmp;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// get a especific register in the database with the id
        /// </summary>
        /// <param name="_id">id to search in the database</param>
        /// <returns>Object type installer or null if not exist</returns>
        public static InstallerEntity GetInstallerById(string _id)
        {
            try
            {
                using var db = new RayosNoDataContext();
                var tmp = db.Installers.FirstOrDefault(I => I.InstallerId.Equals(_id));
                return tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Determinate if exist a register with the specific
        /// </summary>
        /// <param name="_id">id to search</param>
        /// <returns>True if exist the register in the DB, false if not exist or present error </returns>
        public static bool Exist(string _id)
        {
            try
            {
                using var db = new RayosNoDataContext();
                var tmpResult = db.Installers.FirstOrDefault(I => I.InstallerId == _id);
                if (tmpResult != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Add a new register in the database
        /// </summary>
        /// <param name="objInstaller">Objet to insert in the database</param>
        /// <returns>True if insert the object, False if present error</returns>
        public static bool AddInstaller(InstallerEntity objInstaller)
        {
            try
            {
                using var db = new RayosNoDataContext();
                db.Installers.Add(objInstaller);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Check if exist dependencies of a specific Entity
        /// </summary>
        /// <param name="id">Id of the installer to search</param>
        /// <returns>True if exists dependencies</returns>
        public static async Task<bool> checkDependencies(string id)
        {            
            try
            {
                using var DB = new RayosNoDataContext();
                var results = await (from dev in DB.Devices
                                     where dev.InstallerId == id
                                     select dev
                    ).ToListAsync();
                if (results.Count != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
                return false;
            }
        }


        /// <summary>
        /// Delete an exist object in the database
        /// </summary>
        /// <param name="id">Id of the installer</param>      
        public static async Task Delete(string id)
        {
            try
            {
                var installer = GetInstallerById(id);
                using var db = new RayosNoDataContext();
                db.Installers.Remove(installer);
                await db.SaveChangesAsync();
            }
            catch (Exception e)            
            {
                Console.Write(e.Message);
            }
        }

        /// <summary>
        /// Get all the installers registered in the DB
        /// </summary>
        /// <returns></returns>
        public  static List<InstallerEntity> GetInstallers()
        {
            try
            {
                using var db = new RayosNoDataContext();
                return db.Installers.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }


        public static async Task<List<InstallerEntity>> Search(string id = null, string name = null)
        {
            try
            {
                using var db = new RayosNoDataContext();
                List<InstallerEntity> result = new();
                if (id != null && name != null)
                {
                    result = await (
                            from inst
                            in db.Installers
                            where inst.InstallerId.Contains(id) && inst.Name.Contains(name)
                            select inst).ToListAsync();
                }
                else if (id == null && name != null)
                {
                    result = await (
                            from inst
                            in db.Installers
                            where inst.Name.Contains(name)
                            select inst).ToListAsync();
                }
                else if (id != null && name == null)
                {
                    result = await (
                            from inst
                            in db.Installers
                            where inst.InstallerId.Contains(id)
                            select inst).ToListAsync();
                }
                if (result.Count != 0)
                {
                    return result.ToList();
                }
                else
                {
                    return new List<InstallerEntity>();
                }

            }
            catch(Exception f)
            {
                Console.WriteLine(f.Message);
                throw new NotImplementedException();
            }
        }
    }
}
