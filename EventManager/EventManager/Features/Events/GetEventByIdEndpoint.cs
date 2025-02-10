using Carter;
using JasperFx.CodeGeneration.Frames;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventManager.Features.Events
{
    public class GetEventByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/events/{id:guid}", async Task<Results<Ok<Event>, NotFound>> (Guid id, IDocumentSession session) =>
            {
                var evnt = await session.Query<Event>().FirstOrDefaultAsync(e => e.Id == id);
                return evnt is not null ? TypedResults.Ok(evnt) : TypedResults.NotFound();
            });
        }
    }
}
