using Microsoft.AspNetCore.Authentication;

namespace MusicPlayer.IdentityService.Application.Services.Login;

public interface ILoginService<TUser>
{
    Task<bool> ValidateCredentialsAsync(TUser user, string password);
    Task<TUser> FindUserAsync(string userLogin);
    Task SignInAsync(TUser user, AuthenticationProperties properties, string? authenticationMethod = null);
}