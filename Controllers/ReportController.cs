using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;
public class ReportController : Controller
{
    private readonly LibraryContext _context;

    public ReportController(LibraryContext context)
    {
        _context = context;
    }

    // Tampilkan semua data pinjaman
    public async Task<IActionResult> Index()
{
    ViewBag.BookId = new SelectList(_context.Books, "Id", "Title");
    ViewBag.MemberId = new SelectList(_context.Members, "Id", "Name");

    var loans = await _context.Loans
        .Include(l => l.Book)
        .Include(l => l.Member)
        .ToListAsync();

    return View(loans);
}

}