using Carter;
using Marten;
using Marten.Schema;

namespace EventManager.Features.Users
{
    public class CreateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (User newUser, IDocumentSession session) =>
            {
                newUser.Id = Guid.NewGuid();
                newUser.CreatedAt = DateTime.Now;
                newUser.IsActive = true;

                session.Store(newUser);
                await session.SaveChangesAsync();
                return Results.Created($"/users/{newUser.Id}", newUser);
            });
        }
    }
}
