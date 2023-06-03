using System.ComponentModel.DataAnnotations;

namespace API_Affiliates.Models
{
    public class Affiliate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DNI { get; set; }
        public string LU { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
