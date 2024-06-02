using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class ProducerValidator : AbstractValidator<Producer>
    {
        public ProducerValidator(ApplicationDbContext dbContext)
        {
            RuleFor(producer => producer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(producer => producer.ProducerCountry)
                .NotEmpty().WithMessage("ProducerCountry is required.")
                .MaximumLength(255).WithMessage("ProducerCountry cannot exceed 255 characters.");
        }
    }
}
