using SchoolLibraryApp.Models;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Services;

public interface ILibraryService
{
    Task<LibraryStats> GetStatsAsync();
    Task<List<Book>> GetBooksAsync();
    Task<List<Book>> GetAvailableBooksAsync();
    Task<List<Reader>> GetReadersAsync();
    Task<List<Loan>> GetActiveLoansAsync();
    Task<OperationResult> AddBookAsync(BookFormModel model);
    Task<OperationResult> AddReaderAsync(ReaderFormModel model);
    Task<OperationResult> CreateLoanAsync(LoanFormModel model);
    Task<OperationResult> ReturnLoanAsync(int loanId);
}
