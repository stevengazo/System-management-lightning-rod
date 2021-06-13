using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control.Data
{
    public class IDBContext : IdentityDbContext
    {
        public IDBContext(DbContextOptions<IDBContext> options)
            : base(options)
        {
        }
    }
}
