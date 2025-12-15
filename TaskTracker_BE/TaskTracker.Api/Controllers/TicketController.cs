using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.DTO;

using TaskTracker.Api.Data.Response;
using TaskTracker.API.Helpers;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketRepository repo,
IValidator<TicketDTO> validator
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(TicketDTO task)
    {
        validator.ValidateAndThrowValidationException(task);

        task = await repo.CreateAsync(task);

        return Ok(new Response<TicketDTO>
        {
            IsSuccess = true,
            Data = task
        });
    }

    [HttpGet]
    public async Task<ActionResult> GetTasks(
        [FromQuery] string? q,
        [FromQuery] string? sort = "dueDate:asc")
    {
        var tasks = await repo.GetAllAsync(q, sort);

        return Ok(new Response<List<TicketDTO>>() { Data = tasks, IsSuccess = true });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTask(int id)
    {
        var task = await repo.GetByIdAsync(id);

        if (task == null)
            return NotFound(new Response<TicketDTO> { Message = "Task not found", IsSuccess = false });

        return Ok(new Response<TicketDTO>
        {
            IsSuccess = true,
            Data = task
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TicketDTO task)
    {
        validator.ValidateAndThrowValidationException(task);

        // Map the editing Id to the task model
        task.Id = id;
        await repo.UpdateAsync(task);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await repo.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
