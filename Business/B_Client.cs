using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Business
{
    public static class B_Client
    {
        #region CRUD
        /// <summary>
        /// Create a new Client in the table Clients
        /// </summary>
        /// <param name="oClient">Objet Type ClientEntity</param>
        public static void CreateClient(ClientEntity oClient)
        {
            using (var DB = new RayosNoDataContext())
            {
                DB.Clients.Add(oClient);
                DB.SaveChanges();
            }
        }
        /// <summary>
        /// Return the list of Clients in the database
        /// </summary>
        /// <returns>return all the devices in the database</returns>
        public static List<ClientEntity> ListOfClients()
        {
            using (var DB = new RayosNoDataContext())
            {
                IEnumerable<ClientEntity> aux = DB.Clients.ToList().OrderBy(C => C.Name);
                return aux.ToList();
            }
        }
        /// <summary>
        /// Consult if the Client to delete have dependences and Delete the Object
        /// </summary>
        /// <param name="oClient">Client to delete</param>
        public static void Delete(ClientEntity oClient)
        {
            var BandDependence = HaveDependence(oClient);
            if (!BandDependence)
            {
                using (var DB = new RayosNoDataContext())
                {
                    DB.Remove(oClient);
                    DB.SaveChanges();
                }
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
        #endregion
        #region ConsultsAndSearch

        /// <summary>
        /// Search clients in the database
        /// </summary>
        /// <param name="_name">Name of the client</param>
        /// <param name="_Id">Id of the client in the database</param>
        /// <returns>List of clients</returns>
        public static List<ClientEntity> SearchClients(string _name = null, string _Id = null, int _SectorId = 0)
        {
            List<ClientEntity> Clients = new List<ClientEntity>();
            using (var DB = new RayosNoDataContext())
            {
                IQueryable<ClientEntity> quer;
                if ( (_SectorId!=0) )
                {
                    quer = DB.Clients.FromSqlInterpolated($@"Exec SearchClients @_idToSearch ={_Id}, @_name = {_name}, @_SectorId = {_SectorId.ToString()}");
                }
                else
                {
                    quer = DB.Clients.FromSqlInterpolated($@"Exec SearchClients @_idToSearch ={_Id}, @_name = {_name}, @_SectorId = null");
                }

                                
                Clients = quer.ToList();
            }
            return Clients;
        }
    

        /// <summary>
        /// Search clieents by name in the database
        /// </summary>
        /// <param name="Name">name to search </param>
        /// <returns>list of results</returns>
        public static List<ClientEntity> SearchClientsByName(string Name = "")
        {
            using(var DB= new RayosNoDataContext())
            {
                var query = DB.Clients.FromSqlInterpolated(@$"exec dbo.SearchClientByName @_Name ={Name}");
                var aux = query.ToList();
                return aux;
            }
        }
        /// <summary>
        /// Search a client by the id in the database
        /// </summary>
        /// <param name="id">Id to search</param>
        /// <returns>Objetc</returns>
        public static ClientEntity ClientById(string id)
        {
            using (var DB = new RayosNoDataContext())
            {
                
                return DB.Clients.ToList().LastOrDefault(C=>C.Id == id);
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
        #endregion
    }

}
