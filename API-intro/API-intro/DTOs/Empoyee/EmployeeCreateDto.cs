using System.ComponentModel.DataAnnotations;

namespace API_intro.DTOs.Empoyee
{
    public class EmployeeCreateDto
    {
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public IFormFile Photo { get; set; }
    }
}
