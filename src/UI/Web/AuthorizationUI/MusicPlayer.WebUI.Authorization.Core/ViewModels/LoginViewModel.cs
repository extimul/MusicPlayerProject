using FluentValidation;

namespace MusicPlayer.WebUI.Authorization.Core.ViewModels;

public class LoginViewModel
{
    public string Login { get; set; }
    
    public string Password { get; set; }
}

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage($"Поле \"Логин\" должно быть заполнено");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage($"Поле \"Пароль\" должно быть заполнено");
    }
}