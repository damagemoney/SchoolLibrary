using SchoolLibraryApp.Models;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAvailableAsync();
    Task AddAsync(BookFormModel model);
}
