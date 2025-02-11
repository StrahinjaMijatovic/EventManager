using Carter;
using Marten;

namespace EventManager.Features.Registrations
{
    public class RegisterForEventEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/registrations", async (RegisterForEventCommand command, IDocumentSession session) =>
            {
                var newRegistration = new Registration
                {
                    Id = Guid.NewGuid(),
                    EventId = command.EventId,
                    ParticipantId = command.ParticipantId,
                    Status = RegistrationStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                };

                session.Store(newRegistration);
                await session.SaveChangesAsync();
                return Results.Created($"/registrations/{newRegistration.Id}", newRegistration);
            });
        }
    }
}
