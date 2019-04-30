using AutoMapper;
using WsTestes.Model;

namespace WsProjetoVinicius.Model
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>();            
        }

    }
}
