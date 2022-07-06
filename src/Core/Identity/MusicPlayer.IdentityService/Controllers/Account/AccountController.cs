using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.IdentityService.Application.Services.Login;
using MusicPlayer.IdentityService.Domain.Entities;
using MusicPlayer.IdentityService.Models;
using Serilog;
using ServerApp.WebApp.Base.Extensions;

namespace MusicPlayer.IdentityService.Controllers.Account;

[Route("account")]
public class AccountController : Controller
{
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly IClientStore _clientStore;
    private readonly ILoginService<ApplicationUser> _loginService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(IIdentityServerInteractionService interactionService,
        IClientStore clientStore,
        ILoginService<ApplicationUser> loginService,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager)
    {
        _interactionService = interactionService;
        _clientStore = clientStore;
        _loginService = loginService;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpGet("signin")]
    public async Task<IActionResult> SignIn(string returnUrl)
    {
        var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null)
        {
            ModelState.AddModelError("External Login", "External login is not implemented!");
        }

        var viewModel = await BuildSignInViewModelAsync(returnUrl, context);
        
        ViewData["ReturnUrl"] = returnUrl;
        return View(viewModel);
    }
    
    [HttpPost("signin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _loginService.FindUserAsync(model.Login);
            var isValidCredentials = await _loginService.ValidateCredentialsAsync(user, model.Password);
            if (isValidCredentials)
            {
                var tokenLifeTime = _configuration.GetValue("TokenLifetimeMinutes", 1440);

                var props = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifeTime),
                    AllowRefresh = true,
                    RedirectUri = model.ReturnUrl
                };

                if (model.RememberMe)
                {
                    var permanentTokenLifetime = _configuration.GetValue("PermanentTokenLifetimeDays", 1825);
                    props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentTokenLifetime);
                    props.IsPersistent = true;
                }

                await _loginService.SignInAsync(user, props);
                
                if (_interactionService.IsValidReturnUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            
            ModelState.AddModelError("", "Неверные данные для входа");
        }

        var vm = await BuildSignInViewModelAsync(model);
        ViewData["ReturnUrl"] = model.ReturnUrl;
        return View(vm);
    }
    
    private Task<SignInViewModel> BuildSignInViewModelAsync(string returnUrl, AuthorizationRequest context) =>
        Task.FromResult(new SignInViewModel
        {
            ReturnUrl = returnUrl,
            Login = context?.LoginHint,
        });
    
    private async Task<SignInViewModel> BuildSignInViewModelAsync(SignInViewModel model)
    {
        var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
        var viewModel = await BuildSignInViewModelAsync(model.ReturnUrl, context);
        viewModel.Login = model.Login;
        viewModel.RememberMe = model.RememberMe;
        return viewModel;
    }


    [HttpGet("signup")]
    public IActionResult SignUp(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("signup")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(SignUpViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            ApplicationUser user = new()
            {
                UserName = model.UserName,
                Name = model.Name,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Errors.Any())
            {
                ModelState.AddErrors(result);
                return View(model);
            }

            Log.Information("Successfully added new user - {UserName}", user.UserName);
        }

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("SignIn", "Account");
        
        if (HttpContext.User.Identity is {IsAuthenticated: true})
            return Redirect(returnUrl);
        if (ModelState.IsValid)
            return RedirectToAction("SignIn", "Account", new {returnUrl});
        return View(model);
    }

    [HttpGet("redirecting")]
    public IActionResult Redirecting() => View();
}