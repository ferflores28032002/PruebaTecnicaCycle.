using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCycle.Domain;
using PruebaTecnicaCycle.Infrastructure.Data.configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PruebaTecnicaCycle.Infrastructure.Data.context
{
    public class PruebaTecnicaCycleContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=GWTN141-10\\SQLEXPRESS;Database=PruebaTecnicaCycle;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new CategoryConfig());

        }
    }
}
