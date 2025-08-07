using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Buku
    {
        public int Id { get; set; }

        [Required]
        public string Judul { get; set; }

        [Required]
        public string Penulis { get; set; }

        public int TahunTerbit { get; set; }

        public string ISBN { get; set; }

        public int JumlahStok { get; set; }
    }
}
