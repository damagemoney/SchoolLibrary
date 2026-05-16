namespace SchoolLibraryApp.Models;

/// <summary>
/// Читательский билет, связанный с одним читателем.
/// </summary>
public class LibraryCard
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = null!;
}
