using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StudentId { get; set; }

        public string Email { get; set; }
    }
}
