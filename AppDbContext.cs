using Microsoft.EntityFrameworkCore;
using StroySite.WPF.Models;
using System.Diagnostics.Contracts;

namespace StroySite.WPF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contractors> Contractors { get; set; }
        public DbSet<ContractorsType> ContractorsTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=localhost;Database=StroySiteDB;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseLazyLoadingProxies(); // Для автоматической загрузки связанных данных
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи между таблицами
            modelBuilder.Entity<Contractors>()
                .HasOne(c => c.ContractorsType)
                .WithMany(t => t.Contractors)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict); // Запрет каскадного удаления

            base.OnModelCreating(modelBuilder);
        }
    }
}