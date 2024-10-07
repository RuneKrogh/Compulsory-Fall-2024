using FluentValidation;
using Service.DTOs.Read;

namespace Service.Validation
{
    public class UpdateCustomerValidation : AbstractValidator<CustomerDto>
    {
        public UpdateCustomerValidation()
        {
            // Name validation
            RuleFor(customer => customer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            // Address validation
            RuleFor(customer => customer.Address)
                .MaximumLength(100)
                .WithMessage("Address must not exceed 100 characters.")
                .When(customer => !string.IsNullOrWhiteSpace(customer.Address)); // Only validate if not null or empty

            // Phone validation
            RuleFor(customer => customer.Phone)
                .Matches(@"^\+?[0-9]{1,3}[-. ]?[0-9]{1,14}$")
                .WithMessage("Phone number must be in a valid format, e.g., +45 1234 5678.")
                .When(customer => !string.IsNullOrWhiteSpace(customer.Phone)); // Only validate if not null or empty

            // Email validation
            RuleFor(customer => customer.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .When(customer => !string.IsNullOrWhiteSpace(customer.Email)); // Only validate if not null or empty
        }
    }
}