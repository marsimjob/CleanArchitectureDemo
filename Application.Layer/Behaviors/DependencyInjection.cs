using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application.Layer.Behaviors
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registers all MediatR handlers (commands, queries) found in this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            // Auto-discovers and registers all FluentValidation validators in this assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Hooks ValidationBehavior into the MediatR pipeline so validators run before handlers
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}