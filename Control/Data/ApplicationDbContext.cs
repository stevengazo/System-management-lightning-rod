using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;
using System;
using Control.Areas.Identity.Pages.Account.Manage;
using Microsoft.IdentityModel;
namespace Control.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
            IdentityRole Admin = new IdentityRole() {
                Id = "01",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            IdentityRole Editor = new IdentityRole()
            {
                Id = "02",
                Name = "Editor",
                NormalizedName = "EDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            IdentityRole Lector = new IdentityRole
            {
                Id = "03",
                Name = "Lector",
                NormalizedName = "LECTOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            builder.Entity<IdentityRole>().HasData(Admin, Editor, Lector);




        }
    }
}
