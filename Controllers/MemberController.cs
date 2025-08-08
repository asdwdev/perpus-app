using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;

public class MemberController : Controller
{
    private readonly LibraryContext _context;

    public MemberController(LibraryContext context)
    {
        _context = context;
    }

    // Tampilkan semua member
    public async Task<IActionResult> Index()
    {
        var data = await _context.Members.ToListAsync();
        return View(data);
    }

    // Tampilkan form tambah member
    public IActionResult Create()
    {
        return View();
    }

    // Simpan data member baru
    [HttpPost]
    public async Task<IActionResult> Create(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    // Tampilkan form edit member
    public async Task<IActionResult> Edit(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    // Simpan perubahan data member
    [HttpPost]
    public async Task<IActionResult> Edit(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Update(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    // Tampilkan detail member
    public async Task<IActionResult> Details(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    // Proses hapus member
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var member = await _context.Members.FindAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
