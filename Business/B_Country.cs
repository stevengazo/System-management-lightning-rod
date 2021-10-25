using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;
namespace Business
{
    public  static class B_Country
    {

        #region CRUD

        /// <summary>
        /// Registered a new country in the database
        /// </summary>
        /// <param name="oCountry">Country to save</param>
        public static void CreateCountry( CountryEntity oCountry)
        {
           
            using (var db = new RayosNoDataContext())
            {
                db.Countries.Add(oCountry);
                db.SaveChanges();
            }
           

        }

        /// <summary>
        /// List the records in the database
        /// </summary>
        /// <returns>List type CountryEntity</returns>
        public  static List<CountryEntity> ListOfCountries()
        {
            using (var DB = new RayosNoDataContext())
            {
                var query = (from country in DB.Countries select country).ToList();
                return query;
            }            
        }


              /// <summary>
        /// Update an exist country
        /// </summary>
        /// <param name="oCountry"></param>
        public static void UpdateCountry( CountryEntity oCountry)
        {
            using (var db = new RayosNoDataContext())
            {
                db.Countries.Update(oCountry);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Delete and exist country in database 
        /// </summary>
        /// <param name="country"></param>
        public static void DeleteCountry(CountryEntity country)
        {
            bool BandExist = HaveDependences(country);
            if (!BandExist)
            {
                using(var db = new RayosNoDataContext())
                {
                    db.Countries.Remove(country);
                    db.SaveChanges();
                }
            }

        }
        #endregion

        #region Consults
        public static CountryEntity GetCountryById(int _id = 0)
        {

            using (var db = new RayosNoDataContext())
            {
                var country = (from count in db.Countries select count).Where(C => C.CountryId == _id).FirstOrDefault();
                return country;
            }
            return null ;
        }


        /// <summary>
        /// Consult if exists any dependence in the table Device
        /// </summary>
        /// <param name="country">Country to search</param>
        /// <returns>False is not exist dependences, True if exist dependences</returns>
        public static bool HaveDependences (CountryEntity country)
        {
            using (var db = new RayosNoDataContext())
            {
                var query = (from dev in db.Devices select dev.Country).Where(D => D.CountryId == country.CountryId).ToList();
                if (query.Count != 0)
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
        /// Look if exist a country in the database by the CountryId
        /// </summary>
        /// <param name="country">Country to search</param>
        /// <returns>True if exitt, False if not exists</returns>
        public static bool ExistCountry (CountryEntity country)
        {
            using(var db = new RayosNoDataContext())
            {
                var query = (from count in db.Countries select count).Where(C => C.CountryId == country.CountryId).ToList();
                if (query.Count>0)
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
        /// Check if exist a country in the db
        /// </summary>
        /// <param name="CountryName">Country to search</param>
        /// <returns>True if exits a country, false if not exist or present an error</returns>
        public static bool CheckName(string CountryName)
        {
            try
            {
                using (var db = new RayosNoDataContext())
                {
                    var query = (from country in db.Countries select country).Where(C => C.CountryName.Equals(CountryName)).ToList();
                    if(query.Count>0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }

        }
        #endregion

    }
}
