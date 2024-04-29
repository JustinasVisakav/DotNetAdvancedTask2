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
            modelBuilder.Entity<CategoryModel>().HasOne(x=>x.Child).WithOne(x=>x.Parent);
            modelBuilder.Entity<CategoryModel>().HasKey(x => x.Id);

            modelBuilder.Entity<ItemModel>().HasOne(x => x.Category).WithMany(x => x.Items).HasPrincipalKey(x => x.Id).HasForeignKey(x=>x.CategoryId);
            modelBuilder.Entity<ItemModel>().HasKey(x => x.Id);
        }
    }
}
