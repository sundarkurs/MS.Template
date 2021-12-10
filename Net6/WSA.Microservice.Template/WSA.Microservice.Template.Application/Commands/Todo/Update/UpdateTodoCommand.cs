using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Update
{
    public class UpdateTodoCommand : IRequest<Response<TodoDto>>
    {
        public int Id { get; set; }
        public TodoRequest Todo { get; set; }
    }
}
