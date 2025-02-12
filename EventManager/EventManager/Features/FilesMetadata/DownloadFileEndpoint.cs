using Azure.Storage.Blobs;
using Carter;
using Marten;

namespace EventManager.Features.FilesMetadata
{
    public class DownloadFileEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/files/{id:guid}", async (Guid id, IDocumentSession session, BlobServiceClient blobServiceClient) =>
            {
                // Pronađi metapodatke fajla u bazi
                var fileRecord = await session.Query<FileMetadata>()
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (fileRecord == null)
                {
                    return Results.NotFound(new { Message = "File not found." });
                }

                try
                {
                    var containerClient = blobServiceClient.GetBlobContainerClient("event-files");
                    var blobClient = containerClient.GetBlobClient(fileRecord.FileName);

                    // Proveri da li fajl postoji na Azure Blob Storage
                    if (!await blobClient.ExistsAsync())
                    {
                        return Results.NotFound(new { Message = "File not found in Azure Blob Storage." });
                    }

                    // Preuzmi sadržaj fajla
                    var blobDownloadInfo = await blobClient.DownloadAsync();
                    return Results.File(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType, fileRecord.FileName);
                }
                catch (Exception ex)
                {
                    return Results.Problem("Error retrieving file.", statusCode: 500);
                }
            });
        }
    }
}
