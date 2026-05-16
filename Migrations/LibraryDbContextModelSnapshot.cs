using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolLibraryApp.Data;

#nullable disable

namespace SchoolLibraryApp.Migrations;

[DbContext(typeof(LibraryDbContext))]
partial class LibraryDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "8.0.4");
        modelBuilder.Entity("SchoolLibraryApp.Models.Book", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer").HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            b.Property<string>("Author").IsRequired().HasMaxLength(150).HasColumnType("character varying(150)");
            b.Property<int>("AvailableCopies").HasColumnType("integer");
            b.Property<string>("Genre").IsRequired().HasMaxLength(100).HasColumnType("character varying(100)");
            b.Property<string>("Isbn").IsRequired().HasMaxLength(32).HasColumnType("character varying(32)");
            b.Property<int>("PublishedYear").HasColumnType("integer");
            b.Property<string>("Title").IsRequired().HasMaxLength(200).HasColumnType("character varying(200)");
            b.Property<int>("TotalCopies").HasColumnType("integer");
            b.HasKey("Id");
            b.ToTable("Books");
        });
    }
}
