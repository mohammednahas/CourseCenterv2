using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenterv2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitStudentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add FirstName and LastName temporarily as nullable
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            // 2. Split Name into FirstName and LastName
            migrationBuilder.Sql("""
        UPDATE "Students"
        SET
            "FirstName" = CASE
                WHEN POSITION(' ' IN TRIM("Name")) > 0
                THEN LEFT(TRIM("Name"), POSITION(' ' IN TRIM("Name")) - 1)
                ELSE TRIM("Name")
            END,

            "LastName" = CASE
                WHEN POSITION(' ' IN TRIM("Name")) > 0
                THEN SUBSTRING(
                    TRIM("Name")
                    FROM POSITION(' ' IN TRIM("Name")) + 1
                )
                ELSE ''
            END;
        """);

            // 3. Make FirstName and LastName required
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            // 4. Delete the old Name column
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.Sql("""
        UPDATE "Students"
        SET "Name" = TRIM(
            COALESCE("FirstName", '') || ' ' ||
            COALESCE("LastName", '')
        );
        """);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");
        }
    }
}
