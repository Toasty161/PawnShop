using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Data.Models
{
    public class PossessionBuyer
    {
        [Required]
        public string BuyerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(BuyerId))]
        public IdentityUser Buyer { get; set; } = null!;

        [Required]
        public int PossessionId { get; set; }

        [Required]
        [ForeignKey(nameof(PossessionId))]
        public Possession Possession { get; set; } = null!;
    }
}
