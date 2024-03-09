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
    public class Possession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(PossessionNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(PossessionMinArea, PossessionMaxArea)]
        public double Area { get; set; }

        [Required]
        [StringLength(PossessionLocationMaxLength)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}
