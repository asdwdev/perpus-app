using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Judul buku wajib diisi")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nama penulis wajib diisi")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Tahun terbit wajib diisi")]
        public int? PublicationYear { get; set; }

        [Required(ErrorMessage = "ISBN wajib diisi")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Jumlah stok wajib diisi")]
        public int? StockQuantity { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
