using Carter;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventManager.Features.Registrations
{
    public class GetRegistrationsByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/registrations/{id:guid}", async Task<Results<Ok<Registration>, NotFound>> (Guid id, IDocumentSession session) =>
            {
                var registration = await session.Query<Registration>()
                    .FirstOrDefaultAsync(r => r.Id == id);

                return registration is not null ? TypedResults.Ok(registration) : TypedResults.NotFound();
            });
        }
    }
}
