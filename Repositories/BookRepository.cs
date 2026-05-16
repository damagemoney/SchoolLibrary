using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Data;
using SchoolLibraryApp.Models;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Repositories;

public class BookRepository(IDbContextFactory<LibraryDbContext> dbFactory) : IBookRepository
{
    public async Task<List<Book>> GetAllAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Books
            .Include(x => x.Categories)
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .ToListAsync();
    }

    public async Task<List<Book>> GetAvailableAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Books
            .Where(x => x.AvailableCopies > 0)
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .ToListAsync();
    }

    public async Task AddAsync(BookFormModel model)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var publishedYear = int.Parse(model.PublishedYearText);
        var totalCopies = int.Parse(model.TotalCopiesText);

        var categoryNames = model.CategoriesText
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var categories = new List<Category>();
        foreach (var name in categoryNames)
        {
            var category = await db.Categories.FirstOrDefaultAsync(x => x.Name == name);
            if (category is null)
            {
                category = new Category { Name = name };
            }
            categories.Add(category);
        }

        var book = new Book
        {
            Title = model.Title.Trim(),
            Author = model.Author.Trim(),
            Isbn = model.Isbn.Trim(),
            PublishedYear = publishedYear,
            Genre = model.Genre.Trim(),
            TotalCopies = totalCopies,
            AvailableCopies = totalCopies,
            Categories = categories
        };

        db.Books.Add(book);
        await db.SaveChangesAsync();
    }
}
