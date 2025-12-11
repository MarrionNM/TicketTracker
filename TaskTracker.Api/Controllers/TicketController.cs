using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.DTO;


// using TaskTracker.Api.Data.DTO;
using TaskTracker.Api.Data.Models;
using TaskTracker.Api.Data.Repositories;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketRepository repo) : ControllerBase
{
    private readonly ITicketRepository _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDTO>>> GetTasks(
        [FromQuery] string? q,
        [FromQuery] string? sort = "dueDate:asc")
    {
        var tasks = await _repo.GetAllAsync(q, sort);

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDTO>> GetTask(int id)
    {
        var t = await _repo.GetByIdAsync(id);
        if (t == null) return NotFound();

        return Ok(new TicketDTO
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            DueDate = t.DueDate,
            CreatedAt = t.CreatedAt
        });
    }

    [HttpPost]
    public async Task<ActionResult<TicketDTO>> Create(TicketDTO dto)
    {
        var task = new TicketDTO
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.CreateAsync(task);

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TicketDTO dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Status = dto.Status;
        existing.Priority = dto.Priority;
        existing.DueDate = dto.DueDate;

        await _repo.UpdateAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
