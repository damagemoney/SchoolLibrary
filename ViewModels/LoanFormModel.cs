namespace SchoolLibraryApp.ViewModels;

public class LoanFormModel
{
    public string BookIdText { get; set; } = string.Empty;
    public string ReaderIdText { get; set; } = string.Empty;
    public string ReturnUntilText { get; set; } = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
}
