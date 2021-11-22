using AutoMapper;
using Core.DTO;
using Core.Models;

namespace WebAPIBasic.AutoMapper
{
    // 74. Добавить в профиль AutoMapper маршруты конвертации
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventAdministratorDTO, EventAdministrator>();
            CreateMap<EventAdministrator, EventAdministratorDTO>();
        }
    }
}
