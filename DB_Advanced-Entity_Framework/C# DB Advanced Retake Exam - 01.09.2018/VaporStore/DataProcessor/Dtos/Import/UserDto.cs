namespace VaporStore.DataProcessor.Dtos.Import
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using VaporStore.Data.Models;

    public class UserDto
    {
        public UserDto()
        {
            this.Cards = new List<CardDto>();
        }
        
        [Required]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(3, 103)]
        public int Age { get; set; }

        [Required]
        public ICollection<CardDto> Cards { get; set; }
    }
}
