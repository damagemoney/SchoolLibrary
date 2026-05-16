using FluentValidation;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Validators;

public class BookFormValidator : AbstractValidator<BookFormModel>
{
    public BookFormValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Введите название книги.").MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().WithMessage("Введите автора.").MaximumLength(150);
        RuleFor(x => x.PublishedYearText)
            .Must(value => int.TryParse(value, out var year) && year >= 1900 && year <= DateTime.Today.Year + 1)
            .WithMessage("Введите корректный год издания.");
        RuleFor(x => x.TotalCopiesText)
            .Must(value => int.TryParse(value, out var copies) && copies > 0)
            .WithMessage("Количество экземпляров должно быть больше 0.");
    }
}
