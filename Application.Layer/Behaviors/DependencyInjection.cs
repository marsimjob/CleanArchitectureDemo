using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application.Layer.Behaviors
{
    // Why? Its a much cleaner and approporaite way to extend services to Program.cs from the DI own layer.
    //   builder.Services.AddApplicationServices(); in Program.cs

    // How to know why I put it here? You think "What does this layer provide that other layer need to use?
    // * Applicaton: MediatR, Validators, PipelineBehaviors
    // * Infrastructure: DbContext, Repositories and extnral Services (in my case its AI)

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // WHAT: Scans this assembly (Application.Layer.dll) and finds every class that implements
            //       IRequestHandler<TRequest, TResponse> — which includes all your command and query handlers.
            // WHY:  Without this, MediatR wouldn't know CreateToyCommandHandler exists.
            //       _sender.Send(new CreateToyCommand()) would throw "no handler found."
            // HOW IT FINDS THEM: Assembly.GetExecutingAssembly() = "the DLL this code is compiled into"
            //       It uses reflection to scan every class and registers the ones that implement MediatR interfaces.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // WHAT: Same idea but for FluentValidation. Scans for every class that extends AbstractValidator<T>.
            // WHY:  So that ValidationBehavior can ask DI for IEnumerable<IValidator<CreateToyCommand>>
            //       and get [CreateCommandValidator] automatically injected.
            // ADDING A NEW VALIDATOR: Just create the class in this project. That's it. This line picks it up.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // WHAT: Tells MediatR "before running any handler, run ValidationBehavior first."
            // WHY:  This is what connects your validators to MediatR's pipeline.
            //       Without this line, your validators would be registered but never called.
            // typeof(IPipelineBehavior<,>) — the open generic means it applies to ALL request types,
            //       not just one specific command. One registration covers CreateToy, DeleteToy, GetAll, etc.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}