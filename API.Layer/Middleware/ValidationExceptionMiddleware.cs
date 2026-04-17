using FluentValidation;

namespace API.Layer.Middleware
{

    // We create a middleware that intercepts bad requests and returns a personalized response.
    // Why do we use it? To get more specific error messages.
    public class ValidationExceptionMiddleware
    {
        // _next is the next middleware in the pipeline — calling it means "keep going"
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
         
                try
                {
                    // Let the request continue through the pipeline (eventually hits your controller)
                    await _next(context);
                }
                catch (ValidationException ex)
                {
                    // A validator threw. Intercept it and return a 400 response
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";

                    // Pull out just the property names and error messages
                    var errors = ex.Errors.Select(e => new
                    {
                        Property = e.PropertyName,
                        Error = e.ErrorMessage
                    });

                    // Write error as JSON
                    await context.Response.WriteAsJsonAsync(new { errors });
                }
        }
    }
}
