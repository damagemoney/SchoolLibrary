using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Data;
using SchoolLibraryApp.Models;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Repositories;

public class ReaderRepository(IDbContextFactory<LibraryDbContext> dbFactory) : IReaderRepository
{
    public async Task<List<Reader>> GetAllAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Readers
            .Include(x => x.Card)
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .ToListAsync();
    }

    public async Task AddAsync(ReaderFormModel model)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var nextNumber = await db.Readers.CountAsync() + 1;
        var reader = new Reader
        {
            FullName = model.FullName.Trim(),
            ClassName = model.ClassName.Trim(),
            Email = model.Email.Trim(),
            Card = new LibraryCard
            {
                Number = $"LIB-{nextNumber:0000}",
                IssuedAt = DateTime.UtcNow.Date
            }
        };
        db.Readers.Add(reader);
        await db.SaveChangesAsync();
    }
}
