using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpuss.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyProgramStudyAndEnrollmentYearToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentYear",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramStudy",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentYear",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ProgramStudy",
                table: "Members");
        }
    }
}
