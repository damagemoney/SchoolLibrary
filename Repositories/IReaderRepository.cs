using SchoolLibraryApp.Models;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Repositories;

public interface IReaderRepository
{
    Task<List<Reader>> GetAllAsync();
    Task AddAsync(ReaderFormModel model);
}
