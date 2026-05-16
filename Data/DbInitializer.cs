using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Models;

namespace SchoolLibraryApp.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(LibraryDbContext db)
    {
        if (await db.Books.AnyAsync())
        {
            return;
        }

        var textbook = new Category { Name = "Учебник" };
        var literature = new Category { Name = "Литература" };
        var science = new Category { Name = "Научно-популярная" };

        var books = new List<Book>
        {
            new()
            {
                Title = "Алгебра. 8 класс",
                Author = "Макарычев Ю. Н.",
                Isbn = "978-5-09-000001-1",
                PublishedYear = 2022,
                Genre = "Математика",
                TotalCopies = 12,
                AvailableCopies = 12,
                Categories = new List<Category> { textbook }
            },
            new()
            {
                Title = "Капитанская дочка",
                Author = "Пушкин А. С.",
                Isbn = "978-5-09-000002-8",
                PublishedYear = 2021,
                Genre = "Художественная литература",
                TotalCopies = 6,
                AvailableCopies = 6,
                Categories = new List<Category> { literature }
            },
            new()
            {
                Title = "Занимательная физика",
                Author = "Перельман Я. И.",
                Isbn = "978-5-09-000003-5",
                PublishedYear = 2020,
                Genre = "Физика",
                TotalCopies = 4,
                AvailableCopies = 4,
                Categories = new List<Category> { science }
            }
        };

        var readers = new List<Reader>
        {
            new()
            {
                FullName = "Иванов Артем Сергеевич",
                ClassName = "7А",
                Email = "ivanov7a@example.local",
                Card = new LibraryCard { Number = "LIB-0001", IssuedAt = DateTime.UtcNow.Date }
            },
            new()
            {
                FullName = "Петрова Мария Андреевна",
                ClassName = "8Б",
                Email = "petrova8b@example.local",
                Card = new LibraryCard { Number = "LIB-0002", IssuedAt = DateTime.UtcNow.Date }
            }
        };

        db.Books.AddRange(books);
        db.Readers.AddRange(readers);
        await db.SaveChangesAsync();
    }
}
