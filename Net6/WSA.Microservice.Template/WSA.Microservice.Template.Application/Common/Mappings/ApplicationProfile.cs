using AutoMapper;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Domain.Entities;

namespace WSA.Microservice.Template.Application.Common.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<TodoRequest, Todo>();
        }
    }
}
