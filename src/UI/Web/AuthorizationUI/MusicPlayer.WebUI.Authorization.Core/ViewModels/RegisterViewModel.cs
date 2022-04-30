using FluentValidation;

namespace MusicPlayer.WebUI.Authorization.Core.ViewModels;

public class RegisterViewModel
{
    public string Name { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public bool IsAcceptTerms { get; set; }
}

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Поле \"Имя\" должно быть заполнено");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Поле \"Имя пользователя\" должно быть заполнено")
            .Length(4, 36).WithMessage(x => "Длина поля \"Имя пользователя\" должна быть в промежутке {MinLength} - {MaxLength}");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Поле \"Пароль\" должно быть заполнено")
            .Length(8, 30).WithMessage("Длина поля \"Пароль\" должна быть в промежутке {MinLength} - {MaxLength}");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Введите корректную электронную почту")
            .NotEmpty().WithMessage("Поле \"Электронная почта\" должно быть заполнено");

        RuleFor(x => x.IsAcceptTerms)
            .NotEqual(false)
            .WithMessage("Если вы согласны с условиями Пользовательского соглашения, то поставьте галочку напротив");
    }
}