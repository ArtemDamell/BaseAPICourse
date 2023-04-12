using AutoMapper;
using Core.DTO;
using Core.Models;

namespace WebAPIBasic.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventAdministratorDTO, EventAdministrator>();
            CreateMap<EventAdministrator, EventAdministratorDTO>();
        }
    }
}
