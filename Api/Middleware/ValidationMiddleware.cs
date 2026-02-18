using System.Text.Json;
using Application.Security.Dtos;

namespace Api.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method.Equals("GET")
            && context.Request.Path.Value!.ToLower().Contains("/register"))
        {
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Wrong http method",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Detail",
                Instance = context.Request.Path
            });
            return;
        }

        if (context.Request.Method.Equals("POST"))
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            try
            {
                var model = JsonSerializer.Deserialize<RegisterUserRequestDto>(body);
                if (model?.Password is not null && !IsValidPassword(model.Password))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        Title = "Password is incorrect",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Detail",
                        Instance = context.Request.Path
                    });
                    return;
                }
                await next(context);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Detail",
                    Instance = context.Request.Path
                });
            }
        }
        else
        {
            await next(context);
        }
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 4 && password.Length <= 8 && password.Any(char.IsDigit);
    }
}