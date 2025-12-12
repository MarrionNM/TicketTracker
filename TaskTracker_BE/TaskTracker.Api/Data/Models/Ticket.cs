using TaskTracker.Api.Data.Enums;

namespace TaskTracker.Api.Data.Models;

public class Ticket : Base
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EStatus Status { get; set; }
    public EPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
}
