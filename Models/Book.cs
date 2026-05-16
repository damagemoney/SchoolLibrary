namespace SchoolLibraryApp.Models;

/// <summary>
/// Книга школьной библиотеки.
/// </summary>
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public List<Category> Categories { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();
}
