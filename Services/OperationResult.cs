namespace SchoolLibraryApp.Services;

public class OperationResult
{
    public bool Success { get; init; }
    public List<string> Errors { get; init; } = new();

    public static OperationResult Ok() => new() { Success = true };
    public static OperationResult Fail(IEnumerable<string> errors) => new() { Success = false, Errors = errors.ToList() };
    public static OperationResult Fail(string error) => new() { Success = false, Errors = new List<string> { error } };
}
