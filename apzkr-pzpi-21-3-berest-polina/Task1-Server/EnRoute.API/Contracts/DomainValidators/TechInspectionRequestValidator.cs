using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class TechInspectionRequestValidator : AbstractValidator<TechInspectionRequest>
    {
        private static readonly RequestStatus[] StatusesAllowed = Enum.GetValues(typeof(RequestStatus)) as RequestStatus[];

        public TechInspectionRequestValidator(ApplicationDbContext dbContext)
        {
            RuleFor(r => r.Status)
                .Must(r => StatusesAllowed.Contains(r)).WithMessage("Status is invalid. Allowed values are: " + string.Join(", ", StatusesAllowed));
        }
    }
}
