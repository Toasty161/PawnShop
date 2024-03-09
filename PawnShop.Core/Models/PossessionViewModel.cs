using Microsoft.AspNetCore.Identity;
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
    public class PossessionViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(PossessionNameMaxLength, MinimumLength = PossessionNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(PossessionMinArea, PossessionMaxArea)]
        public double Area { get; set; }

        [Required]
        [StringLength(PossessionLocationMaxLength, MinimumLength = PossessionLocationMinLength)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string OwnerId { get; set; } = string.Empty;
    }
}
