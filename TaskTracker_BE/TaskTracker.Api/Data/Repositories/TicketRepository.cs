using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.DTO;
using TaskTracker.Api.Data.Models;

namespace TaskTracker.Api.Data.Repositories;

public class TicketRepository(AppDbContext context, IMapper mapper) : ITicketRepository
{

    public async Task<TicketDTO> CreateAsync(TicketDTO ticket)
    {
        await context.Tickets.AddAsync(mapper.Map<Ticket>(ticket));
        await context.SaveChangesAsync();
        return ticket;
    }

    public async Task<List<TicketDTO>> GetAllAsync(string? q, string? sort)
    {
        var query = context.Tickets.AsQueryable();

        if (!string.IsNullOrEmpty(q))
        {
            q = q.ToLower();
            query = query.Where(t =>
                t.Title.ToLower().Contains(q) ||
                t.Description.ToLower().Contains(q));
        }

        query = sort == "desc"
            ? query.OrderByDescending(t => t.DueDate)
            : query.OrderBy(t => t.DueDate);

        return mapper.Map<List<TicketDTO>>(await query.ToListAsync());
    }

    public async Task<TicketDTO?> GetByIdAsync(int id)
        => mapper.Map<TicketDTO>(await context.Tickets.FindAsync(id));

    public async Task<bool> UpdateAsync(TicketDTO dto)
    {
        var existing = await context.Tickets
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (existing == null)
            return false;

        mapper.Map(dto, existing);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Ticket = await context.Tickets.FindAsync(id);
        if (Ticket == null) return false;

        context.Tickets.Remove(Ticket);
        return await context.SaveChangesAsync() > 0;
    }
}
