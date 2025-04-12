using FortiChat.Models;
using Microsoft.EntityFrameworkCore;

namespace FortiChat.Data;

public class AppDbContext : DbContext
{
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define entity configurations here if needed.
    }
}