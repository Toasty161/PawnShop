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

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

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

            builder.Entity<Category>()
                .HasData(GetCategories());

            base.OnModelCreating(builder);
        }

        private Category[] GetCategories()
        {
            var result = new Category[]
            {
                new Category()
                {
                    Id = 1,
                    Name = "Food"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Drink"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Plant"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Furniture"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Vehicle"
                },
                new Category()
                {
                    Id = 6,
                    Name = "Appliance"
                },
                new Category()
                {
                    Id = 7,
                    Name = "Electronic"
                },
                new Category()
                {
                    Id = 8,
                    Name = "Other"
                },
            };

            return result;
        }
    }
}
