using FluentValidation;

namespace MusicPlayer.IdentityService.Models;

public class SignInViewModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
    
    public bool RememberMe { get; set; }
}

public class SingInViewModelValidator : AbstractValidator<SignInViewModel>
{
    public SingInViewModelValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage($"Поле \"Логин\" должно быть заполнено");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage($"Поле \"Пароль\" должно быть заполнено");
    }
}