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
                CreatedAt = DateTime.UtcNow.AddDays(2),
                DueDate = DateTime.UtcNow.AddDays(2)
            },
            new Ticket
            {
                Title = "Frontend UI setup",
                Description = "Create Angular app and services",
                Status = EStatus.New,
                Priority = EPriority.Medium,
                CreatedAt = DateTime.UtcNow.AddDays(1),
                DueDate = DateTime.UtcNow.AddDays(5)
            },
            new Ticket
            {
                Title = "Build Authentication Module",
                Description = "Implement JWT login and registration",
                Status = EStatus.New,
                Priority = EPriority.High,
                CreatedAt = DateTime.UtcNow.AddDays(5),
                DueDate = DateTime.UtcNow.AddDays(7)
            },
            new Ticket
            {
                Title = "Design Dashboard UI",
                Description = "Create wireframes for dashboard and task cards",
                Status = EStatus.InProgress,
                Priority = EPriority.Low,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(3)
            },
            new Ticket
            {
                Title = "Write Unit Tests for API",
                Description = "Add repository and controller tests",
                Status = EStatus.New,
                Priority = EPriority.Medium,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                DueDate = DateTime.UtcNow.AddDays(6)
            },
            new Ticket
            {
                Title = "Implement Dark/Light Mode",
                Description = "Allow users to toggle app theme",
                Status = EStatus.New,
                Priority = EPriority.Low,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                DueDate = DateTime.UtcNow.AddDays(10)
            },
            new Ticket
            {
                Title = "Optimize Database Queries",
                Description = "Add indexes and refactor slow LINQ queries",
                Status = EStatus.InProgress,
                Priority = EPriority.High,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                DueDate = DateTime.UtcNow.AddDays(4)
            },
            new Ticket
            {
                Title = "Add Notifications System",
                Description = "Email + in-app notifications for updates",
                Status = EStatus.New,
                Priority = EPriority.Medium,
                CreatedAt = DateTime.UtcNow.AddDays(-11),
                DueDate = DateTime.UtcNow.AddDays(12)
            },
            new Ticket
            {
                Title = "Create Settings Page",
                Description = "Profile, preferences, and account settings",
                Status = EStatus.New,
                Priority = EPriority.Low,
                CreatedAt = DateTime.UtcNow.AddDays(1),
                DueDate = DateTime.UtcNow.AddDays(8)
            },
            new Ticket
            {
                Title = "Improve Mobile Responsiveness",
                Description = "Fix UI issues on small screens",
                Status = EStatus.InProgress,
                Priority = EPriority.Medium,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                DueDate = DateTime.UtcNow.AddDays(2)
            }
        );

        context.SaveChanges();
    }
}
