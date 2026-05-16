namespace SchoolLibraryApp.ViewModels;

public class BookFormModel
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string PublishedYearText { get; set; } = DateTime.Today.Year.ToString();
    public string Genre { get; set; } = string.Empty;
    public string CategoriesText { get; set; } = string.Empty;
    public string TotalCopiesText { get; set; } = "1";
}
