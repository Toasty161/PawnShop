using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Infrastructure.Data.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ChatMessageMaxLength)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(ChatMessageSenderMaxLength)]
        public string Sender { get; set; } = string.Empty;
    }
}
