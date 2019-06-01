using Store.DAL.Models;
using Store.Models.DAL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Store.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreDbConnectionString")
        {
            Database.SetInitializer(new StoreInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            var product = modelBuilder.Entity<Product>();

            user.Property(u => u.BirthDate).HasColumnType("smalldatetime");
            product.Property(p => p.Date).HasColumnType("smalldatetime");
            product.Property(p => p.AdddedToCart).HasColumnType("smalldatetime");
            base.OnModelCreating(modelBuilder);
        }


    }
}
