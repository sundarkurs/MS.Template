using AutoMapper;
using WSA.Microservice.Template.Application.DTO;
using WSA.Microservice.Template.Domain.Entities;

namespace WSA.Microservice.Template.Application.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Config, ConfigDto>().ReverseMap();
            CreateMap<ConfigRequest, Config>();
        }
    }
}
