using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator(ApplicationDbContext dbContext)
        {
            RuleFor(item => item.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0.");
        }
    }
}
