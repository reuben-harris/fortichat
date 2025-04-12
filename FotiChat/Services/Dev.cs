using FortiChat.Data;
using FortiChat.Models;

namespace FortiChat.Services;

public class Dev : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    // Inject IServiceScopeFactory so you can create a scope with a fresh DbContext instance.
    public Dev(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {   
        // Execute database work inside a scoped lifetime.
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Create the database if it doesn't exist.
            context.Database.EnsureCreated();

            // Add a new ChatMessage entry if there are no entries.
            if (!context.ChatMessages.Any())
            {
                var chat = new ChatMessage
                {
                    UserId = "user123",
                    Timestamp = DateTime.Now,
                    Message = "Hello, world!"
                };

                context.ChatMessages.Add(chat);
                context.SaveChanges();

                Console.WriteLine("Message added to the database.");
            }

            // Retrieve and display all chat messages from the database.
            var messages = context.ChatMessages.ToList();
            Console.WriteLine("All messages in the database:");
            foreach (var m in messages)
            {
                Console.WriteLine($"Id: {m.ChatMessageId}, UserId: {m.UserId}, Timestamp: {m.Timestamp}, Message: {m.Message}");
            }
        }

        return Task.CompletedTask;
    }

    // This method is executed when the app is shutting down.
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
