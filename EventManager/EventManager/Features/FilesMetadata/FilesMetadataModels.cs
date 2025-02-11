namespace EventManager.Features.FilesMetadata
{
    public class FileMetadata
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string BlobUrl { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
