namespace SchoolLibraryApp.Models;

/// <summary>
/// Запись о выдаче книги.
/// </summary>
public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = null!;
    public DateTime IssuedAt { get; set; }
    public DateTime ReturnUntil { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsReturned => ReturnedAt.HasValue;
}
