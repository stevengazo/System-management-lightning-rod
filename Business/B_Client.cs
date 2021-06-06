using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Client
    {
        public static ClientEntity ClientById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Clients.ToList().LastOrDefault(C=>C.Id == id);
            }
        }
        /// <summary>
        /// Return the list of Clients in the DB
        /// </summary>
        /// <returns></returns>
        public static List<ClientEntity> ListOfClients()
        {
            using (var DB = new RayosNoDataContext())
            {
                return DB.Clients.ToList();
            }
        }
        /// <summary>
        /// Create a new Client in the table Clients
        /// </summary>
        /// <param name="oClient">Objet Type ClientEntity</param>
        public static void  CreateClient(ClientEntity oClient)
        {
            using(var DB =  new RayosNoDataContext())
            {
                DB.Clients.Add(oClient);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Update an existant client in the Table Clients
        /// </summary>
        /// <param name="oClient">Object to Update</param>
        public static void UpdateClient(ClientEntity oClient)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Clients.Update(oClient);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Consult if the Client to delete have dependences and Delete the Object
        /// </summary>
        /// <param name="oClient">Client to delete</param>
        public  static    void RemoteClient(ClientEntity oClient)
        {
            var BandDependence = HaveDependence(oClient);
            if( !BandDependence)
            {
                using (var DB = new RayosNoDataContext())
                {
                    DB.Remove(oClient);
                    DB.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Consult if the Client have Dependences in others Tables
        /// </summary>
        /// <param name="oClient">Client to consult</param>
        /// <returns>True if the exist dependence, False if not exist dependences</returns>
        public  static bool  HaveDependence(ClientEntity oClient)
        {
            using (var DB = new RayosNoDataContext())
            {
                var query = DB.Devices.Where(D => D.ClientId == oClient.Id);
                if( query.Count() == 0)
                {
                    return false; 
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
