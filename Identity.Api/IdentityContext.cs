using Identity.Api;
using Identity.Api.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace identity.service.model
{
    public class IdentityContext : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; } = null!;

        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfigurator.GetConnectionString());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione User - GIÀ CORRETTA
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id).HasName("pk_users");

                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.FirstName).HasColumnName("first_name");
                entity.Property(u => u.SecondName).HasColumnName("second_name");
                entity.Property(u => u.Password).HasColumnName("password");
                entity.Property(u => u.Email).HasColumnName("email");
            });

            // Configurazione Request - MODIFICATA PER CONSISTENZA
            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests");
                entity.HasKey(r => r.Id).HasName("pk_requests"); // Consistent naming

                entity.Property(r => r.Id).HasColumnName("id");
                entity.Property(r => r.Text)
                    .HasColumnName("text")
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(r => r.CreatedAt)
                    .HasColumnName("creation_time")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(r => r.ExecutedAt)
                    .HasColumnName("execution_time")
                    .HasColumnType("timestamp without time zone")
                    .IsRequired(false);

                entity.Property(r => r.Success)
                    .HasColumnName("success_status")
                    .IsRequired(false);

                entity.Property(r => r.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.HasIndex(r => r.Id)
                    .HasDatabaseName("ix_requests_id"); // Naming convention consistente

                entity.HasOne<User>()
                    .WithMany(u => u.Requests)
                    .HasForeignKey(r => r.UserId)
                    .HasConstraintName("fk_requests_users") // Consistent FK naming
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }



    }
}
