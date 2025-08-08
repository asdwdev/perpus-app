using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;
using System;
using System.Linq;

public class ReturnController : Controller
{
    private readonly LibraryContext _context;

    public ReturnController(LibraryContext context)
    {
        _context = context;
    }

    // GET: /Return
    public IActionResult Index()
    {
        var activeMembers = _context.Loans
            .Include(l => l.Member)
            .Where(l => !l.IsReturned)
            .Select(l => l.Member)
            .Distinct()
            .ToList();

        ViewBag.Members = activeMembers
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            })
            .ToList();

        return View();
    }

    // AJAX: GET /Return/GetLoanData?memberId=1
    [HttpGet]
    public IActionResult GetLoanData(int memberId)
    {
        var loan = _context.Loans
            .Include(l => l.Book)
            .Where(l => l.MemberId == memberId && !l.IsReturned)
            .FirstOrDefault();

        if (loan == null)
        {
            return NotFound();
        }

        return Json(new
        {
            bookTitle = loan.Book.Title,
            loanDate = loan.LoanDate.ToString("yyyy-MM-dd")
        });
    }

    // POST: /Return
    // [HttpPost]
    // public IActionResult Index(int memberId, DateTime tanggalPinjam, DateTime tanggalDikembalikan)
    // {
    //     var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
    //     if (member == null)
    //     {
    //         ModelState.AddModelError("", "Mahasiswa tidak ditemukan.");
    //         return RedirectToAction("Index");
    //     }

    //     var loan = _context.Loans
    //         .Include(l => l.Book)
    //         .FirstOrDefault(l =>
    //             l.MemberId == member.Id &&
    //             l.LoanDate.Date == tanggalPinjam.Date &&
    //             !l.IsReturned);

    //     if (loan == null)
    //     {
    //         ModelState.AddModelError("", "Data peminjaman tidak ditemukan.");
    //         return RedirectToAction("Index");
    //     }

    //     loan.IsReturned = true;
    //     loan.ActualReturnDate = tanggalDikembalikan;
    //     loan.Fine = HitungDenda(loan.ReturnDate, tanggalDikembalikan);

    //     // Tambahkan stok buku kembali
    //     var book = _context.Books.FirstOrDefault(b => b.Id == loan.BookId);
    //     if (book != null)
    //     {
    //         book.StockQuantity += 1;
    //     }

    //     _context.SaveChanges();

    //     TempData["Success"] = "Buku berhasil dikembalikan.";
    //     TempData["Source"] = "Return";
    //     TempData["Denda"] = ((double)loan.Fine).ToString("N0");

    //     return RedirectToAction("Index");
    // }
    [HttpPost]
    public IActionResult Index(int memberId, DateTime tanggalPinjam, DateTime tanggalDikembalikan)
    {
        if (tanggalDikembalikan.Date < tanggalPinjam.Date)
        {
            ModelState.AddModelError("", "Tanggal pengembalian tidak boleh lebih awal dari tanggal peminjaman.");
            return RedirectToAction("Index");
        }

        var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null)
        {
            ModelState.AddModelError("", "Mahasiswa tidak ditemukan.");
            return RedirectToAction("Index");
        }

        var loan = _context.Loans
            .Include(l => l.Book)
            .FirstOrDefault(l =>
                l.MemberId == member.Id &&
                l.LoanDate.Date == tanggalPinjam.Date &&
                !l.IsReturned);

        if (loan == null)
        {
            ModelState.AddModelError("", "Data peminjaman tidak ditemukan.");
            return RedirectToAction("Index");
        }

        loan.IsReturned = true;
        loan.ActualReturnDate = tanggalDikembalikan;
        loan.Fine = HitungDenda(loan.ReturnDate, tanggalDikembalikan);

        // Tambahkan stok buku kembali
        var book = _context.Books.FirstOrDefault(b => b.Id == loan.BookId);
        if (book != null)
        {
            book.StockQuantity += 1;
        }

        _context.SaveChanges();

        TempData["Success"] = "Buku berhasil dikembalikan.";
        TempData["Source"] = "Return";
        TempData["Denda"] = ((double)loan.Fine).ToString("N0");

        return RedirectToAction("Index");
    }


    private decimal HitungDenda(DateTime tanggalJatuhTempo, DateTime tanggalDikembalikan)
    {
        var selisihHari = (tanggalDikembalikan - tanggalJatuhTempo).Days;
        return selisihHari > 0 ? selisihHari * 1000 : 0;
    }
}
