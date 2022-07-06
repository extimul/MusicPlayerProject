using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Serilog;
using ServerApp.Base.Extensions;

namespace MusicPlayer.IdentityService.Application.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var typeName = request.GetGenericTypeName();
            
        if (!_validators.Any()) return await next();
            
        Log.Information("Validating command {CommandType}", typeName);

        ValidationContext<TRequest> context = new(request);
        ValidationResult[] validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (!failures.Any()) return await next();
        
        Log.Warning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
            typeName, request, failures);

        throw new ValidationException("Validation exception", failures);

    }
}