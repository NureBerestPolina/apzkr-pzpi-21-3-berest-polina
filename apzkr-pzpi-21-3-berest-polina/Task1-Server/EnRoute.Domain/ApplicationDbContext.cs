using EnRoute.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EnRoute.Domain
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<IssuedToken> IssuedTokens { get; set; }
        public DbSet<PickupCounter> PickupCounters { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<CounterInstallationRequest> CounterInstallationRequests { get; set; }
        public DbSet<CounterDeinstallationRequest> CounterDeinstallationRequests { get; set; }
        public DbSet<TechInspectionRequest> TechInspectionRequests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(c => c.Email).IsUnique();
            builder.Entity<Order>().HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>().HasOne(o => o.Order).WithMany(o => o.Items).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Cell>().HasOne(c => c.Counter).WithMany(p => p.Cells).HasForeignKey(c => c.CounterId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
