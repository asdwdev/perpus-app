using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpuss.Migrations
{
    /// <inheritdoc />
    public partial class AddReturnInfoToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualReturnDate",
                table: "Loans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Fine",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualReturnDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Fine",
                table: "Loans");
        }
    }
}
