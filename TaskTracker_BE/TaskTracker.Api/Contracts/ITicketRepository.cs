using TaskTracker.Api.Data.DTO;

namespace TaskTracker.Api.Contracts;

public interface ITicketRepository
{
    Task<List<TicketDTO>> GetAllAsync(string? q, string? sort);
    Task<TicketDTO?> GetByIdAsync(int id);
    Task<TicketDTO> CreateAsync(TicketDTO ticket);
    Task<bool> UpdateAsync(TicketDTO ticket);
    Task<bool> DeleteAsync(int id);
}
