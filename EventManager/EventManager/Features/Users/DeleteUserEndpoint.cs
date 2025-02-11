using Carter;
using Marten;
using Microsoft.VisualBasic;

namespace EventManager.Features.Users
{
    public class DeleteUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id:guid}", async (Guid id, IDocumentSession session) =>
            {
                var user = await session.Query<User>().FirstOrDefaultAsync(u => u.Id == id);
                if (user is null) return Results.NotFound();

                session.Delete(user);
                await session.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
