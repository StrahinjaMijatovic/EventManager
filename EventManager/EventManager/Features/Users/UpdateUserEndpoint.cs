using Carter;
using Marten;

namespace EventManager.Features.Users
{
    public class UpdateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/users/{id:guid}", async (Guid id, User updatedUser, IDocumentSession session) =>
            {
                var existingUser = await session.Query<User>().FirstOrDefaultAsync(u => u.Id == id);
                if (existingUser is null) return Results.NotFound();

                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.Email = updatedUser.Email;
                existingUser.IsActive = updatedUser.IsActive;
                existingUser.UpdatedAt = updatedUser.UpdatedAt;

                //Role moze menjati samo admin
                if (updatedUser.Role != existingUser.Role)
                {
                    return Results.BadRequest("You are not allowe to change userS role.");
                }
                
                session.Update(existingUser);
                await session.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
