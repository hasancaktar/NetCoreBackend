using FluentValidation;
using MediatR;

namespace Dem.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var failtures = _validators
                .Select(validate => validate
                .ValidateAsync(request))
                .SelectMany(result => result.Result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failtures.Any())
                throw new ValidationException(failtures);
        }
        return next();
    }
}