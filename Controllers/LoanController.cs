using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;

namespace perpuss.Controllers
{
    public class LoanController : Controller
    {
        private readonly LibraryContext _context;

        public LoanController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
            ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
            ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");

            return View();
        }

        // POST: Loan
        [HttpPost]

        public IActionResult Index(Loan loan)
        {
            // Validasi tanggal
            if (loan.ReturnDate < loan.LoanDate)
            {
                ModelState.AddModelError("ReturnDate", "Tanggal pengembalian tidak boleh kurang dari tanggal peminjaman.");
                ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
                ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");
                return View(loan);
            }

            // Validasi apakah member masih punya pinjaman yang belum dikembalikan
            bool hasUnreturnedLoan = _context.Loans
                .Any(l => l.MemberId == loan.MemberId && l.IsReturned == false);

            if (hasUnreturnedLoan)
            {
                ModelState.AddModelError("MemberId", "Member ini masih memiliki pinjaman yang belum dikembalikan.");
                ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
                ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");
                return View(loan);
            }

            // Ambil data buku yang dipinjam
            var book = _context.Books.FirstOrDefault(b => b.Id == loan.BookId);

            if (book == null)
            {
                ModelState.AddModelError("BookId", "Buku tidak ditemukan.");
                ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
                ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");
                return View(loan);
            }

            // Cek apakah stok tersedia
            if (book.StockQuantity <= 0)
            {
                ModelState.AddModelError("BookId", "Stok buku ini sedang kosong.");
                ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
                ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");
                return View(loan);
            }

            // Kurangi stok buku
            book.StockQuantity -= 1;

            // Simpan data pinjaman
            _context.Loans.Add(loan);
            _context.SaveChanges();

            // Kirim notifikasi sukses
            TempData["Success"] = "Peminjaman berhasil ditambahkan!";
            TempData["Source"] = "Loan";
            
            return RedirectToAction("Index");
        }
    }
}
