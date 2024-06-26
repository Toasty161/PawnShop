﻿using Microsoft.AspNetCore.Identity;
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
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ContactMessageMaxLength)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(ContactMessageSenderMaxLength)]
        public string Sender { get; set; } = string.Empty;

        [Required]
        public DateTime TimeSent { get; set; }

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [ForeignKey(nameof(SenderId))]
        public IdentityUser UserSender { get; set; } = null!;
    }
}
