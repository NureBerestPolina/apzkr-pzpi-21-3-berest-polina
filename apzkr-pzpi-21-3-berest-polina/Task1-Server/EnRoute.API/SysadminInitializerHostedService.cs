using System.Security.Claims;
using EnRoute.Common.Configuration;
using EnRoute.Common.Constants;
using EnRoute.Domain.Models;
using Microsoft.AspNetCore.Identity;

public class SysadminInitializerHostedService : IHostedService
{
    private readonly IServiceProvider services;
    private readonly SysadminSettings sysadminSettings;

    public SysadminInitializerHostedService(IServiceProvider services, SysadminSettings sysadminSettings)
    {
        this.services = services;
        this.sysadminSettings = sysadminSettings;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var userFromDb = await userManager.FindByEmailAsync(sysadminSettings.Email);
        if (userFromDb is not null)
        {
            return;
        }

        var newAdminUser = new User()
        {
            UserName = sysadminSettings.Email,
            Email = sysadminSettings.Email,
            Name = sysadminSettings.Email
        };
        var res = await userManager.CreateAsync(newAdminUser, sysadminSettings.Password);
        if (!res.Succeeded)
        {
            throw new Exception("System administrator was not created.");
        }


        userFromDb = await userManager.FindByEmailAsync(sysadminSettings.Email);
        if (userFromDb is null)
        {
            throw new InvalidOperationException("User does not exist in database.");
        }

        var claimAddResult = await userManager.AddClaimAsync(userFromDb, new Claim(ClaimTypes.Role, UserRoles.SystemAdministrator));

        if (!claimAddResult.Succeeded)
        {
            throw new Exception("System administrator claim was not assigned.");
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}