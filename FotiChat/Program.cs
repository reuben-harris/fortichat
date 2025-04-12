using FortiChat.Data;
using FortiChat.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Ensure the SQLite in-memory connection remains open for the application's lifetime.
var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

// Register the connection (optional, if you need it elsewhere).
builder.Services.AddSingleton(connection);

// Add the DbContext to the DI container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connection));

// Optional: If you would like EF Core to apply migrations or create the database at startup,
// you can add a startup task later (or incorporate it into a hosted service).

// Add your hosted service that does the work
builder.Services.AddHostedService<Dev>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();