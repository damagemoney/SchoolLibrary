using SchoolLibraryApp.Models;

namespace SchoolLibraryApp.Repositories;

public interface ILoanRepository
{
    Task<List<Loan>> GetActiveAsync();
    Task CreateAsync(int bookId, int readerId, DateTime returnUntil);
    Task ReturnAsync(int loanId);
}
