using FluentValidation;
using Service.DTOs.Create;

namespace Service.Validation.OrderEntryValidation
{
    public class CreateOrderEntryValidation : AbstractValidator<CreateOrderEntryDto>
    {
        public CreateOrderEntryValidation()
        {
            RuleFor(orderEntry => orderEntry.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(orderEntry => orderEntry.ProductId)
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than 0.");

            RuleFor(orderEntry => orderEntry.OrderId)
                .GreaterThan(0)
                .WithMessage("Order ID must be greater than 0.");
        }
    }
}