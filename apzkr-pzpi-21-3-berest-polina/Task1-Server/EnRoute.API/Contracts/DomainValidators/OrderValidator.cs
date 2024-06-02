using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        private static readonly OrderStatus[] StatusesAllowed = Enum.GetValues(typeof(OrderStatus)) as OrderStatus[];

        public OrderValidator(ApplicationDbContext dbContext)
        {
            RuleFor(r => r.Status)
                .Must(r => StatusesAllowed.Contains(r)).WithMessage("Status is invalid. Allowed values are: " + string.Join(", ", StatusesAllowed));
        }
    }
}
