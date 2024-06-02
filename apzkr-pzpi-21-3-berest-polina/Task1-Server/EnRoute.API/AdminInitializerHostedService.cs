using System.Security.Claims;
using EnRoute.Common.Configuration;
using EnRoute.Common.Constants;
using EnRoute.Domain.Models;
using Microsoft.AspNetCore.Identity;

public class AdminInitializerHostedService : IHostedService
{
    private readonly IServiceProvider services;
    private readonly AdminSettings adminSettings;

    public AdminInitializerHostedService(IServiceProvider services, AdminSettings adminSettings)
    {
        this.services = services;
        this.adminSettings = adminSettings;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var userFromDb = await userManager.FindByEmailAsync(adminSettings.Email);
        if (userFromDb is not null)
        {
            return;
        }

        var newAdminUser = new User()
        {
            UserName = adminSettings.Email,
            Email = adminSettings.Email,
            Name = adminSettings.Email
        };
        var res = await userManager.CreateAsync(newAdminUser, adminSettings.Password);
        if (!res.Succeeded)
        {
            throw new Exception("Admin was not created.");
        }


        userFromDb = await userManager.FindByEmailAsync(adminSettings.Email);
        if (userFromDb is null)
        {
            throw new InvalidOperationException("User does not exist in database.");
        }

        var claimAddResult = await userManager.AddClaimAsync(userFromDb, new Claim(ClaimTypes.Role, UserRoles.Administrator));
        if (!claimAddResult.Succeeded)
        {
            throw new Exception("Admin claim was not assigned.");
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}