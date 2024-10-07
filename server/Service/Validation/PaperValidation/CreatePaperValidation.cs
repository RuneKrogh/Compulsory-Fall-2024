using FluentValidation;
using Service.DTOs.Create;

namespace Service.Validation.PaperValidation;

public class CreatePaperValidation : AbstractValidator<CreatePaperDto>
{
    public CreatePaperValidation()
    {
        RuleFor(paper => paper.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 50).WithMessage("Name must be between 5 and 50 characters.");

        RuleFor(paper => paper.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock must be zero or a positive number.");

        RuleFor(paper => paper.Price)
            .GreaterThan(0)
            .WithMessage("Price must be a positive number.");
    }
}