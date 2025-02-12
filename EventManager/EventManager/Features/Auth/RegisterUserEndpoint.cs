using Carter;
using EventManager.Features.Auth.Services;
using Microsoft.AspNetCore.Identity.Data;

namespace EventManager.Features.Auth
{
    public class RegisterUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register", async (AuthModels.RegisterRequest request, RegisterUserService registerUserService) =>
            { 
                var result = await registerUserService.RegisterUserAsync(request);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }

                return Results.Ok(new { Message = "User registered successfully." });
            });
        }
        
    }
}
