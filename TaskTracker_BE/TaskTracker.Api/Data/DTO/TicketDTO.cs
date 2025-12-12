using System.ComponentModel.DataAnnotations;
using TaskTracker.Api.Data.Enums;

namespace TaskTracker.Api.Data.DTO;

public class TicketDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MinLength(1, ErrorMessage = "Title cannot be empty.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Description cannot be empty.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(EStatus), ErrorMessage = "Invalid task status.")]
    public EStatus Status { get; set; }

    [Required]
    [EnumDataType(typeof(EPriority), ErrorMessage = "Invalid priority value.")]
    public EPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
