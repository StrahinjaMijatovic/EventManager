using Carter;
using Marten;

namespace EventManager.Features.Events
{
    public class UpdateEventEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/events/{id:guid}", async (Guid id, Event updatedEvent, IDocumentSession session) =>
            {
                var existingEvent = await session.Query<Event>().FirstOrDefaultAsync(e => e.Id == id);
                if (existingEvent is null) return Results.NotFound();

                existingEvent.Name = updatedEvent.Name;
                existingEvent.Description = updatedEvent.Description;
                existingEvent.Location = updatedEvent.Location;
                existingEvent.Date = updatedEvent.Date;
                existingEvent.MaxParticipants = updatedEvent.MaxParticipants;
                existingEvent.OrganizerId = updatedEvent.OrganizerId;

                session.Update(existingEvent);
                await session.SaveChangesAsync();
                return Results.Ok(existingEvent);
            });
        }
    }
}
