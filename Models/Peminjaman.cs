using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace perpuss.Models
{
    public class Peminjaman
    {
        public int Id { get; set; }

        [Required]
        public int BukuId { get; set; }

        [Required]
        public int AnggotaId { get; set; }

        public DateTime TanggalPinjam { get; set; }

        public DateTime TanggalKembali { get; set; }

        public bool SudahKembali { get; set; } = false;

        // Navigation
        public Buku Buku { get; set; }
        public Anggota Anggota { get; set; }
    }
}
