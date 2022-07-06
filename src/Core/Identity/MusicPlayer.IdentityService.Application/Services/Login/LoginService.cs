using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MusicPlayer.IdentityService.Domain.Entities;

namespace MusicPlayer.IdentityService.Application.Services.Login;

public class LoginService : ILoginService<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

    public async Task<ApplicationUser> FindUserAsync(string userLogin)
        => await _userManager.FindByEmailAsync(userLogin);

    public async Task SignInAsync(ApplicationUser user, AuthenticationProperties properties,
        string? authenticationMethod = null) => await _signInManager.SignInAsync(user, properties, authenticationMethod);
}