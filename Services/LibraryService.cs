using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Data;
using SchoolLibraryApp.Models;
using SchoolLibraryApp.Repositories;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Services;

public class LibraryService(
    IDbContextFactory<LibraryDbContext> dbFactory,
    IBookRepository bookRepository,
    IReaderRepository readerRepository,
    ILoanRepository loanRepository,
    IValidator<BookFormModel> bookValidator,
    IValidator<ReaderFormModel> readerValidator,
    IValidator<LoanFormModel> loanValidator) : ILibraryService
{
    public async Task<LibraryStats> GetStatsAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return new LibraryStats
        {
            TotalBooks = await db.Books.CountAsync(),
            AvailableCopies = await db.Books.SumAsync(x => x.AvailableCopies),
            TotalReaders = await db.Readers.CountAsync(),
            ActiveLoans = await db.Loans.CountAsync(x => x.ReturnedAt == null)
        };
    }

    public Task<List<Book>> GetBooksAsync() => bookRepository.GetAllAsync();

    public Task<List<Book>> GetAvailableBooksAsync() => bookRepository.GetAvailableAsync();

    public Task<List<Reader>> GetReadersAsync() => readerRepository.GetAllAsync();

    public Task<List<Loan>> GetActiveLoansAsync() => loanRepository.GetActiveAsync();

    public async Task<OperationResult> AddBookAsync(BookFormModel model)
    {
        var validation = await bookValidator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            return OperationResult.Fail(validation.Errors.Select(x => x.ErrorMessage));
        }
        await bookRepository.AddAsync(model);
        return OperationResult.Ok();
    }

    public async Task<OperationResult> AddReaderAsync(ReaderFormModel model)
    {
        var validation = await readerValidator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            return OperationResult.Fail(validation.Errors.Select(x => x.ErrorMessage));
        }
        await readerRepository.AddAsync(model);
        return OperationResult.Ok();
    }

    public async Task<OperationResult> CreateLoanAsync(LoanFormModel model)
    {
        var validation = await loanValidator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            return OperationResult.Fail(validation.Errors.Select(x => x.ErrorMessage));
        }

        if (!int.TryParse(model.BookIdText, out var bookId))
        {
            return OperationResult.Fail("Выберите книгу.");
        }
        if (!int.TryParse(model.ReaderIdText, out var readerId))
        {
            return OperationResult.Fail("Выберите читателя.");
        }
        if (!DateTime.TryParse(model.ReturnUntilText, out var returnUntil))
        {
            return OperationResult.Fail("Введите дату возврата в формате ГГГГ-ММ-ДД.");
        }

        try
        {
            await loanRepository.CreateAsync(bookId, readerId, returnUntil);
            return OperationResult.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }

    public async Task<OperationResult> ReturnLoanAsync(int loanId)
    {
        try
        {
            await loanRepository.ReturnAsync(loanId);
            return OperationResult.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return OperationResult.Fail(ex.Message);
        }
    }
}
