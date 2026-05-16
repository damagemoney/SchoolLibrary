using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolLibraryApp.Data;

#nullable disable

namespace SchoolLibraryApp.Migrations;

[DbContext(typeof(LibraryDbContext))]
[Migration("20260516000100_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Author = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Isbn = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                PublishedYear = table.Column<int>(type: "integer", nullable: false),
                Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                TotalCopies = table.Column<int>(type: "integer", nullable: false),
                AvailableCopies = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Readers",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                ClassName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Readers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "BookCategories",
            columns: table => new
            {
                BookId = table.Column<int>(type: "integer", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookCategories", x => new { x.BookId, x.CategoryId });
                table.ForeignKey(
                    name: "FK_BookCategories_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BookCategories_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LibraryCards",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ReaderId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LibraryCards", x => x.Id);
                table.ForeignKey(
                    name: "FK_LibraryCards_Readers_ReaderId",
                    column: x => x.ReaderId,
                    principalTable: "Readers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Loans",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                BookId = table.Column<int>(type: "integer", nullable: false),
                ReaderId = table.Column<int>(type: "integer", nullable: false),
                IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ReturnUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ReturnedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Loans", x => x.Id);
                table.ForeignKey(
                    name: "FK_Loans_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Loans_Readers_ReaderId",
                    column: x => x.ReaderId,
                    principalTable: "Readers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_BookCategories_CategoryId", table: "BookCategories", column: "CategoryId");
        migrationBuilder.CreateIndex(name: "IX_Categories_Name", table: "Categories", column: "Name", unique: true);
        migrationBuilder.CreateIndex(name: "IX_LibraryCards_Number", table: "LibraryCards", column: "Number", unique: true);
        migrationBuilder.CreateIndex(name: "IX_LibraryCards_ReaderId", table: "LibraryCards", column: "ReaderId", unique: true);
        migrationBuilder.CreateIndex(name: "IX_Loans_BookId", table: "Loans", column: "BookId");
        migrationBuilder.CreateIndex(name: "IX_Loans_ReaderId", table: "Loans", column: "ReaderId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "BookCategories");
        migrationBuilder.DropTable(name: "LibraryCards");
        migrationBuilder.DropTable(name: "Loans");
        migrationBuilder.DropTable(name: "Categories");
        migrationBuilder.DropTable(name: "Books");
        migrationBuilder.DropTable(name: "Readers");
    }
}
