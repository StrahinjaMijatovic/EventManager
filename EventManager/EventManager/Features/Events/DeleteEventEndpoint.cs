using Carter;
using Marten;

namespace EventManager.Features.Events
{
    public class DeleteEventEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/events/{id:guid}", async (Guid id, IDocumentSession session) =>
            {
                session.DeleteWhere<Event>(e => e.Id == id);
                await session.SaveChangesAsync();
                return Results.NoContent();
            });        
        }
    }
}
