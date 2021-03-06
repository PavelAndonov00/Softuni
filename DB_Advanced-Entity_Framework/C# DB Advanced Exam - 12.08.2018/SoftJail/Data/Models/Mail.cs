﻿namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s]+str\.$")]
        public string Address { get; set; }

        [ForeignKey("Prisoner")]
        public int? PrisonerId { get; set; }

        [Required]
        public Prisoner Prisoner { get; set; }
    }
}
