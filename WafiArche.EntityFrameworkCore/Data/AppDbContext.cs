using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiArche.Domain.Products;
using WafiArche.Domain.PublicHolidays;

namespace WafiArche.EntityFrameworkCore.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PublicHoliday>()
                .HasIndex(h => h.Date)
                .IsUnique();
            modelBuilder.Entity<PublicHoliday>()
                .Property(h => h.Date)
                .HasColumnType("date");
        }
    }
}
