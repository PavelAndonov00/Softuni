namespace VaporStore.DataProcessor.Dtos.Import
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using VaporStore.Data.Models;

    public class GameDto
    {
        public GameDto()
        {
            this.Tags = new List<string>();
        }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("Genre")]
        [Required(AllowEmptyStrings = false)]
        public string GenreName { get; set; }

        [JsonProperty("Developer")]
        [Required(AllowEmptyStrings = false)]
        public string DeveloperName { get; set; }

        [MinLength(1)]
        public ICollection<string> Tags { get; set; }
    }
}
