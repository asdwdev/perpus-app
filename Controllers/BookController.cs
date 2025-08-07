using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;

public class BookController : Controller
{
    private readonly LibraryContext _context;

    public BookController(LibraryContext context)
    {
        _context = context;
    }

    // Tampilkan semua buku
    public async Task<IActionResult> Index()
    {
        var data = await _context.Books.ToListAsync();
        return View(data);
    }

    // Tampilkan form tambah
    public IActionResult Create()
    {
        return View();
    }

    // Simpan data buku baru
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

   // Tampilkan form edit
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return View(book);
    }

    // Simpan perubahan
    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // Proses hapus
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound(); 
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}
