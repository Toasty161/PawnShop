using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawnShop.Infrastructure.Data.Models;

namespace PawnShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Possession> Possessions { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PossessionBuyer> PossessionBuyers { get; set; }

        public DbSet<ProductBuyer> ProductBuyers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductBuyer>()
                .HasKey(c => new { c.BuyerId, c.ProductId });

            builder.Entity<ProductBuyer>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductBuyer>()
                .HasOne(c => c.Buyer)
                .WithMany()
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PossessionBuyer>()
                .HasKey(c => new { c.BuyerId, c.PossessionId });

            builder.Entity<PossessionBuyer>()
                .HasOne(c => c.Possession)
                .WithMany()
                .HasForeignKey(c => c.PossessionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PossessionBuyer>()
                .HasOne(c => c.Buyer)
                .WithMany()
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
