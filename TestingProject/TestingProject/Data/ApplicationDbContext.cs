using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestingProject.Models;

namespace TestingProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ComCustomer> comCustomers { get; set; }
        public DbSet<SoOrder> SoOrders { get; set; }
        public DbSet<SoItem> SoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SoOrder>()
                .HasMany(o => o.SoItems)
                .WithOne(i => i.SoOrder)
                .HasForeignKey(i => i.SoOrderId)
                .OnDelete(DeleteBehavior.Cascade);
                

        }


    }
}