using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(ProductDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(ProductMinWeight, ProductMaxWeight)]
        public double Weight { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
