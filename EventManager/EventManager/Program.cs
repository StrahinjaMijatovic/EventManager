using EventManager.Infrastructure;
using Marten;
using Carter;
using Azure.Storage.Blobs;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var postgresConnection = Env.GetString("POSTGRES_CONNECTION");
var azureBlobStorage = Env.GetString("AZURE_BLOB_STORAGE");

builder.Configuration["ConnectionStrings:PostgresConnection"] = postgresConnection;
builder.Configuration["ConnectionStrings:AzureBlobStorage"] = azureBlobStorage;

builder.Services.ConfigureMarten(postgresConnection);

var blobServiceClient = new BlobServiceClient(azureBlobStorage);
builder.Services.AddSingleton(blobServiceClient);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.UseCors("AllowLocalhost5173");

app.Run();
