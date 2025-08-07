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

    public IActionResult Edit(int id)
    {
        return View();
    }

    public IActionResult Delete(int id)
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}
