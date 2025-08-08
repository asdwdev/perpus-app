using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perpuss.Data;
using perpuss.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

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


    // buat export pdf
    // [HttpGet]
    // public async Task<IActionResult> ExportToPdf()
    // {
    //     // Set license type (required)
    //     QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

    //     var loans = await _context.Loans
    //         .Include(l => l.Book)
    //         .Include(l => l.Member)
    //         .ToListAsync();

    //     var document = Document.Create(container =>
    //     {
    //         container.Page(page =>
    //         {
    //             page.Margin(30);
    //             page.Size(PageSizes.A4);

    //             page.Header()
    //                 .Text("Laporan Transaksi Pinjaman Buku")
    //                 .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

    //             page.Content()
    //                 .Table(table =>
    //                 {
    //                     table.ColumnsDefinition(columns =>
    //                     {
    //                         columns.ConstantColumn(30); // No
    //                         columns.RelativeColumn();
    //                         columns.RelativeColumn();
    //                         columns.RelativeColumn();
    //                         columns.RelativeColumn();
    //                         columns.RelativeColumn();
    //                         columns.RelativeColumn();
    //                     });

    //                     table.Header(header =>
    //                     {
    //                         header.Cell().Text("#").Bold();
    //                         header.Cell().Text("Nama").Bold();
    //                         header.Cell().Text("Buku").Bold();
    //                         header.Cell().Text("Tgl Pinjam").Bold();
    //                         header.Cell().Text("Tgl Kembali").Bold();
    //                         header.Cell().Text("Status").Bold();
    //                         header.Cell().Text("Denda").Bold();
    //                     });

    //                     int no = 1;
    //                     foreach (var b in loans)
    //                     {
    //                         table.Cell().Text(no++.ToString());
    //                         table.Cell().Text(b.Member?.Name ?? "-");
    //                         table.Cell().Text(b.Book?.Title ?? "-");
    //                         table.Cell().Text(b.LoanDate.ToString("d/M/yyyy"));
    //                         table.Cell().Text(b.ReturnDate.ToString("d/M/yyyy"));
    //                         table.Cell().Text(b.IsReturned ? "Sudah" : "Belum");
    //                         table.Cell().Text($"Rp {(b.Fine.HasValue ? b.Fine.Value.ToString("N0", new System.Globalization.CultureInfo("id-ID")) : "0")}");
    //                     }
    //                 });

    //             page.Footer()
    //                 .AlignCenter()
    //                 .Text(x =>
    //                 {
    //                     x.Span("Generated on ");
    //                     x.Span(DateTime.Now.ToString("dd MMM yyyy HH:mm")).SemiBold();
    //                 });
    //         });
    //     });

    //     var pdfBytes = document.GeneratePdf();
    //     return File(pdfBytes, "application/pdf", "Laporan_Transaksi.pdf");
    // }

    [HttpGet]
public async Task<IActionResult> ExportToPdf()
{
    QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

    var loans = await _context.Loans
        .Include(l => l.Book)
        .Include(l => l.Member)
        .ToListAsync();

    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Margin(40); // Tailwind-like padding (p-10)
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

            page.Header()
                .AlignCenter()
                .Text("📚 Laporan Transaksi Pinjaman Buku")
                .FontSize(20).Bold().FontColor(Colors.Blue.Medium); // Tailwind's "text-blue-500" and "text-xl font-bold"

            page.Content()
                .PaddingTop(20) // pt-5
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);  // #
                        columns.RelativeColumn();    // Nama
                        columns.RelativeColumn();    // Buku
                        columns.RelativeColumn();    // Tgl Pinjam
                        columns.RelativeColumn();    // Tgl Kembali
                        columns.RelativeColumn();    // Status
                        columns.RelativeColumn();    // Denda
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("#").Bold();
                        header.Cell().Element(CellStyle).Text("Nama").Bold();
                        header.Cell().Element(CellStyle).Text("Buku").Bold();
                        header.Cell().Element(CellStyle).Text("Tgl Pinjam").Bold();
                        header.Cell().Element(CellStyle).Text("Tgl Kembali").Bold();
                        header.Cell().Element(CellStyle).Text("Status").Bold();
                        header.Cell().Element(CellStyle).Text("Denda").Bold();
                    });

                    // Content rows
                    int no = 1;
                    foreach (var b in loans)
                    {
                        table.Cell().Element(CellStyle).Text(no++.ToString());
                        table.Cell().Element(CellStyle).Text(b.Member?.Name ?? "-");
                        table.Cell().Element(CellStyle).Text(b.Book?.Title ?? "-");
                        table.Cell().Element(CellStyle).Text(b.LoanDate.ToString("dd/MM/yyyy"));
                        table.Cell().Element(CellStyle).Text(b.ReturnDate.ToString("dd/MM/yyyy"));
                        table.Cell().Element(CellStyle).Text(b.IsReturned ? "✅ Sudah" : "⏳ Belum");
                        table.Cell().Element(CellStyle).Text($"Rp {(b.Fine.HasValue ? b.Fine.Value.ToString("N0", new System.Globalization.CultureInfo("id-ID")) : "0")}");
                    }

                    // Local function for cell style to mimic Tailwind padding and border
                    IContainer CellStyle(IContainer container) =>
                        container
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten2)
                            .PaddingVertical(4) // Tailwind's "py-1"
                            .PaddingHorizontal(6) // Tailwind's "px-2"
                            .AlignMiddle();
                });

            page.Footer()
                .AlignCenter()
                .PaddingTop(20)
                .Text(x =>
                {
                    x.Span("📅 Generated on ");
                    x.Span(DateTime.Now.ToString("dd MMM yyyy HH:mm")).SemiBold().FontColor(Colors.Grey.Darken2);
                });
        });
    });

    var pdfBytes = document.GeneratePdf();
    return File(pdfBytes, "application/pdf", "Laporan_Transaksi.pdf");
}

    

}