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

    public IActionResult Create()
    {
        return View();
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
