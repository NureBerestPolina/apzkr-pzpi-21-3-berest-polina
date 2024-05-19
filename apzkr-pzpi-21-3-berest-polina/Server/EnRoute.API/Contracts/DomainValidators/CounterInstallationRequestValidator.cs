using EnRoute.Domain.Constants;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using FluentValidation;

namespace EnRoute.API.Contracts.DomainValidators
{
    public class CounterInstallationRequestValidator : AbstractValidator<CounterInstallationRequest>
    {
        private static readonly RequestStatus[] StatusesAllowed = Enum.GetValues(typeof(RequestStatus)) as RequestStatus[];

        public CounterInstallationRequestValidator(ApplicationDbContext dbContext)
        {
            RuleFor(request => request.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(request => request.PlacementDescription)
                .NotEmpty().WithMessage("PlacementDescription is required.")
                .MaximumLength(500).WithMessage("PlacementDescription cannot exceed 500 characters.");

            RuleFor(request => request.CellCount)
                .GreaterThan(0).WithMessage("CellCount must be greater than 0.");

            RuleFor(request => request.CellWithTempControlCount)
                .LessThanOrEqualTo(request => request.CellCount).WithMessage("CellWithTempControlCount must be less or equal to CellCount.");

            RuleFor(r => r.RequestStatus)
                .Must(r => StatusesAllowed.Contains(r)).WithMessage("Request status is invalid. Allowed values are: " + string.Join(", ", StatusesAllowed));
        }
    }
}
