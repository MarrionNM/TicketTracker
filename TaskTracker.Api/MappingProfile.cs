using AutoMapper;
using TaskTracker.Api.Data.DTO;
using TaskTracker.Api.Data.Models;

namespace TaskTracker.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Ticket, TicketDTO>().ReverseMap();
    }
}
