using FluentValidation;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Validators;

public class LoanFormValidator : AbstractValidator<LoanFormModel>
{
    public LoanFormValidator()
    {
        RuleFor(x => x.BookIdText).NotEmpty().WithMessage("Выберите книгу.");
        RuleFor(x => x.ReaderIdText).NotEmpty().WithMessage("Выберите читателя.");
        RuleFor(x => x.ReturnUntilText)
            .Must(value => DateTime.TryParse(value, out var date) && date.Date >= DateTime.Today)
            .WithMessage("Дата возврата не может быть раньше текущей даты.");
    }
}
