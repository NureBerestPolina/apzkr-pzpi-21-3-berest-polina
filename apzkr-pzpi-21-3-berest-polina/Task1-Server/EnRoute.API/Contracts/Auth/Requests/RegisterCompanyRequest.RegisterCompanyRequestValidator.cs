using EnRoute.Common.Constants;
using EnRoute.Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnRoute.API.Contracts.Auth.Requests;

public class RegisterCompanyRequestValidator : AbstractValidator<RegisterCompanyRequest>
{
    private static readonly IEnumerable<string> RolesAllowedForRegistration =
        UserRoles.AvailableRoles.Except(new[] { UserRoles.Administrator, UserRoles.Customer }).ToArray();

    public RegisterCompanyRequestValidator(ApplicationDbContext dbContext)
    {
        RuleFor(c => c.Password).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Role)
            .Must(c => RolesAllowedForRegistration.Contains(c.ToLower())).WithMessage("Registration role is invalid.");

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("Email address is not valid")
            .MustAsync((e, _) => dbContext.Users.AllAsync(u => u.Email != e)).WithMessage("User with this email already exists.");

        RuleFor(c => c.Description).NotEmpty().MinimumLength(3);
        RuleFor(c => c.OrganizationName).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Role)
            .Must(c => c.ToLower() != UserRoles.Customer).WithMessage("Registration role is invalid.");
    }
}