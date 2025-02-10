using Carter;
using Marten;
using System.Security.Cryptography.X509Certificates;

namespace EventManager.Features.Events
{
    public class GetAllEventsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/events", async (IDocumentSession session) =>
            {
                var events = await session.Query<Event>().ToListAsync();
                return Results.Ok(events);
            });
        }
    }
}
