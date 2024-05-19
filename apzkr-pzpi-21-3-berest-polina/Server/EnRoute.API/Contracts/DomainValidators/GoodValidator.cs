using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class GoodValidator : AbstractValidator<Good>
    {
        public GoodValidator(ApplicationDbContext dbContext)
        {
            RuleFor(good => good.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(good => good.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(good => good.MeasurementUnit)
                .NotEmpty().WithMessage("MeasurementUnit is required.")
                .MaximumLength(50).WithMessage("MeasurementUnit cannot exceed 50 characters.");

            RuleFor(good => good.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(good => good.AmountInStock)
                .GreaterThanOrEqualTo(0).WithMessage("AmountInStock cannot be negative.");
        }
    }
}
