namespace EventManager.Features.Users
{
    public enum UserRole
    {
        Admin,
        Organizer,
        Participant
    }

    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }

    public class DeleteUserModel
    {
        public string Email { set; get; } = string.Empty;
    }
}
