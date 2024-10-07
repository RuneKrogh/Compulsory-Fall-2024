using FluentValidation;
using Service.DTOs.Create;

namespace Service.Validation.OrderValidation
{
    public class CreateOrderValidation : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidation()
        {
            // Validate OrderDate
            RuleFor(order => order.OrderDate)
                .NotEmpty().WithMessage("Order date is required.")
                .Must(date => date <= DateTime.Now).WithMessage("Order date cannot be in the future.");

            // Validate DeliveryDate (if provided)
            RuleFor(order => order.DeliveryDate)
                .Must((dto, date) => date == null || date >= DateOnly.FromDateTime(dto.OrderDate))
                .WithMessage("Delivery date must be greater than or equal to the order date.");

            // Validate Status
            RuleFor(order => order.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Length(1, 15).WithMessage("Status must be between 1 and 15 characters.");

            // Validate TotalAmount
            RuleFor(order => order.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than 0.");

            // Validate OrderEntries
            RuleFor(order => order.OrderEntries)
                .NotEmpty().WithMessage("At least one order entry is required.")
                .Must(entries => entries.All(entry => entry != null && entry.Quantity > 0 && entry.ProductId > 0))
                .WithMessage("Each order entry must have a valid product ID and a quantity greater than 0.");
        }
    }
}