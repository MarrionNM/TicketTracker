using TaskTracker.Api.Data.Enums;

namespace TaskTracker.Api.Data.DTO;

public class TicketDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public EStatus Status { get; set; }

    public EPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
