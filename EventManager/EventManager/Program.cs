using EventManager.Infrastructure;
using Marten;
using Carter;
using Azure.Storage.Blobs;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var postgresConnection = Env.GetString("POSTGRES_CONNECTION");
var azureBlobStorage = Env.GetString("AZURE_BLOB_STORAGE");

builder.Configuration["ConnectionStrings:PostgresConnection"] = postgresConnection;
builder.Configuration["ConnectionStrings:AzureBlobStorage"] = azureBlobStorage;

builder.Services.ConfigureMarten(postgresConnection);

var blobServiceClient = new BlobServiceClient(azureBlobStorage);
builder.Services.AddSingleton(blobServiceClient);


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
