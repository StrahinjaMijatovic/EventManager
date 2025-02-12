using EventManager.Features.Users;

namespace EventManager.Features.Auth
{
    public class AuthModels
    {
        public record RegisterRequest(string FirstName, string LastName, string Email, string Password, UserRole Role);
        public record LoginRequest(string Email, string Password);
        public record AuthResponse(string Token);
    }

}

