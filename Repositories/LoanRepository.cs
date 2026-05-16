using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Data;
using SchoolLibraryApp.Models;

namespace SchoolLibraryApp.Repositories;

public class LoanRepository(IDbContextFactory<LibraryDbContext> dbFactory) : ILoanRepository
{
    public async Task<List<Loan>> GetActiveAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.Loans
            .Include(x => x.Book)
            .Include(x => x.Reader)
            .Where(x => x.ReturnedAt == null)
            .AsNoTracking()
            .OrderBy(x => x.ReturnUntil)
            .ToListAsync();
    }

    public async Task CreateAsync(int bookId, int readerId, DateTime returnUntil)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var book = await db.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        var reader = await db.Readers.FirstOrDefaultAsync(x => x.Id == readerId);

        if (book is null)
        {
            throw new InvalidOperationException("Книга не найдена.");
        }
        if (reader is null)
        {
            throw new InvalidOperationException("Читатель не найден.");
        }
        if (book.AvailableCopies <= 0)
        {
            throw new InvalidOperationException("Нет доступных экземпляров книги.");
        }

        book.AvailableCopies--;
        db.Loans.Add(new Loan
        {
            BookId = bookId,
            ReaderId = readerId,
            IssuedAt = DateTime.UtcNow.Date,
            ReturnUntil = DateTime.SpecifyKind(returnUntil.Date, DateTimeKind.Utc)
        });
        await db.SaveChangesAsync();
    }

    public async Task ReturnAsync(int loanId)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var loan = await db.Loans.Include(x => x.Book).FirstOrDefaultAsync(x => x.Id == loanId);
        if (loan is null)
        {
            throw new InvalidOperationException("Выдача не найдена.");
        }
        if (loan.ReturnedAt is not null)
        {
            return;
        }
        loan.ReturnedAt = DateTime.UtcNow.Date;
        loan.Book.AvailableCopies++;
        await db.SaveChangesAsync();
    }
}
