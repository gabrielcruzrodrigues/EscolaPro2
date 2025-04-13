using AutoMapper;
using EscolaPro.Models;
using EscolaPro.Models.Dtos;

namespace EscolaPro.AutoMapper
{
    public class UserGeneralProfile : Profile
    {
        public UserGeneralProfile()
        {
            CreateMap<UserGeneral, UserGeneralDto>();
        }
    }
}
