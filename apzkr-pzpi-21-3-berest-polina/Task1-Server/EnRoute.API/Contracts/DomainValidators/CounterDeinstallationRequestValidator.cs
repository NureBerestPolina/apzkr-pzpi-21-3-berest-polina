using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class CounterDeinstallationRequestValidator : AbstractValidator<CounterDeinstallationRequest>
    {
        private static readonly RequestStatus[] StatusesAllowed = Enum.GetValues(typeof(RequestStatus)) as RequestStatus[];

        public CounterDeinstallationRequestValidator(ApplicationDbContext dbContext)
        {
            RuleFor(r => r.RequestStatus)
                .Must(r => StatusesAllowed.Contains(r)).WithMessage("Request status is invalid. Allowed values are: " + string.Join(", ", StatusesAllowed));
        }
    }
}
