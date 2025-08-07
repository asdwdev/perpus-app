using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpuss.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anggotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anggotas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bukus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Penulis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TahunTerbit = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JumlahStok = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bukus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peminjamans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BukuId = table.Column<int>(type: "int", nullable: false),
                    AnggotaId = table.Column<int>(type: "int", nullable: false),
                    TanggalPinjam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TanggalKembali = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SudahKembali = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjamans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Anggotas_AnggotaId",
                        column: x => x.AnggotaId,
                        principalTable: "Anggotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Bukus_BukuId",
                        column: x => x.BukuId,
                        principalTable: "Bukus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_AnggotaId",
                table: "Peminjamans",
                column: "AnggotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_BukuId",
                table: "Peminjamans",
                column: "BukuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjamans");

            migrationBuilder.DropTable(
                name: "Anggotas");

            migrationBuilder.DropTable(
                name: "Bukus");
        }
    }
}
