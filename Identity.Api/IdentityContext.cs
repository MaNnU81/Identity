using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Api;
using Microsoft.EntityFrameworkCore;

namespace identity.service.model
{
    public class IdentityContext : DbContext
    {


        public DbSet<User> Users { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfigurator.GetConnectionString());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.SecondName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        }

    }
}
