using BoardGameNight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BoardGameNight.Services;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    private readonly UserManager<Persoon> _userManager;

    public MinimumAgeHandler(UserManager<Persoon> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var user = await _userManager.GetUserAsync(context.User);

        if (user == null)
        {
            return;
        }

        var age = user.GetAge();

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
    }
}