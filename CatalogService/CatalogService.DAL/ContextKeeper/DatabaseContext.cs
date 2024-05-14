using CatalogService.DAL.Models;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.DAL.ContextKeeper
{
    public class DatabaseContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ItemModel> Items { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>().HasKey(x => x.Id);
            modelBuilder.Entity<CategoryModel>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<ItemModel>().HasKey(x => x.Id);
            modelBuilder.Entity<ItemModel>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<ItemModel>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<ItemModel>().Property(x => x.Amount).IsRequired();
        }
    }
}
