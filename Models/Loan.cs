using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace perpuss.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Judul buku wajib diisi")]
        public int? BookId { get; set; }

        [Required(ErrorMessage = "Nama anggota wajib diisi")]
        public int? MemberId { get; set; }

        [Required(ErrorMessage = "Tanggal peminjaman wajib diisi")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        [Required(ErrorMessage = "Tanggal pengembalian wajib diisi")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ActualReturnDate { get; set; } // tanggal dikembalikan beneran

        public bool IsReturned { get; set; } = false;

        public decimal? Fine { get; set; } // denda

        // Navigation
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
