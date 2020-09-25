using Microsoft.EntityFrameworkCore;
using SalesOne.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneSales.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base (options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                        .HasIndex(c => c.Name)
                        .IsUnique();

            modelBuilder.Entity<City>()
                        .HasIndex(c => c.Name)
                        .IsUnique();

            modelBuilder.Entity<Department>()
                        .HasIndex(d => d.Name)
                        .IsUnique();
        }
    }
}
