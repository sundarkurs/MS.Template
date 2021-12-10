using MediatR;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Delete
{
    public class DeleteTodoCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
