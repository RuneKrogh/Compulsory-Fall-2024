﻿using FluentValidation;
using Service.DTOs.Read;

namespace Service.Validation.PropertyValidation;

public class UpdatePropertyValidation : AbstractValidator<PropertyDto>
{
    public UpdatePropertyValidation()
    {
        RuleFor(property => property.PropertyName)
            .NotEmpty().WithMessage("Property name is required.")
            .Length(5, 50).WithMessage("Property name must be between 5 and 50 characters.");
    }
}