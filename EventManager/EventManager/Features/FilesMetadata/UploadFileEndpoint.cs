using Carter;
using Azure.Storage.Blobs;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Features.FilesMetadata
{
    public class UploadFileEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/files/upload", async 
                    (IFormFile file, [FromForm] Guid eventId, BlobServiceClient blobServiceClient, 
                        IDocumentSession session) =>
            {
                try
                {
                    if (file == null || file.Length == 0)
                    {
                        return Results.BadRequest(new { Message = "No file uploaded or file is empty." });
                    }

                    var containerClient = blobServiceClient.GetBlobContainerClient("event-files");
                    await containerClient.CreateIfNotExistsAsync();

                    var blobClient = containerClient.GetBlobClient(file.FileName);
                    await blobClient.UploadAsync(file.OpenReadStream(), overwrite: true);

                    var fileRecord = new FileMetadata
                    {
                        Id = Guid.NewGuid(),
                        EventId = eventId,
                        FileName = file.FileName,
                        BlobUrl = blobClient.Uri.ToString(),
                        UploadedAt = DateTime.UtcNow
                    };

                    session.Store(fileRecord);
                    await session.SaveChangesAsync();

                    return Results.Ok(new { fileRecord.FileName, fileRecord.BlobUrl });
                }
                catch (Exception ex)
                {
                    //return Results.Problem("Internal server error.", statusCode: 500, detail = ex.Message);
                    return Results.Problem("Internal server error.", statusCode: 500);
                }
            });
        }
    }
}
