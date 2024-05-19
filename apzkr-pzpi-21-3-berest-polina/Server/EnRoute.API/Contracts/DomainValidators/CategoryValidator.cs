using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator(ApplicationDbContext dbContext)
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
