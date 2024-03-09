using Microsoft.AspNetCore.Identity;
using PawnShop.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Core.Models
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(ProductMinWeight, ProductMaxWeight)]
        public double Weight { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
    }
}
