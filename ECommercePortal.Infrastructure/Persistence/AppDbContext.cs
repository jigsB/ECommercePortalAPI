using ECommercePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommercePortal.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<ProductDetail> Products => Set<ProductDetail>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<OrderItem>()
                .Property(x => x.TotalPrice)
                .HasComputedColumnSql("[Quantity] * [UnitPrice]");
        }
    }
}
