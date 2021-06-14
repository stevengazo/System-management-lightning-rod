using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IdentityDataAccess
{

    public class IDBContext : IdentityDbContext
    {
        #region Internal Attributes
        /// <summary>
        /// Temporal Connection String (for internal use of the .dll)
        /// </summary>
        internal string MyConnectionString { get; set; }
        internal IConfiguration Configuration { get; set; }
        #endregion

        /// <summary>
        /// Create a new configuration builder and connect to appsettings.json and read the 
        /// connection string and set the value
        /// </summary>
        public string GetConnectionString(string connectionStringName = "IdentityConnection")
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json")
                              .AddEnvironmentVariables();
            Configuration = builder.Build();
            MyConnectionString = Configuration.GetConnectionString(connectionStringName);
            return MyConnectionString;
        }

        public IDBContext(DbContextOptions<IDBContext> options) : base(options)
        {

        }

       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           
        }


    }
}
