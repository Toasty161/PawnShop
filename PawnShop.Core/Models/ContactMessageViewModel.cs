﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawnShop.Infrastructure.Data.Constants.DataConstants;

namespace PawnShop.Core.Models
{
    public class ContactMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ContactMessageMaxLength, MinimumLength = ContactMessageMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(ContactMessageSenderMaxLength, MinimumLength = ContactMessageSenderMinLength)]
        public string Sender { get; set; } = string.Empty;
    }
}
