using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenterv2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStudentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DropColumn(
        name: "Name",
        table: "Students");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.AddColumn<string>(
        name: "Name",
        table: "Students",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: "");

        }
    }
}
