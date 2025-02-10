using EventManager.Features.Users;

namespace EventManager.Features.Registrations
{
    public enum RegistrationStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Registration
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid ParticipantId { get; set; }

        public RegistrationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? RejectionReason { get; set; } = string.Empty;

    }

    public record RegisterDto
    {

        public string Email { get; set; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public UserRole Role { get; set; }
    }

    public record RegisterForEventCommand
    {
        public Guid EventId { set; get; }

        public Guid ParticipantId { set; get; }
    }

    public record UpdateRegistrationStatusCommand
    {
        public Guid RegistrationId { set; get; }

        public RegistrationStatus NewStatus { set; get; }

        public string? RejectionReason { get; init; } = string.Empty;

    }

    public record RegistrationDto
    {
        public Guid EventId { get; set; }

        public Guid ParticipantId { get; set; }
    }

    public record RegistrationResponse
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid ParticipantId { get; set; }

        public RegistrationStatus Status { get; set; }

        public DateTime CreatedAt { set; get; }

        public string? RejectionReason { get; init; } = string.Empty;
    }

}
