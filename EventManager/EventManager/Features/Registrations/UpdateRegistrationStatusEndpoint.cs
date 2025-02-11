using Carter;
using Marten;

namespace EventManager.Features.Registrations
{
    public class UpdateRegistrationStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/registrations/{id:guid}/status", async 
                (Guid id, UpdateRegistrationStatusCommand command, IDocumentSession session) =>
            {
                var registration = await session.Query<Registration>().FirstOrDefaultAsync(r => r.Id == id);
                if (registration is null) return Results.NotFound();

                registration.Status = command.NewStatus;
                registration.UpdatedAt = DateTime.UtcNow;
                registration.RejectionReason = command.RejectionReason;

                session.Update(registration);
                await session.SaveChangesAsync();
                return Results.Ok(registration);
            });
        }
    }
}
