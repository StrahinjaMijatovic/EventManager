using Carter;
using Marten;

namespace EventManager.Features.Registrations
{
    public class GetRegistrationsByParticipantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/participants/{participantId:guid}/registrations",
                async (Guid participantId, IDocumentSession session) =>
            {
                var registrations = await session.Query<Registration>()
                    .Where(r => r.ParticipantId == participantId).ToListAsync();

                return Results.Ok(registrations);
            });
        }
    }
}
