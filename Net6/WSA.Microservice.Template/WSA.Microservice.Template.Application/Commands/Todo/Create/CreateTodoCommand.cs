using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Create
{
    public class CreateTodoCommand : IRequest<Response<TodoDto>>
    {
        public TodoRequest Todo { get; set; }
    }
}
