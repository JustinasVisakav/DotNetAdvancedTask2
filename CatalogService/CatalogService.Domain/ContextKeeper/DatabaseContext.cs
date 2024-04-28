using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.Domain.ContextKeeper
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
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryModel>().HasOne(x=>x.Child).WithOne(x=>x.Parent);
            modelBuilder.Entity<ItemModel>().HasOne(x => x.Category).WithMany(x => x.Items).HasPrincipalKey(x => x.Id).HasForeignKey(x=>x.CategoryId);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
