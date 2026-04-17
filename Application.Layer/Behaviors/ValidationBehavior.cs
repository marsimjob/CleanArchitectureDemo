using FluentValidation;
using MediatR;

// WHY THIS EXISTS:
// This is a MediatR "pipeline behavior" — think of it like middleware, but for MediatR commands/queries.
// Every time you call _sender.Send(someCommand), the request passes through this behavior BEFORE
// reaching your actual handler (e.g., CreateToyCommandHandler).
//
// THE FLOW:
//   Controller calls _sender.Send(new CreateToyCommand { Name = "" })
//       ↓
//   ValidationBehavior runs FIRST — checks if any validators exist for CreateToyCommand
//       ↓ (if validators found)
//   Runs CreateCommandValidator → finds "Name is empty" → throws ValidationException
//       ↓ (exception bubbles up to ValidationExceptionMiddleware → 400 response)
//
//   OR if validation passes:
//       ↓
//   Calls next() → CreateToyCommandHandler.Handle() runs normally
//
// WHY GENERIC <TRequest, TResponse>:
// This single class handles ALL commands and queries. You don't write a separate behavior
// for CreateToy, DeleteToy, GetAllToys, etc. The generics mean:
//   - TRequest = whatever command/query was sent (CreateToyCommand, DeleteToyCommand, etc.)
//   - TResponse = whatever that command returns (ToyDto, Unit, List<ToyDto>, etc.)
// DI automatically injects only the validators that match TRequest.
//
// EXAMPLE WITH YOUR CODE:
//   Send(CreateToyCommand) → _validators contains [CreateCommandValidator] → validates → next()
//   Send(DeleteToyCommand) → _validators contains [DeleteToyCommandValidator] → validates → next()
//   Send(GetAllToysQuery)  → _validators is empty (no validator class for it) → skips straight to next()
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    // DI injects ALL validators registered for TRequest.
    // If TRequest is CreateToyCommand, this contains [CreateCommandValidator].
    // If no validator exists for a given request, this is an empty collection — not null.
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    // This method is called by MediatR BEFORE your actual handler
    // "next" is a delegate that calls the next thing in the pipeline (your handler)
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Only run validation if there are validators for this request type
        if (_validators.Any())
        {
            // Wrap the request in a FluentValidation context
            var context = new ValidationContext<TRequest>(request);

            // Run every validator and collect all failures into one flat list
            // .Select  → runs each validator, gets a ValidationResult per validator
            // .SelectMany → flattens all errors from all results into one list
            // .Where → filters out any nulls (safety check)
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            // If ANY validation rule failed, throw — this stops the handler from running
            // The ValidationExceptionMiddleware catches this and returns a 400
            if (failures.Any())
                throw new ValidationException(failures);
        }

        // Validation passed (or no validators exist) — continue to the actual handler
        return await next();
    }
}