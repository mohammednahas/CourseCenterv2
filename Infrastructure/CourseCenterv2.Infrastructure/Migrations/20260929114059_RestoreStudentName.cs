using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenterv2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestoreStudentName : Migration
    {
        /// <inheritdoc />
       protected override void Up(MigrationBuilder migrationBuilder)
{
    // 1. Add Name as nullable temporarily
    migrationBuilder.AddColumn<string>(
        name: "Name",
        table: "Students",
        type: "character varying(200)",
        maxLength: 200,
        nullable: true);

    // 2. Migrate existing names into the new column
    migrationBuilder.Sql("""
        UPDATE "Students"
        SET "Name" = TRIM(
            COALESCE("FirstName", '') || ' ' ||
            COALESCE("LastName", '')
        );
        """);

    // 3. Make Name required
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

    // 4. Remove old columns
    migrationBuilder.DropColumn(
        name: "FirstName",
        table: "Students");

    migrationBuilder.DropColumn(
        name: "LastName",
        table: "Students");

    migrationBuilder.DropColumn(
        name: "Mobile",
        table: "Students");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // Restore old columns
    migrationBuilder.AddColumn<string>(
        name: "FirstName",
        table: "Students",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<string>(
        name: "LastName",
        table: "Students",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<string>(
        name: "Mobile",
        table: "Students",
        type: "character varying(20)",
        maxLength: 20,
        nullable: true);

    // Restore first and last names from Name
    migrationBuilder.Sql("""
        UPDATE "Students"
        SET "FirstName" = SPLIT_PART("Name", ' ', 1),
            "LastName" = CASE
                WHEN POSITION(' ' IN "Name") > 0
                THEN SUBSTRING("Name" FROM POSITION(' ' IN "Name") + 1)
                ELSE ''
            END;
        """);

    migrationBuilder.DropColumn(
        name: "Name",
        table: "Students");
}
    }
}
