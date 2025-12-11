using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Data.Models;

namespace TaskTracker.Api;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
}
