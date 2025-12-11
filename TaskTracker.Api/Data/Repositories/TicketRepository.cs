using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.DTO;
using TaskTracker.Api.Data.Models;

namespace TaskTracker.Api.Data.Repositories;

public class TicketRepository(AppDbContext context, IMapper mapper) : ITicketRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<TicketDTO>> GetAllAsync(string? q, string? sort)
    {
        var query = _context.Tickets.AsQueryable();

        if (!string.IsNullOrEmpty(q))
        {
            q = q.ToLower();
            query = query.Where(t =>
                t.Title.ToLower().Contains(q) ||
                t.Description.ToLower().Contains(q));
        }

        query = sort == "dueDate:desc"
            ? query.OrderByDescending(t => t.DueDate)
            : query.OrderBy(t => t.DueDate);

        return mapper.Map<IEnumerable<TicketDTO>>(await query.ToListAsync());
    }

    public async Task<TicketDTO?> GetByIdAsync(int id)
        => mapper.Map<TicketDTO>(await _context.Tickets.FindAsync(id));

    public async Task<TicketDTO> CreateAsync(TicketDTO ticket)
    {
        await _context.Tickets.AddAsync(mapper.Map<Ticket>(ticket));
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<bool> UpdateAsync(TicketDTO ticket)
    {
        _context.Tickets.Update(mapper.Map<Ticket>(ticket));
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Ticket = await _context.Tickets.FindAsync(id);
        if (Ticket == null) return false;

        _context.Tickets.Remove(Ticket);
        return await _context.SaveChangesAsync() > 0;
    }
}
