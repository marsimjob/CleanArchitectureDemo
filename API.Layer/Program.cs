using API.Layer.Middleware;
using Application.Layer.Behaviors;
using Application.Layer.Interfaces;
using Application.Layer.Services;
using Infrastructure.Layer;
using Microsoft.EntityFrameworkCore;
namespace API.Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();

            // OpenAI services
            builder.Services.AddScoped<IOpenAIService, OpenAIService>();

            // Mapping Models to DTOs
            builder.Services.AddAutoMapper(typeof(ToyMappingProfile));

            // Register DbContext with connection string
            builder.Services.AddDbContext<Infrastructure.Layer.AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<IToyRepository, ToyRepository>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}