using TaskTracker.Api.Data.Models;
using TaskTracker.Api.Data.Enums;

namespace TaskTracker.Api.Data.Seeding;

public static class SeedData
{
    public static void Seed(AppDbContext context)
    {
        if (context.Tickets.Any())
            return;

        context.Tickets.AddRange(
            new Ticket
            {
                Title = "Create API structure",
                Description = "Set up controllers, models, repo pattern",
                Status = EStatus.InProgress,
                Priority = EPriority.High,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(2)
            },
            new Ticket
            {
                Title = "Frontend UI setup",
                Description = "Create Angular app and services",
                Status = EStatus.New,
                Priority = EPriority.Medium,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5)
            }
        );

        context.SaveChanges();
    }
}
