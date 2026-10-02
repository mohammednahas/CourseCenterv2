using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenterv2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSSN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Students",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Students");
        }
    }
}
