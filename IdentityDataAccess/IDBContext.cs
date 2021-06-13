using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDataAccess
{
    public class IDBContext : IdentityDbContext
    {       
        public IDBContext(DbContextOptions<IDBContext> options) : base(options)
        {

        }
       /* protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=STEVENLAPTOP;Initial Catalog=SegurityBlazorTest;User ID=TestingUserDb;Password=TestingUserDb1");
        }*/
       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           
        }


    }
}
