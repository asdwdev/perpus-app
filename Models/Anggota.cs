using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Anggota
    {
        public int Id { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string NIM { get; set; }

        public string Email { get; set; }
    }
}
