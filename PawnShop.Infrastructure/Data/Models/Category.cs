using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Infrastructure.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;
    }
}
