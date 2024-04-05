using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Core.Models
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ChatMessageMaxLength, MinimumLength = ChatMessageMinLength)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(ChatMessageSenderMaxLength, MinimumLength = ChatMessageSenderMinLength)]
        public string Sender { get; set; } = string.Empty;

        [Required]
        public DateTime TimeSent { get; set; }

        public string SenderId { get; set; } = string.Empty;
    }
}
