using EventManager.Infrastructure;
using Marten;
using Carter;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.ConfigureMarten(connectionString);

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
