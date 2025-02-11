using Carter;
using Marten;
using EventManager.Features.Registrations;
using EventManager.Features.FilesMetadata;

namespace EventManager.Features.Events
{
    public class GetEventDetailsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/events/{id:guid}/details", async (Guid id, IDocumentSession session) =>
            {
                var evnt = await session.Query<Event>().FirstOrDefaultAsync(e => e.Id == id);
                if (evnt is null) return Results.NotFound();

                var registrations = await session.Query<Registration>().Where(r => r.EventId == id).ToListAsync();


                var files = await session.Query<FileMetadata>().Where(f => f.EventId == id).ToListAsync();

                return Results.Ok(new { Event = evnt, Regsitrations = registrations, Files = files });

            });
        }
    }
}
