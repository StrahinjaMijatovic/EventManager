using Carter;
using Marten;

namespace EventManager.Features.Registrations
{
    public class GetRegistrationsByEventEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/events/{eventId:guid}/registrations", async (Guid eventId, IDocumentSession session) =>
            {
                var registrations = await session.Query<Registration>().Where(r => r.EventId == eventId).ToListAsync();
                
                return Results.Ok(registrations);
            });
        }
    }
}
