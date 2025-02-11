namespace EventManager.Features.Events
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        
        public int MaxParticipants { get; set; }

        public Guid OrganizerId { get; set; }
    }

    public class EventFilterQuery
    {
        public DateTime? Date { get; set; }

        public string? Location { get; set; }

        public string? Type { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; }
    }
}
