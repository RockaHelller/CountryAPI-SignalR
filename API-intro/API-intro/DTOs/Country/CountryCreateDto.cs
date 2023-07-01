using System.ComponentModel.DataAnnotations;

namespace API_intro.DTOs.Country
{
    public class CountryCreateDto
    {
        [Required]
        public string Name { get; set; }
        public int Population { get; set; }
    }
}
