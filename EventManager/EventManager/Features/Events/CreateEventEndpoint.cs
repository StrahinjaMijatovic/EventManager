using Carter;
using Marten;

namespace EventManager.Features.Events
{
    public class CreateEventEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/events", async (Event newEvent, IDocumentSession session) =>
            {
                newEvent.Id = Guid.NewGuid();
                session.Store(newEvent);
                await session.SaveChangesAsync();
                return Results.Created($"/events/{newEvent.Id}", newEvent);
            });
        }
    }
}
