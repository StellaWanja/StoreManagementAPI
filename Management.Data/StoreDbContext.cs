using Management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Data
{
    public class StoreDbContext : IdentityDbContext<AppUser>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {

        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
