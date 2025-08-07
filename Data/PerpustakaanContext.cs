using Microsoft.EntityFrameworkCore;
using perpuss.Models;

namespace perpuss.Data
{
    public class PerpustakaanContext : DbContext
    {
        public PerpustakaanContext(DbContextOptions<PerpustakaanContext> options)
            : base(options) { }

        public DbSet<Buku> Bukus { get; set; }
        public DbSet<Anggota> Anggotas { get; set; }
        public DbSet<Peminjaman> Peminjamans { get; set; }
    }
}
