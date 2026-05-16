using FluentValidation;
using SchoolLibraryApp.ViewModels;

namespace SchoolLibraryApp.Validators;

public class ReaderFormValidator : AbstractValidator<ReaderFormModel>
{
    public ReaderFormValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Введите ФИО читателя.").MaximumLength(200);
        RuleFor(x => x.ClassName).NotEmpty().WithMessage("Введите класс читателя.").MaximumLength(20);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("Введите корректный email.");
    }
}
