namespace SchoolLibraryApp.Models;

/// <summary>
/// Категория книги.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = new();
}
