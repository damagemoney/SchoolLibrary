namespace SchoolLibraryApp.Models;

/// <summary>
/// Читатель школьной библиотеки.
/// </summary>
public class Reader
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public LibraryCard? Card { get; set; }
    public List<Loan> Loans { get; set; } = new();
}
