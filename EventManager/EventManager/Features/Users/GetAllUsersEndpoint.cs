using Carter;
using Marten;

namespace EventManager.Features.Users
{
    public class GetAllUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (IDocumentSession session) =>
            {
                var users = await session.Query<User>().ToListAsync();
                return Results.Ok(users);
            });
        }
    }
}
